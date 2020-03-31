using Microsoft.EntityFrameworkCore.Migrations;

namespace MemeSite.Migrations
{
    public partial class Upd0330320202 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Memes_MemeId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Memes_MemeId",
                table: "Votes");

            migrationBuilder.AlterColumn<int>(
                name: "MemeId",
                table: "Votes",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "MemeRefId",
                table: "Votes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "MemeId",
                table: "Comments",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "MemeRefId",
                table: "Comments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Memes_MemeId",
                table: "Comments",
                column: "MemeId",
                principalTable: "Memes",
                principalColumn: "MemeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Memes_MemeId",
                table: "Votes",
                column: "MemeId",
                principalTable: "Memes",
                principalColumn: "MemeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Memes_MemeId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Memes_MemeId",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "MemeRefId",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "MemeRefId",
                table: "Comments");

            migrationBuilder.AlterColumn<int>(
                name: "MemeId",
                table: "Votes",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MemeId",
                table: "Comments",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Memes_MemeId",
                table: "Comments",
                column: "MemeId",
                principalTable: "Memes",
                principalColumn: "MemeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Memes_MemeId",
                table: "Votes",
                column: "MemeId",
                principalTable: "Memes",
                principalColumn: "MemeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
