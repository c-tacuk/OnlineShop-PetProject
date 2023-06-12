using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace OnlineShop.Db.Models
{
    public class User : IdentityUser
    {
        public string Image { get; set; } = "/images/users/defaultPhoto.png";
    }
}
