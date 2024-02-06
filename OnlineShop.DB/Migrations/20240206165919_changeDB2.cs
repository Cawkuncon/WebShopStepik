using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.DB.Migrations
{
    public partial class changeDB2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("055b0c47-5eee-4892-ad70-a4791bad0c1b"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c3a84abe-f437-4a01-ab66-359f8297ef9e"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("cfe8fcaa-f30d-4840-8444-04071af860ad"));

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Cost", "Description", "Name" },
                values: new object[] { new Guid("2213852f-8972-4fe6-a30d-17c2c9daf57d"), 22, "Second Product", "Second" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Cost", "Description", "Name" },
                values: new object[] { new Guid("d65828f0-9150-4277-8130-710761c191ea"), 3, "Third Product", "Third" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Cost", "Description", "Name" },
                values: new object[] { new Guid("fa757dd0-e3a7-4805-9d2e-774f38dfc03d"), 11, "First Product", "First" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("2213852f-8972-4fe6-a30d-17c2c9daf57d"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("d65828f0-9150-4277-8130-710761c191ea"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("fa757dd0-e3a7-4805-9d2e-774f38dfc03d"));

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Cost", "Description", "Name" },
                values: new object[] { new Guid("055b0c47-5eee-4892-ad70-a4791bad0c1b"), 11, "First Product", "First" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Cost", "Description", "Name" },
                values: new object[] { new Guid("c3a84abe-f437-4a01-ab66-359f8297ef9e"), 22, "Second Product", "Second" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Cost", "Description", "Name" },
                values: new object[] { new Guid("cfe8fcaa-f30d-4840-8444-04071af860ad"), 3, "Third Product", "Third" });
        }
    }
}
