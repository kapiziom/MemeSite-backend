using Microsoft.EntityFrameworkCore.Migrations;

namespace MemeSite.Migrations
{
    public partial class editmememodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Memes");

            migrationBuilder.AddColumn<string>(
                name: "ByteHead",
                table: "Memes",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ByteHead",
                table: "Memes");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Memes",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
