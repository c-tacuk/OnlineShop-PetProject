using System;
using System.Collections.Generic;

namespace OnlineShop.Db.Models
{
    public class WishList
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public List<WishItem> Items { get; set; }
    }
}
