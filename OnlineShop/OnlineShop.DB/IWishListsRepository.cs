using OnlineShop.Db.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShopWebApp
{
    public interface IWishListsRepository
    {
        Task<WishList> TryGetWishListAsync(string userId);
        Task AddItemAsync(Product product, string userId);
        Task RemoveItemAsync(Product product, string userId);
        Task IsWishAsync(List<Product> products, string userId);
        Task IsWishAsync(Product product, string userId);
    }
}
