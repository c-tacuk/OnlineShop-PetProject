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
    public class WishListController : Controller
    {
        private readonly IMapper mapper;
        private readonly IProductsRepository productsRepository;
        private readonly IWishListsRepository wishListsRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly string userId;
        public WishListController(IMapper mapper, IProductsRepository productsRepository, IWishListsRepository wishListsRepository, IHttpContextAccessor httpContextAccessor)
        {
            this.mapper = mapper;
            this.productsRepository = productsRepository;
            this.wishListsRepository = wishListsRepository;
            this.httpContextAccessor = httpContextAccessor;
            userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
        public async Task<ActionResult> Index()
        {
            var wishList = await wishListsRepository.TryGetWishListAsync(userId);
            if (wishList == null || wishList.Items.Count == 0)
                return View("Empty");
            var wishListViewModel = mapper.Map<WishListViewModel>(wishList);
            return View(wishListViewModel.Items);
        }
        public async Task<ActionResult> Add(Guid id)
        {
            var product = await productsRepository.TryGetByIdAsync(id);
            await wishListsRepository.AddItemAsync(product, userId);
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> Remove(Guid id)
        {
            var product = await productsRepository.TryGetByIdAsync(id);
            if (product != null)
            {
                await wishListsRepository.RemoveItemAsync(product, userId);
                return RedirectToAction("Index");
            }
            return View("../Product/NotFound");
        }
    }
}
