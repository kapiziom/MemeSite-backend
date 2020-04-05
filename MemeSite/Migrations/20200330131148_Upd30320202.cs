﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace MemeSite.Migrations
{
    public partial class Upd30320202 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Memes_Categories_CategoryId",
                table: "Memes");

            migrationBuilder.DropIndex(
                name: "IX_Memes_CategoryId",
                table: "Memes");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Memes",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Memes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Memes_CategoryId",
                table: "Memes",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Memes_Categories_CategoryId",
                table: "Memes",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}