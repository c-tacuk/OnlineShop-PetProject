using Microsoft.EntityFrameworkCore;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopWebApp
{
    public class WishListsDbRepository : IWishListsRepository
    {
        private DatabaseContext databaseContext;
        public WishListsDbRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }
        
        public async Task<WishList> TryGetWishListAsync(string userId)
        {
            var wishList = await databaseContext.WishLists.Include(x => x.Items).ThenInclude(x => x.Product).FirstOrDefaultAsync(x => x.UserId == userId);
            return wishList;
        }
        public async Task AddItemAsync(Product product, string userId)
        {
            var wishList = await TryGetWishListAsync(userId);
            if (wishList == null)
            {
                var newWishList = new WishList()
                {
                    UserId = userId
                };
                var items = new List<WishItem>
                    {
                        new WishItem
                        {
                            Product = product,
                            WishList = newWishList
                        }
                    };
                newWishList.Items = items;
                product.InWishList = true;
                await databaseContext.AddAsync(newWishList);
                await databaseContext.SaveChangesAsync();
            }
            else
            {
                var item = wishList.Items.FirstOrDefault(x => x.Product.Id == product.Id);
                if (item == null)
                {
                    wishList.Items.Add(new WishItem
                    {
                        Product = product,
                        WishList = wishList
                    });
                }
                product.InWishList = true;
                await databaseContext.SaveChangesAsync();
            }
        }
        public async Task RemoveItemAsync(Product product, string userId)
        {
            var wishList = await TryGetWishListAsync(userId);
            if (wishList != null)
            {
                var wishItem = wishList.Items.FirstOrDefault(x => x.Product.Id == product.Id);
                if (wishItem != null)
                {
                    wishItem.Product.InWishList = false;
                    wishList.Items.Remove(wishItem);
                    databaseContext.Remove(wishItem);
                    if (wishList.Items.Count == 0)
                        databaseContext.Remove(wishList);
                }
            }
            await databaseContext.SaveChangesAsync();
        }
        public async Task IsWishAsync(List<Product> products, string userId)
        {
            var wishList = await TryGetWishListAsync(userId);
            if (wishList != null && wishList.Items.Count != 0)
            {
                foreach (var product in products)
                {
                    var wishProduct = wishList.Items.FirstOrDefault(x => x.Product.Id == product.Id);
                    if (wishProduct != null)
                        product.InWishList = true;
                }
                await databaseContext.SaveChangesAsync();
            }
        }
        public async Task IsWishAsync(Product product, string userId)
        {
            var wishList = await TryGetWishListAsync(userId);
            if (wishList != null)
            {
                var wishProduct = wishList.Items.FirstOrDefault(x => x.Product.Id == product.Id);
                if (wishProduct != null)
                    product.InWishList = true;
                else
                    product.InWishList = false;
            }
            await databaseContext.SaveChangesAsync();
        }
    }
}
