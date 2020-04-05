using Microsoft.EntityFrameworkCore.Migrations;

namespace MemeSite.Migrations
{
    public partial class upd01042020002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Txt",
                table: "Comments",
                maxLength: 2000,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000);

            migrationBuilder.AddColumn<bool>(
                name: "IsArchived",
                table: "Comments",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "TxtForHtml",
                table: "Comments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsArchived",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "TxtForHtml",
                table: "Comments");

            migrationBuilder.AlterColumn<string>(
                name: "Txt",
                table: "Comments",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                oldClrType: typeof(string),
                oldMaxLength: 2000);
        }
    }
}
