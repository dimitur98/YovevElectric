using Microsoft.EntityFrameworkCore.Migrations;

namespace YovevElectric.Data.Migrations
{
    public partial class AddBagToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BagId",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_BagId",
                table: "AspNetUsers",
                column: "BagId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Bags_BagId",
                table: "AspNetUsers",
                column: "BagId",
                principalTable: "Bags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Bags_BagId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_BagId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BagId",
                table: "AspNetUsers");
        }
    }
}
