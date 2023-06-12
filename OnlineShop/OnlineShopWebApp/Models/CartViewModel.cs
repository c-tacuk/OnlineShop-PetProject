using System.Collections.Generic;
using System.Linq;

namespace OnlineShopWebApp.Models
{
    public class CartViewModel
    {
        public string UserId { get; set; }
        public List<CartItemViewModel> Items { get; set; }
        public decimal FullPrice 
        { 
            get 
            {
                decimal price = 0;

                if (Items == null) price = 0;

                else
                {
                    foreach (var item in Items)
                    {
                        price += item.FullCost;
                    }
                }
                return price;
            }
        }
        public int Count
        {
            get
            {
                if(Items != null)
                    return Items.Sum(x => x.Count);
                return 0;
            }
        }
    }
}
