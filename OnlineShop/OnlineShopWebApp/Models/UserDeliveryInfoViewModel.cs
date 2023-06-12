using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public class UserDeliveryInfoViewModel
    {
        [Required(ErrorMessage = "Укажите своё имя")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Имя должно состоять минимум из двух букв")]
        public string Name { get; set; }


        [Required(ErrorMessage = "Укажите номер телефона")]
        [Phone(ErrorMessage = "Некорректный номер телефона")]
        public string Tel { get; set; }


        [Required(ErrorMessage = "Укажите адрес доставки")]
        [StringLength(300, MinimumLength = 10, ErrorMessage = "Адрес должен содержать в себе город, улицу и дом")]
        public string Address { get; set; }


        [Required(ErrorMessage = "Укажите свой email")]
        [EmailAddress(ErrorMessage = "Указан некорректный email")]
        public string Email { get; set; }

        public string PromocodeText { get; set; }
    }
}
