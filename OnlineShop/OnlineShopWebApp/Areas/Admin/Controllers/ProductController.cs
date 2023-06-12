using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Areas.Admin.Controllers
{
    [Area(Constants.AdminRoleName)]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class ProductController : Controller
    {
        private readonly IMapper mapper;
        private readonly IProductsRepository productsRepository;
        private readonly IWebHostEnvironment appEnvironment;
        public ProductController(IMapper mapper, IProductsRepository productsRepository, IWebHostEnvironment appEnvironment)
        {
            this.mapper = mapper;
            this.productsRepository = productsRepository;
            this.appEnvironment = appEnvironment;
        }

        public async Task<ActionResult> Products()
        {
            var products = await productsRepository.GetAllAsync();
            var productsViewModels = mapper.Map<List<ProductViewModel>>(products);
            return View(productsViewModels);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProductViewModel creatingProduct)
        {
            if (ModelState.IsValid)
            {
                if (creatingProduct.UploadedImage != null)
                {
                    var productImagesPath = Path.Combine(appEnvironment.WebRootPath + "/images/products/");
                    if (!Directory.Exists(productImagesPath))
                        Directory.CreateDirectory(productImagesPath);
                    var fileName = Guid.NewGuid() + "." + creatingProduct.UploadedImage.FileName.Split('.').Last();
                    using (var fileStream = new FileStream(productImagesPath + fileName, FileMode.Create))
                    {
                        creatingProduct.UploadedImage.CopyTo(fileStream);
                    }
                    creatingProduct.Image = "/images/products/" + fileName;
                }
                var dbProduct = mapper.Map<Product>(creatingProduct);
                await productsRepository.CreateAsync(dbProduct);
                var productViewModel = mapper.Map<ProductViewModel>(dbProduct);
                return View("ProductDetails", productViewModel);
            }
            return View();
        }

        public async Task<ActionResult> Edit(Guid id)
        {
            var product = await productsRepository.TryGetByIdAsync(id);
            if (product == null)
                return View("ProductNotFound");
            var productViewModel = mapper.Map<ProductViewModel>(product);
            return View(productViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(ProductViewModel editingProduct)
        {
            if (ModelState.IsValid)
            {
                if (editingProduct.UploadedImage != null)
                {
                    var productImagesPath = Path.Combine(appEnvironment.WebRootPath + "/images/products/");
                    if (!Directory.Exists(productImagesPath))
                        Directory.CreateDirectory(productImagesPath);
                    var fileName = Guid.NewGuid() + "." + editingProduct.UploadedImage.FileName.Split('.').Last();
                    using (var fileStream = new FileStream(productImagesPath + fileName, FileMode.Create))
                    {
                        editingProduct.UploadedImage.CopyTo(fileStream);
                    }
                    editingProduct.Image = "/images/products/" + fileName;
                }
                var dbProduct = mapper.Map<Product>(editingProduct);
                await productsRepository.EditAsync(dbProduct);
                var editedProductViewModel = mapper.Map<ProductViewModel>(await productsRepository.TryGetByIdAsync(editingProduct.Id));
                return View("ProductDetails", editedProductViewModel);
            }
            return View();
        }
        public async Task<ActionResult> RemoveProduct(Guid id)
        {
            await productsRepository.RemoveAsync(id);
            var uptadetProductsList = await productsRepository.GetAllAsync();
            var updateProductsListViewModel = new List<ProductViewModel>();
            foreach (var product in uptadetProductsList)
            {
                updateProductsListViewModel.Add(mapper.Map<ProductViewModel>(product));
            }
            return View("Products", updateProductsListViewModel);
        }
    }
}
