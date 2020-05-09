using Microsoft.EntityFrameworkCore.Migrations;

namespace YovevElectric.Data.Migrations
{
    public partial class AddIsNewInOrderData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsNew",
                table: "OrderData",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsNew",
                table: "OrderData");
        }
    }
}
