using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MemeSite.Data.Migrations
{
    public partial class AddFavDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateFavDate",
                table: "Favourites",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateFavDate",
                table: "Favourites");
        }
    }
}
