using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Controllers
{
    [Authorize]
    public class OrderController : Controller
    {
        private DatabaseContext databaseContext;
        private readonly IMapper mapper;
        private readonly ICartsRepository cartsRepository;
        private readonly IOrdersRepository ordersRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly string userId;
        public OrderController(DatabaseContext databaseContext, IMapper mapper, ICartsRepository cartsRepository, IOrdersRepository ordersRepository, IHttpContextAccessor httpContextAccessor)
        {
            this.databaseContext = databaseContext; 
            this.mapper = mapper;
            this.cartsRepository = cartsRepository;
            this.ordersRepository = ordersRepository;
            this.httpContextAccessor = httpContextAccessor;
            userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
        public IActionResult OrderRegistration()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> OrderRegistration(UserDeliveryInfoViewModel user)
        {
            if (ModelState.IsValid)
            {
                var cart = await cartsRepository.TryGetCartAsync(userId);
                if (cart != null)
                {
                    var order = new Order
                    {
                        UserId = userId,
                        UserInfo = mapper.Map<UserDeliveryInfo>(user),
                        CartItems = cart.Items,
                    };
                    var price = 0;
                    foreach (var item in cart.Items)
                    {
                        price += item.Product.Cost * item.Count;
                    }
                    var promo = await databaseContext.Promocodes.FirstOrDefaultAsync(x => x.Text == user.PromocodeText);
                    if (promo != null) 
                        price = price - (price * promo.Discount / 100);
                    order.FullPrice = price;
                    await ordersRepository.SaveOrderAsync(order);
                    await cartsRepository.RemoveCartAsync(cart);
                }
                return View("Done");
            }
            return View();
        }
    }
}
