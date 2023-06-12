using OnlineShop.Db.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShop.Db
{
    public interface ICartsRepository
    {
        Task<List<CartItem>> GetAllAsync(string userId);
        Task<Cart> TryGetCartAsync(string userId);
        Task AddAsync(Product product, string userId);
        Task RemoveCartItemAsync(Product product, string userId);
        Task ClearCartAsync(Cart cart);
        Task RemoveCartAsync(Cart cart);
    }
}
