using System;
using System.Collections.Generic;

namespace OnlineShop.Db.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Cost { get; set; }
        public string Image { get; set; }
        public bool InWishList { get; set; }
        public List<Comment> Comments { get; set; }
        public List<CartItem> CartItems { get; set; }
        public List<WishItem> WishItems { get; set; }
    }
    
}
