using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MOJ.ProductManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_lastOrderDate_col : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastOrderDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductId",
                table: "Products",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Products_ProductId",
                table: "Products",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Products_ProductId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "LastOrderDate",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "Products");
        }
    }
}
