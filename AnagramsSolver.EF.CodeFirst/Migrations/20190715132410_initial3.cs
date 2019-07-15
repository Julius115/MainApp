using Microsoft.EntityFrameworkCore.Migrations;

namespace AnagramSolver.EF.CodeFirst.Migrations
{
    public partial class initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CachedWords_Words_ResponseWordNavigationId",
                table: "CachedWords");

            migrationBuilder.DropIndex(
                name: "IX_CachedWords_ResponseWordNavigationId",
                table: "CachedWords");

            migrationBuilder.DropColumn(
                name: "ResponseWordNavigationId",
                table: "CachedWords");

            migrationBuilder.RenameColumn(
                name: "ResponseWord",
                table: "CachedWords",
                newName: "WordId");

            migrationBuilder.CreateIndex(
                name: "IX_CachedWords_WordId",
                table: "CachedWords",
                column: "WordId");

            migrationBuilder.AddForeignKey(
                name: "FK_CachedWords_Words_WordId",
                table: "CachedWords",
                column: "WordId",
                principalTable: "Words",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CachedWords_Words_WordId",
                table: "CachedWords");

            migrationBuilder.DropIndex(
                name: "IX_CachedWords_WordId",
                table: "CachedWords");

            migrationBuilder.RenameColumn(
                name: "WordId",
                table: "CachedWords",
                newName: "ResponseWord");

            migrationBuilder.AddColumn<int>(
                name: "ResponseWordNavigationId",
                table: "CachedWords",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CachedWords_ResponseWordNavigationId",
                table: "CachedWords",
                column: "ResponseWordNavigationId");

            migrationBuilder.AddForeignKey(
                name: "FK_CachedWords_Words_ResponseWordNavigationId",
                table: "CachedWords",
                column: "ResponseWordNavigationId",
                principalTable: "Words",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
