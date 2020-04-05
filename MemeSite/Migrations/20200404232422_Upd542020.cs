using Microsoft.EntityFrameworkCore.Migrations;

namespace MemeSite.Migrations
{
    public partial class Upd542020 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Memes_MemeId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_PageUserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Memes_MemeId",
                table: "Votes");

            migrationBuilder.DropIndex(
                name: "IX_Votes_MemeId",
                table: "Votes");

            migrationBuilder.DropIndex(
                name: "IX_Comments_MemeId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_PageUserId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "MemeId",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "MemeId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "PageUserId",
                table: "Comments");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Votes",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "Memes",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "Comments",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Votes_MemeRefId",
                table: "Votes",
                column: "MemeRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_UserId",
                table: "Votes",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Memes_CategoryId",
                table: "Memes",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Memes_UserID",
                table: "Memes",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_MemeRefId",
                table: "Comments",
                column: "MemeRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_UserID",
                table: "Comments",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Memes_MemeRefId",
                table: "Comments",
                column: "MemeRefId",
                principalTable: "Memes",
                principalColumn: "MemeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_UserID",
                table: "Comments",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Memes_Categories_CategoryId",
                table: "Memes",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Memes_AspNetUsers_UserID",
                table: "Memes",
                column: "UserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Memes_MemeRefId",
                table: "Votes",
                column: "MemeRefId",
                principalTable: "Memes",
                principalColumn: "MemeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_AspNetUsers_UserId",
                table: "Votes",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Memes_MemeRefId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_UserID",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Memes_Categories_CategoryId",
                table: "Memes");

            migrationBuilder.DropForeignKey(
                name: "FK_Memes_AspNetUsers_UserID",
                table: "Memes");

            migrationBuilder.DropForeignKey(
                name: "FK_Votes_Memes_MemeRefId",
                table: "Votes");

            migrationBuilder.DropForeignKey(
                name: "FK_Votes_AspNetUsers_UserId",
                table: "Votes");

            migrationBuilder.DropIndex(
                name: "IX_Votes_MemeRefId",
                table: "Votes");

            migrationBuilder.DropIndex(
                name: "IX_Votes_UserId",
                table: "Votes");

            migrationBuilder.DropIndex(
                name: "IX_Memes_CategoryId",
                table: "Memes");

            migrationBuilder.DropIndex(
                name: "IX_Memes_UserID",
                table: "Memes");

            migrationBuilder.DropIndex(
                name: "IX_Comments_MemeRefId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_UserID",
                table: "Comments");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Votes",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "MemeId",
                table: "Votes",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "Memes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserID",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MemeId",
                table: "Comments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PageUserId",
                table: "Comments",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Votes_MemeId",
                table: "Votes",
                column: "MemeId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_MemeId",
                table: "Comments",
                column: "MemeId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_PageUserId",
                table: "Comments",
                column: "PageUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Memes_MemeId",
                table: "Comments",
                column: "MemeId",
                principalTable: "Memes",
                principalColumn: "MemeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_PageUserId",
                table: "Comments",
                column: "PageUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Votes_Memes_MemeId",
                table: "Votes",
                column: "MemeId",
                principalTable: "Memes",
                principalColumn: "MemeId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
