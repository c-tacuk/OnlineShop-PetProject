using System;

namespace OnlineShop.Db.Models
{
    public class WishItem
    {
        public Guid Id { get; set; }
        public Product Product { get; set; }
        public WishList WishList { get; set; }
    }
}
