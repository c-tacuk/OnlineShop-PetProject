using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Helpers;
using OnlineShopWebApp.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Views.Shared.Components.Cart
{
    public class CartViewComponent : ViewComponent
    {
        private readonly IMapper mapper;
        private readonly ICartsRepository cartsRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly string userId;
        public CartViewComponent(IMapper mapper, ICartsRepository cartsRepository, IHttpContextAccessor httpContextAccessor)
        {
            this.mapper = mapper;
            this.cartsRepository = cartsRepository;
            this.httpContextAccessor = httpContextAccessor;
            userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                var cookies = httpContextAccessor.HttpContext.Request.Cookies;
                if (cookies.ContainsKey("CurrentUserId"))
                {
                    userId = cookies["CurrentUserId"];
                }
                else
                {
                    userId = Guid.NewGuid().ToString();
                    httpContextAccessor.HttpContext.Response.Cookies.Append("CurrentUserId", userId);
                }
            }
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cart = await cartsRepository.TryGetCartAsync(userId);
            var cartViewModel = mapper.Map<CartViewModel>(cart);           
            return View("Cart", cartViewModel);
        }
    }
}
