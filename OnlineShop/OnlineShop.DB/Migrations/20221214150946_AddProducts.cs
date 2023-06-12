using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineShop.Db.Migrations
{
    /// <inheritdoc />
    public partial class AddProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Cost", "Description", "Image", "InWishList", "Name" },
                values: new object[,]
                {
                    { new Guid("05170ab1-6d83-408a-8b0e-c7c021de2b18"), 589, "All Dogs - это полнорационный корм для собак всех пород.", "https://cdn1.ozone.ru/s3/multimedia-6/6235794426.jpg", false, "All Dogs - сухой корм для взрослых собак" },
                    { new Guid("291c1c5c-7d2e-451e-8e71-16c373cb9da5"), 449, "Представляем Felix Двойная Вкуснятина – новый сухой корм для кошек, который полностью соответствует потребностям кошки.", "https://vetrost.ru/wp-content/uploads/2021/01/e5c84d48-4a8c-11eb-80eb-ac1f6bda0b3d_e5c84d52-4a8c-11eb-80eb-ac1f6bda0b3d.jpeg", false, "Felix - сухой корм для кошек любого возраста" },
                    { new Guid("2f99df3f-5ee8-47ad-a17a-9a048138e2f6"), 599, "Полнорационный сухой корм Chappi. Сытный мясной обед.", "https://domingo.su/images/main/BIG/000-470-414.jpg", false, "Chappi - сухой корм для взрослых собак" },
                    { new Guid("359ee017-8b18-4575-ad81-be752f3647fb"), 2949, "Полезная и вкусная еда, приготовленная с учётом всех физиологических потребностей собак разных пород.", "https://www.mirkorma.ru/upload/iblock/1df/1df2b954d3626b1d7376a9d2b83f3257.jpg", false, "Pedigree - сухой корм для собак любого возраста" },
                    { new Guid("5172be97-87a1-4ce3-bb80-20944c1182f5"), 39, "Корм для кошек любого возраста. 85г телятина-язык в соусе.", "https://tornado.shop/images/detailed/2/P1012615__2_.jpg", false, "Sheba - влажный корм для кошек любого возраста" },
                    { new Guid("bcc1db7c-af36-4c86-bc39-4eaa27d0b01e"), 419, "All Dogs - это полнорационный корм для собак всех пород.", "https://zoopark-shop.ru/images/product_images/info_images/z731039.jpg", false, "Eukanua - сухой корм для котят до одного года" },
                    { new Guid("d1668211-b3fb-4b05-9cfc-338aabcaaa05"), 2149, "Корм Purina ONE разработан специально для стерилизованных кошек и кастрированных котов.", "https://zooex.ru/upload/iblock/dcb/dcbca2b019d94f93d2595a82dc1acc39.jpg", false, "Purina One - сухой корм для стерилизованных кошек" },
                    { new Guid("e637fdb0-70f4-40a3-ab47-050e6cfb8dd1"), 499, "Cesar - влажный корм с натуральными ингридиентами для щенят. Без консервантов.", "https://posyltorg33.ru/upload/iblock/32a/32a6aed4c046aac0459c64cd5d19f5ea.jpg", false, "Cesar - влажный корм для щенят до двух лет" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("05170ab1-6d83-408a-8b0e-c7c021de2b18"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("291c1c5c-7d2e-451e-8e71-16c373cb9da5"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("2f99df3f-5ee8-47ad-a17a-9a048138e2f6"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("359ee017-8b18-4575-ad81-be752f3647fb"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("5172be97-87a1-4ce3-bb80-20944c1182f5"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("bcc1db7c-af36-4c86-bc39-4eaa27d0b01e"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d1668211-b3fb-4b05-9cfc-338aabcaaa05"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("e637fdb0-70f4-40a3-ab47-050e6cfb8dd1"));
        }
    }
}
