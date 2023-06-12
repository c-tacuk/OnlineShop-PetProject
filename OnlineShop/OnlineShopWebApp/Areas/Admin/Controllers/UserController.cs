using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class UserController : Controller
    {
        private readonly IMapper mapper;
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IOrdersRepository ordersRepository;
        private readonly IWebHostEnvironment appEnvironment;
        private readonly IdentityContext identityContext;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly string userId;
        public UserController(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, IOrdersRepository ordersRepository, IWebHostEnvironment appEnvironment, IdentityContext identityContext, IHttpContextAccessor httpContextAccessor)
        {
            this.mapper = mapper;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.ordersRepository = ordersRepository;
            this.appEnvironment = appEnvironment;
            this.identityContext = identityContext;
            this.httpContextAccessor = httpContextAccessor;
            userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
        public IActionResult Users()
        {
            var users = userManager.Users.ToList();
            var userViewModel = mapper.Map<List<UserViewModel>>(users);
            return View(userViewModel);
        }
        public async Task<ActionResult> UserDetails(string email)
        {
            var user = userManager.FindByNameAsync(email).Result;
            var orders = await ordersRepository.GetUserOrdersAsync(user.Email);
            var ordersViewModel = mapper.Map<List<OrderViewModel>>(orders);
            ViewBag.SomeProperty = ordersViewModel;
            return View(mapper.Map<UserViewModel>(user));
        }
        [HttpPost]
        public async Task<ActionResult> UserDetails(string email, UserRole role)
        {
            var user = await userManager.FindByEmailAsync(email);
            if(role.ToString() == Constants.UserRoleName)
            {
                await userManager.RemoveFromRoleAsync(user, Constants.AdminRoleName);
                await userManager.AddToRoleAsync(user, Constants.UserRoleName);
            }
            else if (role.ToString() == Constants.AdminRoleName)
            {
                await userManager.RemoveFromRoleAsync(user, Constants.UserRoleName);
                await userManager.AddToRoleAsync(user, Constants.AdminRoleName);
            }
            var ordersViewModel = mapper.Map<List<OrderViewModel>>(await ordersRepository.GetUserOrdersAsync(user.Id));
            ViewBag.SomeProperty = ordersViewModel;
            return View(mapper.Map<UserViewModel>(user));
        }
        public async Task<ActionResult> EditPassword(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            return View(mapper.Map<UserViewModel>(user));
        }
        [HttpPost]
        public async Task<ActionResult> EditPassword(string newPassword, string repeadedPassword, string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (newPassword != repeadedPassword)
            {
                ModelState.AddModelError("Password", "Пароли должны совпадать");
            }
            if (newPassword == email)
            {
                ModelState.AddModelError("Password", "Пароль имя не должны совпадать");
            }
            if (ModelState.IsValid)
            {
                var newPasswordHash = userManager.PasswordHasher.HashPassword(user, newPassword);
                user.PasswordHash = newPasswordHash;
                await userManager.UpdateAsync(user);
                return RedirectToAction("Users");
            }
            return View(mapper.Map<UserViewModel>(user));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditPhoto(UserPhotoUploader user)
        {
            var userDB = userManager.FindByNameAsync(user.Name).Result;
            var userViewModel = mapper.Map<UserViewModel>(userDB);
            if (user.UploadedImage != null)
            {
                var userImagesPath = Path.Combine(appEnvironment.WebRootPath + "/images/users/");
                if (!Directory.Exists(userImagesPath))
                    Directory.CreateDirectory(userImagesPath);
                var fileName = Guid.NewGuid() + "." + user.UploadedImage.FileName.Split('.').Last();
                using (var fileStream = new FileStream(userImagesPath + fileName, FileMode.Create))
                {
                    user.UploadedImage.CopyTo(fileStream);
                }
                userViewModel.Image = "/images/users/" + fileName;
                userDB.Image = "/images/users/" + fileName;
                identityContext.SaveChanges();
            }
            var ordersViewModel = mapper.Map<List<OrderViewModel>>(await ordersRepository.GetUserOrdersAsync(userDB.Id));
            ViewBag.SomeProperty = ordersViewModel;
            return View("UserDetails", userViewModel);
        }
        [HttpPost]
        public async Task<ActionResult> DeletePhoto(string email)
        {
            var userDB = await userManager.FindByEmailAsync(email);
            var userViewModel = mapper.Map<UserViewModel>(userDB);
            userViewModel.Image = "/images/users/defaultPhoto.png";
            userDB.Image = "/images/users/defaultPhoto.png";
            identityContext.SaveChanges();
            var ordersViewModel = mapper.Map<List<OrderViewModel>>(await ordersRepository.GetUserOrdersAsync(userDB.Id));
            ViewBag.SomeProperty = ordersViewModel;
            return View("UserDetails", userViewModel);
        }
        public async Task<ActionResult> Remove(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user != null)
            {
                await userManager.DeleteAsync(user);
                if (email == httpContextAccessor.HttpContext.User.Identity.Name)
                {
                    await signInManager.SignOutAsync();
                }
            }
                
                
            return RedirectToAction("Users");
        }
    }
}
