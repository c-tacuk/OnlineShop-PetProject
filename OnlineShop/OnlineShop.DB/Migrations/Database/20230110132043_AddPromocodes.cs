using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineShop.Db.Migrations.Database
{
    /// <inheritdoc />
    public partial class AddPromocodes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PromocodeId",
                table: "UserDeliveryInfo",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Promocodes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Discount = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Promocodes", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Promocodes",
                columns: new[] { "Id", "Discount", "Text" },
                values: new object[,]
                {
                    { new Guid("2fb64628-eff5-4673-b39d-9d59ea557665"), 10, "SKIDKA10" },
                    { new Guid("be60ec3e-e063-4401-b712-0bf76797edbd"), 23, "NEW2023YEAR" },
                    { new Guid("d573e8e9-82af-416c-b1d7-551a62988a20"), 50, "FIRSTORDER" },
                    { new Guid("ddf02e6e-ca02-4b4e-b23a-8581c0fa6d9a"), 20, "SKIDKA20" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserDeliveryInfo_PromocodeId",
                table: "UserDeliveryInfo",
                column: "PromocodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserDeliveryInfo_Promocodes_PromocodeId",
                table: "UserDeliveryInfo",
                column: "PromocodeId",
                principalTable: "Promocodes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserDeliveryInfo_Promocodes_PromocodeId",
                table: "UserDeliveryInfo");

            migrationBuilder.DropTable(
                name: "Promocodes");

            migrationBuilder.DropIndex(
                name: "IX_UserDeliveryInfo_PromocodeId",
                table: "UserDeliveryInfo");

            migrationBuilder.DropColumn(
                name: "PromocodeId",
                table: "UserDeliveryInfo");
        }
    }
}
