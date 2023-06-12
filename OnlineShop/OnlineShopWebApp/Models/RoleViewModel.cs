using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class RoleViewModel
    {
        [Required(ErrorMessage = "Введите название роли")]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Нельзя использовать такое короткое название")]
        public string Name { get; set; }
    }
}
