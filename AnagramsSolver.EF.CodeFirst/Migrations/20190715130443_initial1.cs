using Microsoft.EntityFrameworkCore.Migrations;

namespace AnagramSolver.EF.CodeFirst.Migrations
{
    public partial class initial1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CachedWords_Words_ResponseWordNavigationId",
                table: "CachedWords");

            migrationBuilder.RenameColumn(
                name: "ResponseWordNavigationId",
                table: "CachedWords",
                newName: "ResponseWordId");

            migrationBuilder.RenameIndex(
                name: "IX_CachedWords_ResponseWordNavigationId",
                table: "CachedWords",
                newName: "IX_CachedWords_ResponseWordId");

            migrationBuilder.AddForeignKey(
                name: "FK_CachedWords_Words_ResponseWordId",
                table: "CachedWords",
                column: "ResponseWordId",
                principalTable: "Words",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CachedWords_Words_ResponseWordId",
                table: "CachedWords");

            migrationBuilder.RenameColumn(
                name: "ResponseWordId",
                table: "CachedWords",
                newName: "ResponseWordNavigationId");

            migrationBuilder.RenameIndex(
                name: "IX_CachedWords_ResponseWordId",
                table: "CachedWords",
                newName: "IX_CachedWords_ResponseWordNavigationId");

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
