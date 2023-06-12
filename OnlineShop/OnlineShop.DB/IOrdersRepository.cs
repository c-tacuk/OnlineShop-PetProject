using OnlineShop.Db.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShop.Db
{
    public interface IOrdersRepository
    {
        Task SaveOrderAsync(Order order);
        Task<List<Order>> GetAllAsync();
        Task<List<Order>> GetUserOrdersAsync(string userEmail);
        Task<Order> TryGetByIdAsync(string orderId);
        Task UpdateOrderStatusAsync(string orderId, OrderStatus status);
    }
}
