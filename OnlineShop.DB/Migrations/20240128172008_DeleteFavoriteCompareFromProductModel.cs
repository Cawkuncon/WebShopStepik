using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.DB.Migrations
{
    public partial class DeleteFavoriteCompareFromProductModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "Comparsion",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Favorite",
                table: "Products");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Cost", "Description", "Name" },
                values: new object[] { new Guid("a645ec7c-6c49-45c3-adc7-6f460a06c064"), 11, "First Product", "First" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Cost", "Description", "Name" },
                values: new object[] { new Guid("e6d080f3-dd6c-4d76-adfe-9db4d453c168"), 22, "Second Product", "Second" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Cost", "Description", "Name" },
                values: new object[] { new Guid("fd81a556-81d9-4130-b7a4-04aacfa4fae0"), 3, "Third Product", "Third" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("a645ec7c-6c49-45c3-adc7-6f460a06c064"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("e6d080f3-dd6c-4d76-adfe-9db4d453c168"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("fd81a556-81d9-4130-b7a4-04aacfa4fae0"));

            migrationBuilder.AddColumn<bool>(
                name: "Comparsion",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Favorite",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

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
    }
}
