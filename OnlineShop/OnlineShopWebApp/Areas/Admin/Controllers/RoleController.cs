using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Db;
using OnlineShopWebApp.Models;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class RoleController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        public async Task<ActionResult> Roles()
        {
            var roles = await roleManager.Roles.ToListAsync();
            return View(roles.Select(x => new RoleViewModel { Name = x.Name }).ToList());
        }
        public IActionResult AddRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> AddRoleAsync(RoleViewModel newRole)
        {
            var result = await roleManager.CreateAsync(new IdentityRole(newRole.Name));
            if (result.Succeeded)
                return RedirectToAction("Roles");
            else
            {
                foreach(var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }
            return View(newRole);
        }
        public async Task<ActionResult> RemoveRoleAsync(string name)
        {
            var role = await roleManager.FindByNameAsync(name);
            if (role != null)
                await roleManager.DeleteAsync(role);
            return RedirectToAction("Roles");
        }
    }
}
