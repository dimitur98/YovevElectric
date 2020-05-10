using Microsoft.EntityFrameworkCore.Migrations;

namespace YovevElectric.Data.Migrations
{
    public partial class AddUserIdToBagAndProductQuantity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ProductsQuantities",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Bags",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ProductsQuantities");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Bags");
        }
    }
}
