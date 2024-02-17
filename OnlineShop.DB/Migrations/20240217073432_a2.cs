using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.DB.Migrations
{
    public partial class a2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("196f0df6-b888-4a15-91c0-32629fd27b1d"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("6b2e74b1-6da2-441b-b13d-a244ec05f2d5"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("accde501-bb84-42d6-952e-d7dbe77545b1"));

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Cost", "Description", "Name" },
                values: new object[] { new Guid("302674e3-7f5e-4262-9335-a0ba8f2bdd22"), 3, "Third Product", "Third" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Cost", "Description", "Name" },
                values: new object[] { new Guid("6aaa1464-93e6-47cf-9bc6-7b46992be537"), 22, "Second Product", "Second" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Cost", "Description", "Name" },
                values: new object[] { new Guid("72c495d4-3e62-4db2-bcf4-eab06f9b86cd"), 11, "First Product", "First" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("302674e3-7f5e-4262-9335-a0ba8f2bdd22"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("6aaa1464-93e6-47cf-9bc6-7b46992be537"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("72c495d4-3e62-4db2-bcf4-eab06f9b86cd"));

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Cost", "Description", "Name" },
                values: new object[] { new Guid("196f0df6-b888-4a15-91c0-32629fd27b1d"), 22, "Second Product", "Second" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Cost", "Description", "Name" },
                values: new object[] { new Guid("6b2e74b1-6da2-441b-b13d-a244ec05f2d5"), 11, "First Product", "First" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Cost", "Description", "Name" },
                values: new object[] { new Guid("accde501-bb84-42d6-952e-d7dbe77545b1"), 3, "Third Product", "Third" });
        }
    }
}
