using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Db;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace OnlineShopWebApp.Controllers
{
    public class CommentController : Controller
    {
        private readonly IMapper mapper;
        private readonly IProductsRepository productsRepository;
        private readonly ICommentsRepository feedBacksRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly string userId;
        public CommentController(IMapper mapper, IProductsRepository productsRepository, IWishListsRepository wishListsRepository, ICommentsRepository feedBacksRepository, IHttpContextAccessor httpContextAccessor)
        {
            this.mapper = mapper;
            this.productsRepository = productsRepository;
            this.feedBacksRepository = feedBacksRepository;
            this.httpContextAccessor = httpContextAccessor;
            userId = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
        public async Task<ActionResult> CommentsOfProduct(Guid id)
        {
            var product = await productsRepository.TryGetByIdAsync(id);
            var productViewModel = mapper.Map<ProductViewModel>(product);
            return View(productViewModel);
        }
        public async Task<ActionResult> PostComment(Guid id)
        {
            var product = await productsRepository.TryGetByIdAsync(id);
            var productViewModel = mapper.Map<ProductViewModel>(product);
            productViewModel.CommentViewModels = mapper.Map<List<CommentViewModel>>(product.Comments);
            return View(productViewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> PostComment(Comment comment, Guid productId)
        {
            var product = await productsRepository.TryGetByIdAsync(productId);
            var productViewModel = mapper.Map<ProductViewModel>(product);
            productViewModel.CommentViewModels = mapper.Map<List<CommentViewModel>>(product.Comments);
            if (ModelState.IsValid)
            {
                await feedBacksRepository.SaveAsync(comment, productId, new Guid(userId));
                return View(productViewModel);
            }
            return View(productViewModel);
        }
    }
}
