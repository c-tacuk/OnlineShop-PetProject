using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShopWebApp.Models;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly IMapper mapper;
        private readonly IProductsRepository productsRepository;
        private readonly ICartsRepository cartsRepository;
        private readonly IWishListsRepository wishListsRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly string userId;
        public CartController(IMapper mapper, IProductsRepository productsRepository, ICartsRepository cartsRepository, IWishListsRepository wishListsRepository, IHttpContextAccessor httpContextAccessor)
        {
            this.mapper = mapper;
            this.productsRepository = productsRepository;
            this.cartsRepository = cartsRepository;
            this.wishListsRepository = wishListsRepository;
            this.httpContextAccessor = httpContextAccessor;
            userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
        public async Task<ActionResult> Index()
        {
            var cart = await cartsRepository.TryGetCartAsync(userId);
            var cartViewModel = mapper.Map<CartViewModel>(cart);
            if (cartViewModel == null || cartViewModel.FullPrice == 0)
                return View("Empty");
            return View(cartViewModel);
        }
        public async Task<ActionResult> Add(Guid id)
        {
            var product = await productsRepository.TryGetByIdAsync(id);
            if (product != null)
            {
                await wishListsRepository.RemoveItemAsync(product, userId);
                await cartsRepository.AddAsync(product, userId);
                return RedirectToAction("Index");
            }
                return View("../Product/NotFound");
        }
        public async Task<ActionResult> Remove(Guid id)
        {
            var product = await productsRepository.TryGetByIdAsync(id);
            if (product != null)
            {
                await cartsRepository.RemoveCartItemAsync(product, userId);
                return RedirectToAction("Index");
            }
            return View("../Product/NotFound");
        }
        public async Task<ActionResult> Clear(string userId)
        {
            var cart = await cartsRepository.TryGetCartAsync(userId);
            await cartsRepository.ClearCartAsync(cart);
            return RedirectToAction("Index");
        }
    }
}
