﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OnlineShop.Db;

#nullable disable

namespace OnlineShop.Db.Migrations.Database
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20230110132043_AddPromocodes")]
    partial class AddPromocodes
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("OnlineShop.Db.Models.Cart", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Carts");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.CartItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("CartId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<Guid?>("OrderId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("CartId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Evaluation")
                        .HasColumnType("int");

                    b.Property<Guid?>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Order", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UserInfoId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserInfoId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Cost")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("InWishList")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = new Guid("05170ab1-6d83-408a-8b0e-c7c021de2b18"),
                            Cost = 589,
                            Description = "All Dogs - это полнорационный корм для собак всех пород.",
                            Image = "/images/products/991804f2-de45-4eb4-b804-6998d7b394cf.jpg",
                            InWishList = false,
                            Name = "All Dogs - сухой корм для взрослых собак"
                        },
                        new
                        {
                            Id = new Guid("2f99df3f-5ee8-47ad-a17a-9a048138e2f6"),
                            Cost = 599,
                            Description = "Полнорационный сухой корм Chappi. Сытный мясной обед.",
                            Image = "/images/products/bd1d9d7f-6ed0-4c8e-a249-b70c90235d7b.jpg",
                            InWishList = false,
                            Name = "Chappi - сухой корм для взрослых собак"
                        },
                        new
                        {
                            Id = new Guid("e637fdb0-70f4-40a3-ab47-050e6cfb8dd1"),
                            Cost = 499,
                            Description = "Cesar - влажный корм с натуральными ингридиентами для щенят. Без консервантов.",
                            Image = "/images/products/0b86f819-a994-4252-83af-890a44cc3f6c.jpg",
                            InWishList = false,
                            Name = "Cesar - влажный корм для щенят до двух лет"
                        },
                        new
                        {
                            Id = new Guid("bcc1db7c-af36-4c86-bc39-4eaa27d0b01e"),
                            Cost = 419,
                            Description = "All Dogs - это полнорационный корм для собак всех пород.",
                            Image = "/images/products/701a141a-8a6f-4450-a703-d4d6176ae50b.jpg",
                            InWishList = false,
                            Name = "Eukanua - сухой корм для котят до одного года"
                        },
                        new
                        {
                            Id = new Guid("291c1c5c-7d2e-451e-8e71-16c373cb9da5"),
                            Cost = 449,
                            Description = "Представляем Felix Двойная Вкуснятина – новый сухой корм для кошек, который полностью соответствует потребностям кошки.",
                            Image = "/images/products/8acd43eb-4c9d-4c3d-a567-1ee58e0df4f0.jpeg",
                            InWishList = false,
                            Name = "Felix - сухой корм для кошек любого возраста"
                        },
                        new
                        {
                            Id = new Guid("359ee017-8b18-4575-ad81-be752f3647fb"),
                            Cost = 2949,
                            Description = "Полезная и вкусная еда, приготовленная с учётом всех физиологических потребностей собак разных пород.",
                            Image = "/images/products/7786e83a-5fdb-4e78-babf-bb5c4387f933.jpg",
                            InWishList = false,
                            Name = "Pedigree - сухой корм для собак любого возраста"
                        },
                        new
                        {
                            Id = new Guid("d1668211-b3fb-4b05-9cfc-338aabcaaa05"),
                            Cost = 2149,
                            Description = "Корм Purina ONE разработан специально для стерилизованных кошек и кастрированных котов.",
                            Image = "/images/products/0cc9fe76-f2a0-4c9f-b1b0-15c5b434aace.jpg",
                            InWishList = false,
                            Name = "Purina One - сухой корм для стерилизованных кошек"
                        },
                        new
                        {
                            Id = new Guid("5172be97-87a1-4ce3-bb80-20944c1182f5"),
                            Cost = 39,
                            Description = "Корм для кошек любого возраста. 85г телятина-язык в соусе.",
                            Image = "/images/products/d53362e0-945a-469a-a072-ef11a2522db7.jpg",
                            InWishList = false,
                            Name = "Sheba - влажный корм для кошек любого возраста"
                        });
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Promocode", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Discount")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Promocodes");

                    b.HasData(
                        new
                        {
                            Id = new Guid("2fb64628-eff5-4673-b39d-9d59ea557665"),
                            Discount = 10,
                            Text = "SKIDKA10"
                        },
                        new
                        {
                            Id = new Guid("ddf02e6e-ca02-4b4e-b23a-8581c0fa6d9a"),
                            Discount = 20,
                            Text = "SKIDKA20"
                        },
                        new
                        {
                            Id = new Guid("be60ec3e-e063-4401-b712-0bf76797edbd"),
                            Discount = 23,
                            Text = "NEW2023YEAR"
                        },
                        new
                        {
                            Id = new Guid("d573e8e9-82af-416c-b1d7-551a62988a20"),
                            Discount = 50,
                            Text = "FIRSTORDER"
                        });
                });

            modelBuilder.Entity("OnlineShop.Db.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.UserDeliveryInfo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("PromocodeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Tel")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("PromocodeId");

                    b.ToTable("UserDeliveryInfo");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.WishItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("WishListId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ProductId");

                    b.HasIndex("WishListId");

                    b.ToTable("WishItem");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.WishList", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("WishLists");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.CartItem", b =>
                {
                    b.HasOne("OnlineShop.Db.Models.Cart", null)
                        .WithMany("Items")
                        .HasForeignKey("CartId");

                    b.HasOne("OnlineShop.Db.Models.Order", null)
                        .WithMany("CartItems")
                        .HasForeignKey("OrderId");

                    b.HasOne("OnlineShop.Db.Models.Product", "Product")
                        .WithMany("CartItems")
                        .HasForeignKey("ProductId");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Comment", b =>
                {
                    b.HasOne("OnlineShop.Db.Models.Product", "Product")
                        .WithMany("Comments")
                        .HasForeignKey("ProductId");

                    b.HasOne("OnlineShop.Db.Models.User", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId");

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Order", b =>
                {
                    b.HasOne("OnlineShop.Db.Models.UserDeliveryInfo", "UserInfo")
                        .WithMany()
                        .HasForeignKey("UserInfoId");

                    b.Navigation("UserInfo");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.UserDeliveryInfo", b =>
                {
                    b.HasOne("OnlineShop.Db.Models.Promocode", "Promocode")
                        .WithMany()
                        .HasForeignKey("PromocodeId");

                    b.Navigation("Promocode");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.WishItem", b =>
                {
                    b.HasOne("OnlineShop.Db.Models.Product", "Product")
                        .WithMany("WishItems")
                        .HasForeignKey("ProductId");

                    b.HasOne("OnlineShop.Db.Models.WishList", "WishList")
                        .WithMany("Items")
                        .HasForeignKey("WishListId");

                    b.Navigation("Product");

                    b.Navigation("WishList");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Cart", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Order", b =>
                {
                    b.Navigation("CartItems");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.Product", b =>
                {
                    b.Navigation("CartItems");

                    b.Navigation("Comments");

                    b.Navigation("WishItems");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.User", b =>
                {
                    b.Navigation("Comments");
                });

            modelBuilder.Entity("OnlineShop.Db.Models.WishList", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}