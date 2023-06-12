using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShop.Db
{
    public class OrdersDbRepository : IOrdersRepository
    {
        private readonly DatabaseContext databaseContext;
        private readonly UserManager<User> userManager;
        public OrdersDbRepository(DatabaseContext databaseContext, UserManager<User> userManager)
        {
            this.databaseContext = databaseContext;
            this.userManager = userManager;

        }
        public async Task SaveOrderAsync(Order order)
        {
            if (order != null)
            {
                await databaseContext.Orders.AddAsync(order);
                await databaseContext.SaveChangesAsync();
            }
        }
        public async Task<List<Order>> GetAllAsync()
        {
            return await databaseContext.Orders.Include(x => x.UserInfo).Include(x => x.CartItems).ThenInclude(x => x.Product).ToListAsync();
        }
        public async Task<List<Order>> GetUserOrdersAsync(string userEmail)
        {
            var user = await userManager.FindByEmailAsync(userEmail);
            var orders = new List<Order>();
            foreach (var order in await GetAllAsync())
            {
                if (order.UserId == user.Id)
                {
                    orders.Add(order);
                }
            }
            return orders;
        }
        public async Task<Order> TryGetByIdAsync(string orderId)
        {
            var order = await databaseContext.Orders.Include(x => x.UserInfo).Include(x => x.CartItems).ThenInclude(x => x.Product).FirstOrDefaultAsync(x => x.Id.ToString() == orderId);
            return order;
        }
        public async Task UpdateOrderStatusAsync(string orderId, OrderStatus status)
        {
            var order = await TryGetByIdAsync(orderId);
            order.Status = status;
            await databaseContext.SaveChangesAsync();
        }
    }
}
