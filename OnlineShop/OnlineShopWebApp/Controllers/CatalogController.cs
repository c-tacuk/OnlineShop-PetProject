using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShopWebApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Controllers
{
    public class CatalogController : Controller
    {
        private readonly IMapper mapper;
        private readonly IProductsRepository productsRepository;
        private readonly IWishListsRepository wishListsRepository;
        public CatalogController(IMapper mapper, IProductsRepository productsRepository, IWishListsRepository wishListsRepository)
        {
            this.mapper = mapper;
            this.productsRepository = productsRepository;
            this.wishListsRepository = wishListsRepository;
        }
        public async Task<ActionResult> Index()
        {
            var products = await productsRepository.GetAllAsync();
            await wishListsRepository.IsWishAsync(products, Constants.UserId);
            var productsViewModels = mapper.Map<List<ProductViewModel>>(products);
            return View(productsViewModels);
        }

        [HttpPost]
        public async Task<ActionResult> Search(string request)
        {
            if(request == null) { return RedirectToAction("Index"); }
            var products = await productsRepository.SearchAsync(request);
            if (products == null || products.Count == 0)
                return View("NotFound");
            var productsViewModels = mapper.Map<List<ProductViewModel>>(products);
            return View("Index", productsViewModels);
        }
    }
}
