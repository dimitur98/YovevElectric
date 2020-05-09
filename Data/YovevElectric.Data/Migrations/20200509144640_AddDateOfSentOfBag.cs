using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YovevElectric.Data.Migrations
{
    public partial class AddDateOfSentOfBag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfSent",
                table: "Bags",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfSent",
                table: "Bags");
        }
    }
}
