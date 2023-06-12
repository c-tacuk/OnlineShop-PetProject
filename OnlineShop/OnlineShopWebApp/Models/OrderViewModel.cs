using System;
using System.Collections.Generic;

namespace OnlineShopWebApp.Models
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public List<CartItemViewModel> CartItems { get; set; }
        public UserDeliveryInfoViewModel UserInfo { get; set; }
        public OrderStatusViewModel Status { get; set; }
        public DateTime CreateTime { get; set; }

        public OrderViewModel()
        {
            Status = OrderStatusViewModel.Created;
            CreateTime = DateTime.Now;
        }
    }
}
