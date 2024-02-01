using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.DB.Migrations
{
    public partial class FirstProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Comparsion", "Cost", "Description", "Favorite", "Name" },
                values: new object[] { new Guid("21d8ec4d-2095-4be4-a600-077c79811d77"), false, 22, "Second Product", false, "Second" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Comparsion", "Cost", "Description", "Favorite", "Name" },
                values: new object[] { new Guid("d19d4709-701e-4055-b8e3-d4bad3aa29f0"), false, 3, "Third Product", false, "Third" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Comparsion", "Cost", "Description", "Favorite", "Name" },
                values: new object[] { new Guid("fe769ffb-d471-4fc5-83b5-e71b56c7deec"), false, 11, "First Product", false, "First" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("21d8ec4d-2095-4be4-a600-077c79811d77"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d19d4709-701e-4055-b8e3-d4bad3aa29f0"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("fe769ffb-d471-4fc5-83b5-e71b56c7deec"));
        }
    }
}
