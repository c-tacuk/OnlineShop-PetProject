using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }


        [Required(ErrorMessage = "Укажите имя продукта")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Имя должно состоять минимум из четырех символов")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Придумайте описание продукта")]
        [StringLength(1000, MinimumLength = 20, ErrorMessage = "Описание должно состоять минимум из двадцати символов")]
        public string Description { get; set; }


        [Required(ErrorMessage = "Укажите цену продукта")]
        [Range(0, int.MaxValue, ErrorMessage = "Пожалуйста, введите цену")]
        public int Cost { get; set; }

        public IFormFile UploadedImage { get; set; }
        public string Image { get; set; }
        public bool InWishList { get; set; }
        public List<CommentViewModel> CommentViewModels { get; set; }
    }
}
