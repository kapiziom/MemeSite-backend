using Microsoft.EntityFrameworkCore.Migrations;

namespace MemeSite.Migrations
{
    public partial class upd01042020001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsArchivized",
                table: "Memes");

            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "Memes",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "Memes");

            migrationBuilder.AddColumn<bool>(
                name: "IsArchivized",
                table: "Memes",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
