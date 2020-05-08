using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YovevElectric.Data.Migrations
{
    public partial class ProductsQunatities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Bags_BagId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_BagId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "BagId",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "ProductsQuantities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    ProductId = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    BagId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsQuantities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductsQuantities_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductsQuantities_Bags_BagId",
                        column: x => x.BagId,
                        principalTable: "Bags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductsQuantities_IsDeleted",
                table: "ProductsQuantities",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsQuantities_ProductId",
                table: "ProductsQuantities",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsQuantities_BagId",
                table: "ProductsQuantities",
                column: "BagId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductsQuantities");

            migrationBuilder.AddColumn<string>(
                name: "BagId",
                table: "Products",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Products_BagId",
                table: "Products",
                column: "BagId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Bags_BagId",
                table: "Products",
                column: "BagId",
                principalTable: "Bags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
