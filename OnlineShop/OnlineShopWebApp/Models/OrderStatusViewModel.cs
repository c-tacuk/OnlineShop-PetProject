using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Models
{
    public enum OrderStatusViewModel
    {
        [Display(Name = "Создан")]
        Created,
        [Display(Name = "Обработан")]
        Processed, 
        [Display(Name = "В пути")]
        InTransit,
        [Display(Name = "В пункте выдачи")]
        Waiting,
        [Display(Name = "Доставлен")]
        Delivered,
        [Display(Name = "Отменён")]
        Cancelled
    }
}
