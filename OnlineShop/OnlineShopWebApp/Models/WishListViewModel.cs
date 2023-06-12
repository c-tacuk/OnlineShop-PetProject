using System;
using System.Collections.Generic;

namespace OnlineShopWebApp.Models
{
    public class WishListViewModel
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public List<WishItemViewModel> Items { get; set; }
    }
}
