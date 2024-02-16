using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineShop.DB.Migrations
{
    public partial class addImagePathToProduct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("32fc2acf-cd7d-44f8-8891-da6977feae29"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("c240873f-da13-486d-86ce-811455b9fe1e"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("cf129db8-1af5-4206-9274-bd569468f22d"));

            migrationBuilder.AddColumn<string>(
                name: "imageProdPath",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "imageProdPath",
                table: "Products");

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Cost", "Description", "Name" },
                values: new object[] { new Guid("32fc2acf-cd7d-44f8-8891-da6977feae29"), 22, "Second Product", "Second" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Cost", "Description", "Name" },
                values: new object[] { new Guid("c240873f-da13-486d-86ce-811455b9fe1e"), 3, "Third Product", "Third" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Cost", "Description", "Name" },
                values: new object[] { new Guid("cf129db8-1af5-4206-9274-bd569468f22d"), 11, "First Product", "First" });
        }
    }
}
