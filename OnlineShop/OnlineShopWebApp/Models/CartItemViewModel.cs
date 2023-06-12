namespace OnlineShopWebApp.Models
{
    public class CartItemViewModel
    {
        public ProductViewModel Product { get; set; }
        public int Count { get; set; }
        public decimal FullCost { get { return Product.Cost*Count; } }
    }
}
