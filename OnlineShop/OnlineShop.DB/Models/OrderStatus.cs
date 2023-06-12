﻿using System.ComponentModel.DataAnnotations;

namespace OnlineShop.Db.Models
{
    public enum OrderStatus
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
