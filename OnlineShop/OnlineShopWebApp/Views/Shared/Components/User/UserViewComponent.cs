using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using System;

namespace OnlineShopWebApp.Views.Shared.Components.User
{
    public class UserViewComponent : ViewComponent
    {
        private readonly UserManager<OnlineShop.Db.Models.User> userManager;
        public UserViewComponent(UserManager<OnlineShop.Db.Models.User> userManager)
        {
            this.userManager = userManager;
        }
        public IViewComponentResult Invoke(string claimId)
        {
            var id = claimId.Remove(0, claimId.Length-36);
            var user = userManager.FindByIdAsync(id).Result;
            return View("User", user);
        }
    }
}
