using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.DB.Migrations
{
    public partial class dbch : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("37201232-b2fc-467f-8f2b-036a69f64c70"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("587360d0-c648-418d-89f2-7f0075ffc5ca"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("e204ac00-f61b-43b8-b59e-0d083b13d334"));

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Cost", "Description", "Name", "imageProdPath" },
                values: new object[] { new Guid("354b81c4-1335-4f28-a05b-0ea3dec35587"), 3, "Third Product", "Third", null });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Cost", "Description", "Name", "imageProdPath" },
                values: new object[] { new Guid("382aea64-7c45-4f65-809d-9e4befe0f5b8"), 11, "First Product", "First", null });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Cost", "Description", "Name", "imageProdPath" },
                values: new object[] { new Guid("9e8f4f9f-4aac-4457-bb9a-d38e0f92cf4e"), 22, "Second Product", "Second", null });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("354b81c4-1335-4f28-a05b-0ea3dec35587"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("382aea64-7c45-4f65-809d-9e4befe0f5b8"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("9e8f4f9f-4aac-4457-bb9a-d38e0f92cf4e"));

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Cost", "Description", "Name", "imageProdPath" },
                values: new object[] { new Guid("37201232-b2fc-467f-8f2b-036a69f64c70"), 11, "First Product", "First", null });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Cost", "Description", "Name", "imageProdPath" },
                values: new object[] { new Guid("587360d0-c648-418d-89f2-7f0075ffc5ca"), 22, "Second Product", "Second", null });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Cost", "Description", "Name", "imageProdPath" },
                values: new object[] { new Guid("e204ac00-f61b-43b8-b59e-0d083b13d334"), 3, "Third Product", "Third", null });
        }
    }
}
