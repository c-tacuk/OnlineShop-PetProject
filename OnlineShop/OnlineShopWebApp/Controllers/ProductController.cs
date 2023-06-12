using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShopWebApp.Models;
using System;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IMapper mapper;
        private readonly IProductsRepository productsRepository;
        private readonly IWishListsRepository wishListsRepository;
        public ProductController(IMapper mapper, IProductsRepository productsRepository, IWishListsRepository wishListsRepository)
        {
            this.mapper = mapper;
            this.productsRepository = productsRepository;
            this.wishListsRepository = wishListsRepository;
        }
        public async Task<ActionResult> ProductDetails(Guid id)
        {
            var product = await productsRepository.TryGetByIdAsync(id);
            if (product == null)
                return View("ProductNotFound");
            await wishListsRepository.IsWishAsync(product, Constants.UserId);
            var productViewModel = mapper.Map<ProductViewModel>(product);
            return View("ProductDetails", productViewModel);
        }
    }
}
