using OnlineShop.Db.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShop.Db
{
    public interface IProductsRepository
    {
        Task<List<Product>> GetAllAsync();
        Task<Product> TryGetByIdAsync(Guid id);
        Task CreateAsync(Product creatingProduct);
        Task RemoveAsync(Guid id);
        Task EditAsync(Product editingProduct);
        Task<List<Product>> SearchAsync(string request);
    }
}

