using Microsoft.EntityFrameworkCore.Migrations;

namespace MemeSite.Migrations
{
    public partial class upd01042020003 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TxtForHtml",
                table: "Comments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TxtForHtml",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
