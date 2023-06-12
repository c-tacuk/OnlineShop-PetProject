namespace OnlineShopWebApp.Models
{
    public class WishItemViewModel
    {
        public ProductViewModel Product { get; set; }
        public string Image { get { return Product.Image; } }
    }
}
