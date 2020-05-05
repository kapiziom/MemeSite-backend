using Microsoft.EntityFrameworkCore.Migrations;

namespace MemeSite.Data.Migrations
{
    public partial class Upd55202018 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Categories_CategoryName",
                table: "Categories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddUniqueConstraint(
                name: "AK_Categories_CategoryName",
                table: "Categories",
                column: "CategoryName");
        }
    }
}
