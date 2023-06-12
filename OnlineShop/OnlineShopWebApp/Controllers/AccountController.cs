using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        public IActionResult Login(string returnUrl)
        {
            if (returnUrl == null) 
                returnUrl = "/Catalog/Index";
            return View(new AuthorizingUser() { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<ActionResult> Login(AuthorizingUser user)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(user.Email, user.Password, user.Remember, false);
                if (result.Succeeded)
                {
                    if (user.ReturnUrl == "/accout/login")
                    {
                        user.ReturnUrl = "/catalog";
                    }
                    return Redirect(user.ReturnUrl);
                }
                else
                    ModelState.AddModelError("Email", "Неправильный email или пароль");
            }
            var authUser = new AuthorizingUser();
            authUser.ReturnUrl = "/accout/login";
            return View("Login", authUser);
        }
        public async Task<ActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Catalog");
        }

        public IActionResult Register(string returnUrl)
        {
            if (returnUrl == null)
                returnUrl = "/Catalog/Index";
            return View(new RegisteringUser() { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public async Task<ActionResult> Register(RegisteringUser registeringUser)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = registeringUser.Email, Email = registeringUser.Email, PhoneNumber = registeringUser.Phone };
                var result = await userManager.CreateAsync(user, registeringUser.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return Redirect(registeringUser.ReturnUrl);
                }
                else
                {
                    foreach (var error in result.Errors)
                        ModelState.AddModelError("Password", error.Description);
                }
            }
            return View("Register", registeringUser);
        }
    }
}
