using Microsoft.EntityFrameworkCore.Migrations;

namespace YovevElectric.Data.Migrations
{
    public partial class AddImgForCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImgPath",
                table: "Categories",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgPath",
                table: "Categories");
        }
    }
}
