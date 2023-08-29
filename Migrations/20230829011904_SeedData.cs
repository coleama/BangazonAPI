using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BangazonAPI.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "payment_types");

            migrationBuilder.DropTable(
                name: "product_types");

            migrationBuilder.RenameColumn(
                name: "ProductTypeId",
                table: "Products",
                newName: "ProductType");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "users",
                type: "integer",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SellerId",
                table: "Products",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CustomerId", "DatePlaced", "DateShipped", "OrderStatus", "PaymentType" },
                values: new object[,]
                {
                    { 1, "1", new DateTime(2023, 8, 28, 20, 19, 4, 243, DateTimeKind.Local).AddTicks(3273), new DateTime(2023, 8, 28, 20, 19, 4, 243, DateTimeKind.Local).AddTicks(3296), 1, null },
                    { 2, "2", new DateTime(2023, 8, 28, 20, 19, 4, 243, DateTimeKind.Local).AddTicks(3298), new DateTime(2023, 8, 28, 20, 19, 4, 243, DateTimeKind.Local).AddTicks(3299), 2, null }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "DateAdded", "Description", "ImageUrl", "InStock", "Price", "ProductType", "SellerId" },
                values: new object[] { 1, new DateTime(2023, 8, 28, 20, 19, 4, 243, DateTimeKind.Local).AddTicks(3313), "This is a  Electronic Product", "this is url", true, 19.99m, 1, "1" });

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "Address", "Email", "FirstName", "IsSeller", "LastName", "OrderId" },
                values: new object[,]
                {
                    { "1", "123 Test Road", "Email@email.com", "Test", true, "Testerson", null },
                    { "2", "123 Not Test Road", "email@notEmail.com", "Bob", false, "Bobberson", null }
                });

            migrationBuilder.InsertData(
                table: "ordered_Products",
                columns: new[] { "OrderId", "ProductId", "Id" },
                values: new object[] { 1, 1, 0 });

            migrationBuilder.CreateIndex(
                name: "IX_users_OrderId",
                table: "users",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_users_Orders_OrderId",
                table: "users",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_users_Orders_OrderId",
                table: "users");

            migrationBuilder.DropIndex(
                name: "IX_users_OrderId",
                table: "users");

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ordered_Products",
                keyColumns: new[] { "OrderId", "ProductId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "users");

            migrationBuilder.RenameColumn(
                name: "ProductType",
                table: "Products",
                newName: "ProductTypeId");

            migrationBuilder.AlterColumn<int>(
                name: "SellerId",
                table: "Products",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateTable(
                name: "payment_types",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_payment_types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "product_types",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProductId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_types", x => x.Id);
                    table.ForeignKey(
                        name: "FK_product_types_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_product_types_ProductId",
                table: "product_types",
                column: "ProductId");
        }
    }
}
