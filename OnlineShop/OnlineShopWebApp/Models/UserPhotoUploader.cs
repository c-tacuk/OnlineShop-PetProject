using Microsoft.AspNetCore.Http;

namespace OnlineShopWebApp.Models
{
    public class UserPhotoUploader
    {
        public string Name { get; set; }
        public IFormFile UploadedImage { get; set; }
    }
}
