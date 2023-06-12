using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public enum UserRole
    {
        [Display(Name = "Admin")]
        Admin,
        [Display(Name = "User")]
        User
    }
}