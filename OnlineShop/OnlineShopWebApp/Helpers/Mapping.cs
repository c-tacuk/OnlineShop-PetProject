using AutoMapper;
using OnlineShop.Db.Models;
using OnlineShopWebApp.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.Helpers
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap <Cart, CartViewModel > ().ReverseMap();
            CreateMap<CartItem, CartItemViewModel>().ReverseMap();
            CreateMap<Comment, CommentViewModel>().ReverseMap();
            CreateMap<Order, OrderViewModel>().ReverseMap();
            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<UserDeliveryInfo, UserDeliveryInfoViewModel>().ReverseMap();
            CreateMap<WishItem, WishItemViewModel>().ReverseMap();
            CreateMap<WishList, WishListViewModel>().ReverseMap();
        }
    }
}
