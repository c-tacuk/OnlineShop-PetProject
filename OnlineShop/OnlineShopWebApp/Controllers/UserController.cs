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

namespace OnlineShopWebApp.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IMapper mapper;
        private readonly IOrdersRepository ordersRepository;
        private readonly UserManager<User> userManager;
        private readonly IWebHostEnvironment appEnvironment;
        private IdentityContext identityContext;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly string userId;
        public UserController(IMapper mapper, IOrdersRepository ordersRepository, UserManager<User> userManager, IWebHostEnvironment appEnvironment, IdentityContext identityContext, IHttpContextAccessor httpContextAccessor)
        {
            this.mapper = mapper;
            this.ordersRepository = ordersRepository;
            this.userManager = userManager;
            this.appEnvironment = appEnvironment;
            this.identityContext = identityContext;
            this.httpContextAccessor = httpContextAccessor;
            userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
        public async Task<ActionResult> UserDetails(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
                user = await userManager.FindByIdAsync(userId);
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
        public async Task<ActionResult> EditPassword(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            var ordersViewModel = mapper.Map<List<OrderViewModel>>(await ordersRepository.GetUserOrdersAsync(user.Email));
            ViewBag.SomeProperty = ordersViewModel;
            var regUser = new RegisteringUser { Email = email };
            return View(regUser);
        }

        [HttpPost]
        public async Task<ActionResult> EditPassword(string password, string repeadPassword, string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (password != repeadPassword)
            {
                ModelState.AddModelError("Password", "Пароли должны совпадать");
            }
            if (password == email)
            {
                ModelState.AddModelError("Password", "Пароль имя не должны совпадать");
            }
            if (ModelState.IsValid)
            {
                var newPasswordHash = userManager.PasswordHasher.HashPassword(user, password);
                user.PasswordHash = newPasswordHash;
                await userManager.UpdateAsync(user);
                var userViewModel = mapper.Map<UserViewModel>(user);
                return RedirectToAction("UserDetails", userViewModel);
            }
            ViewBag.SomeProperty = userId;
            var regUser = new RegisteringUser { Email = email };
            return View(regUser);
        }
        public async Task<ActionResult> EditEmail(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            return View(mapper.Map<UserViewModel>(user));
        }

        [HttpPost]
        public async Task<ActionResult> EditEmail(string oldEmail, string email)
        {
            var user = await userManager.FindByEmailAsync(oldEmail);
            if (ModelState.IsValid)
            {
                user.Email = email;
                user.UserName = email;
                await userManager.UpdateAsync(user);
                var userViewModel = mapper.Map<UserViewModel>(user);
                return RedirectToAction("UserDetails", userViewModel);
            }
            return View(mapper.Map<UserViewModel>(user));
        }

        public async Task<ActionResult> Remove(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user != null)
                await userManager.DeleteAsync(user);
            return RedirectToAction("Users");
        }
    }
}
