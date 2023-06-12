using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShop.Db
{
    public class CartsDbRepository : ICartsRepository
    {
        private DatabaseContext databaseContext;
        public CartsDbRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }
        public async Task<List<CartItem>> GetAllAsync(string id)
        {
            var existingCart = await TryGetCartAsync(id);
            if (existingCart == null)
                return null;
            return existingCart.Items;
        }
        public async Task<Cart> TryGetCartAsync(string userId)
        {
            var cart = await databaseContext.Carts.Include(x=>x.Items).ThenInclude(x=>x.Product).FirstOrDefaultAsync(x => x.UserId == userId);
            return cart;
        }
        public async Task AddAsync(Product product, string userId)
        {
            var cart = await TryGetCartAsync(userId);
            if (cart == null)
            {
                var newCart = new Cart()
                {
                    UserId = userId
                };

                newCart.Items = new List<CartItem>
                    {
                        new CartItem
                        {
                            Count = 1,
                            Product = product,
                        }
                    };
                await databaseContext.Carts.AddAsync(newCart);
            }
            else
            {
                var cartItem = cart.Items.FirstOrDefault(x => x.Product.Id == product.Id);
                if (cartItem != null) cartItem.Count++;
                else
                {
                    var newCartItem = (new CartItem
                    {
                        Count = 1,
                        Product = product,
                    });
                    cart.Items.Add(newCartItem);
                    await databaseContext.CartItems.AddAsync(newCartItem);
                }
            }
            await databaseContext.SaveChangesAsync();
        }
        public async Task RemoveCartItemAsync(Product product, string id)
        {
            var cart = await TryGetCartAsync(id);
            var сartItem = cart.Items.FirstOrDefault(x => x.Product.Id == product.Id);
            if (сartItem != null)
            {
                сartItem.Count--;
                if(сartItem.Count == 0)
                {
                    databaseContext.CartItems.Remove(сartItem);
                }
                await databaseContext.SaveChangesAsync();
            }
            if (cart.Items.Count == 0)
                databaseContext.Carts.Remove(cart);
            await databaseContext.SaveChangesAsync();
        }
        public async Task ClearCartAsync(Cart cart)
        {
            var items = cart.Items;
            foreach (var item in items)
            {
                databaseContext.Remove(item);
            }
            databaseContext.Carts.Remove(cart);
            await databaseContext.SaveChangesAsync();
        }
        public async Task RemoveCartAsync(Cart cart)
        {
            databaseContext.Carts.Remove(cart);
            await databaseContext.SaveChangesAsync();
        }
    }
}
