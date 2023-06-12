using Microsoft.EntityFrameworkCore;
using OnlineShop.Db.Models;
using System;

namespace OnlineShop.Db
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<WishList> WishLists { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Promocode> Promocodes { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.Migrate();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Product product1 = new Product
            {
                Id = new Guid("05170ab1-6d83-408a-8b0e-c7c021de2b18"),
                Name = "All Dogs - сухой корм для взрослых собак",
                Cost = 589,
                Description = "All Dogs - это полнорационный корм для собак всех пород.",
                Image = "/images/products/991804f2-de45-4eb4-b804-6998d7b394cf.jpg",
                InWishList = false
            };
            Product product2 = new Product
            {
                Id = new Guid("2f99df3f-5ee8-47ad-a17a-9a048138e2f6"),
                Name = "Chappi - сухой корм для взрослых собак",
                Cost = 599,
                Description = "Полнорационный сухой корм Chappi. Сытный мясной обед.",
                Image = "/images/products/bd1d9d7f-6ed0-4c8e-a249-b70c90235d7b.jpg",
                InWishList = false
            };
            Product product3 = new Product
            {
                Id = new Guid("e637fdb0-70f4-40a3-ab47-050e6cfb8dd1"),
                Name = "Cesar - влажный корм для щенят до двух лет",
                Cost = 499,
                Description = "Cesar - влажный корм с натуральными ингридиентами для щенят. Без консервантов.",
                Image = "/images/products/0b86f819-a994-4252-83af-890a44cc3f6c.jpg",
                InWishList = false
            };
            Product product4 = new Product
            {
                Id = new Guid("bcc1db7c-af36-4c86-bc39-4eaa27d0b01e"),
                Name = "Eukanua - сухой корм для котят до одного года",
                Cost = 419,
                Description = "All Dogs - это полнорационный корм для собак всех пород.",
                Image = "/images/products/701a141a-8a6f-4450-a703-d4d6176ae50b.jpg",
                InWishList = false
            };
            Product product5 = new Product
            {
                Id = new Guid("291c1c5c-7d2e-451e-8e71-16c373cb9da5"),
                Name = "Felix - сухой корм для кошек любого возраста",
                Cost = 449,
                Description = "Представляем Felix Двойная Вкуснятина – новый сухой корм для кошек, который полностью соответствует потребностям кошки.",
                Image = "/images/products/8acd43eb-4c9d-4c3d-a567-1ee58e0df4f0.jpeg",
                InWishList = false
            };
            Product product6 = new Product
            {
                Id = new Guid("359ee017-8b18-4575-ad81-be752f3647fb"),
                Name = "Pedigree - сухой корм для собак любого возраста",
                Cost = 2949,
                Description = "Полезная и вкусная еда, приготовленная с учётом всех физиологических потребностей собак разных пород.",
                Image = "/images/products/7786e83a-5fdb-4e78-babf-bb5c4387f933.jpg",
                InWishList = false
            };
            Product product7 = new Product
            {
                Id = new Guid("d1668211-b3fb-4b05-9cfc-338aabcaaa05"),
                Name = "Purina One - сухой корм для стерилизованных кошек",
                Cost = 2149,
                Description = "Корм Purina ONE разработан специально для стерилизованных кошек и кастрированных котов.",
                Image = "/images/products/0cc9fe76-f2a0-4c9f-b1b0-15c5b434aace.jpg",
                InWishList = false
            };
            Product product8 = new Product
            {
                Id = new Guid("5172be97-87a1-4ce3-bb80-20944c1182f5"),
                Name = "Sheba - влажный корм для кошек любого возраста",
                Cost = 39,
                Description = "Корм для кошек любого возраста. 85г телятина-язык в соусе.",
                Image = "/images/products/d53362e0-945a-469a-a072-ef11a2522db7.jpg",
                InWishList = false
            };


            Promocode promocode1 = new Promocode
            {
                Id = new Guid("2fb64628-eff5-4673-b39d-9d59ea557665"),
                Discount = 10,
                Text = "SKIDKA10"
            };
            Promocode promocode2 = new Promocode
            {
                Id = new Guid("ddf02e6e-ca02-4b4e-b23a-8581c0fa6d9a"),
                Discount = 20,
                Text = "SKIDKA20"
            };
            Promocode promocode3 = new Promocode
            {
                Id = new Guid("be60ec3e-e063-4401-b712-0bf76797edbd"),
                Discount = 23,
                Text = "NEW2023YEAR"
            };
            Promocode promocode4 = new Promocode
            {
                Id = new Guid("d573e8e9-82af-416c-b1d7-551a62988a20"),
                Discount = 50,
                Text = "FIRSTORDER"
            };

            // добавляем данные
            modelBuilder.Entity<Product>().HasData(product1, product2, product3, product4, product5, product6, product7, product8);
            modelBuilder.Entity<Promocode>().HasData(promocode1, promocode2, promocode3, promocode4);
        }
    }
}
