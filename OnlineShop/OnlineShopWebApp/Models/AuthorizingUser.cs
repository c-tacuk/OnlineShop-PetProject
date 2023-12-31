﻿
using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class AuthorizingUser
    {
        [Required(ErrorMessage = "Укажите email")]
        [EmailAddress(ErrorMessage = "Указан некорректный email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Укажите пароль")]
        [StringLength(25, MinimumLength = 6, ErrorMessage = "Длина пароля должна быть от 6 до 25 символов")]
        public string Password { get; set; }
        public bool Remember { get; set; }
        public string ReturnUrl { get; set; }
    }
}
