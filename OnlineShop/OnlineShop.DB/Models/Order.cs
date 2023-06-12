using System;
using System.Collections.Generic;

namespace OnlineShop.Db.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public List<CartItem> CartItems { get; set; }
        public UserDeliveryInfo UserInfo { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreateTime { get; set; }
        public int FullPrice { get; set; }

        public Order()
        {
            Status = OrderStatus.Created;
            CreateTime = DateTime.Now;
        }
    }
}
