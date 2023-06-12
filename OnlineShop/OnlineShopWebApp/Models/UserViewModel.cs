using Microsoft.AspNetCore.Http;

namespace OnlineShopWebApp.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public UserRole Role { get; set; } = UserRole.User;
        public IFormFile UploadedImage { get; set; }
        public string Image { get; set; }
        
    }
}
