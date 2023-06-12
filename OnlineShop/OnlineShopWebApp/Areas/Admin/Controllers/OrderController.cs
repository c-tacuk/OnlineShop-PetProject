using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class OrderController : Controller
    {
        private readonly IMapper mapper;
        private readonly IOrdersRepository ordersRepository;
        public OrderController(IMapper mapper, IOrdersRepository ordersRepository)
        {
            this.mapper = mapper;
            this.ordersRepository = ordersRepository;
        }
        public async Task<ActionResult> OrderDetails(string orderId)
        {
            var order = await ordersRepository.TryGetByIdAsync(orderId);
            var orderViewModel = mapper.Map<OrderViewModel>(order);
            return View(orderViewModel);
        }

        [HttpPost]
        public async Task<ActionResult> OrderDetails(string orderId, OrderStatusViewModel status)
        {
            var order = await ordersRepository.TryGetByIdAsync(orderId);
            await ordersRepository.UpdateOrderStatusAsync(orderId, (OrderStatus)(int)status);
            var orderViewModel = mapper.Map<OrderViewModel>(order);
            return View(orderViewModel);
        }
        public async Task<ActionResult> Orders()
        {
            var orders = await ordersRepository.GetAllAsync();
            var ordersViewModels = mapper.Map<List<OrderViewModel>>(orders);
            return View(ordersViewModels);
        }
    }
}
