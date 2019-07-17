using Microsoft.EntityFrameworkCore.Migrations;

namespace AnagramSolver.EF.CodeFirst.Migrations
{
    public partial class changeCachedWordForeignKeyToOptional : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CachedWords_DictionaryWords_DictionaryWordId",
                table: "CachedWords");

            migrationBuilder.AlterColumn<int>(
                name: "DictionaryWordId",
                table: "CachedWords",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_CachedWords_DictionaryWords_DictionaryWordId",
                table: "CachedWords",
                column: "DictionaryWordId",
                principalTable: "DictionaryWords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CachedWords_DictionaryWords_DictionaryWordId",
                table: "CachedWords");

            migrationBuilder.AlterColumn<int>(
                name: "DictionaryWordId",
                table: "CachedWords",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CachedWords_DictionaryWords_DictionaryWordId",
                table: "CachedWords",
                column: "DictionaryWordId",
                principalTable: "DictionaryWords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
