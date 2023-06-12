using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShop.Db
{
    public class ProductsDbRepository : IProductsRepository
    {
        private DatabaseContext databaseContext;
        public ProductsDbRepository(DatabaseContext databaseContext)
        {
            this.databaseContext = databaseContext;
        }
        
        public async Task<List<Product>> GetAllAsync()
        {
            return await databaseContext.Products.Include(x => x.CartItems).Include(x => x.Comments).ToListAsync(); ;
        }
        public async Task<Product> TryGetByIdAsync(Guid id)
        {
            return await databaseContext.Products.Include(x=>x.CartItems).Include(x=>x.Comments).FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task CreateAsync(Product creatingProduct)
        {
            databaseContext.Products.Add(creatingProduct);
            await databaseContext.SaveChangesAsync();
        }
        public async Task RemoveAsync(Guid id)
        {
            var product = await databaseContext.Products.FirstOrDefaultAsync(x => x.Id == id);
            databaseContext.Products.Remove(product);
            await databaseContext.SaveChangesAsync();

            var products = await GetAllAsync();
        }
        public async Task EditAsync(Product editingProduct)
        {
            var product = await databaseContext.Products.FirstOrDefaultAsync(x => x.Id == editingProduct.Id);
            product.Name = editingProduct.Name;
            product.Description = editingProduct.Description;
            product.Cost = editingProduct.Cost;
            if(editingProduct.Image != null)
                product.Image = editingProduct.Image;
            await databaseContext.SaveChangesAsync();
        }
        public async Task<List<Product>> SearchAsync(string request)
        {
            var products = await GetAllAsync();
            var result = new List<Product>();
            foreach (var product in products)
            {
                if (product.Name.ToLower().Contains(request.ToLower()))
                    result.Add(product);
            }
            return result;
        }
    }
}
