using Microsoft.EntityFrameworkCore.Migrations;

namespace MemeSite.Migrations
{
    public partial class Update240420 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CommentId1",
                table: "Comments",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Favourites",
                columns: table => new
                {
                    FavId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    MemeRefId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favourites", x => x.FavId);
                    table.ForeignKey(
                        name: "FK_Favourites_Memes_MemeRefId",
                        column: x => x.MemeRefId,
                        principalTable: "Memes",
                        principalColumn: "MemeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favourites_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_CommentId1",
                table: "Comments",
                column: "CommentId1");

            migrationBuilder.CreateIndex(
                name: "IX_Favourites_MemeRefId",
                table: "Favourites",
                column: "MemeRefId");

            migrationBuilder.CreateIndex(
                name: "IX_Favourites_UserId",
                table: "Favourites",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Comments_CommentId1",
                table: "Comments",
                column: "CommentId1",
                principalTable: "Comments",
                principalColumn: "CommentId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Comments_CommentId1",
                table: "Comments");

            migrationBuilder.DropTable(
                name: "Favourites");

            migrationBuilder.DropIndex(
                name: "IX_Comments_CommentId1",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "CommentId1",
                table: "Comments");
        }
    }
}
