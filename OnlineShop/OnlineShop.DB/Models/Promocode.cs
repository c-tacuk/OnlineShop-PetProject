using System;

namespace OnlineShop.Db.Models
{
    public class Promocode
    {
        public Guid Id { get; set; }
        public int Discount { get; set; }
        public string Text { get; set; }
    }
}
