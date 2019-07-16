using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AnagramSolver.EF.CodeFirst.Migrations
{
    public partial class initia10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CachedWords_UserLogs_RequestWord",
                table: "CachedWords");

            migrationBuilder.DropForeignKey(
                name: "FK_CachedWords_Words_WordId",
                table: "CachedWords");

            migrationBuilder.DropTable(
                name: "Words");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_UserLogs_RequestWord",
                table: "UserLogs");

            migrationBuilder.DropIndex(
                name: "IX_CachedWords_RequestWord",
                table: "CachedWords");

            migrationBuilder.DropColumn(
                name: "RequestWord",
                table: "UserLogs");

            migrationBuilder.DropColumn(
                name: "RequestWord",
                table: "CachedWords");

            migrationBuilder.RenameColumn(
                name: "WordId",
                table: "CachedWords",
                newName: "RequestWordId");

            migrationBuilder.RenameIndex(
                name: "IX_CachedWords_WordId",
                table: "CachedWords",
                newName: "IX_CachedWords_RequestWordId");

            migrationBuilder.AddColumn<string>(
                name: "RequestWordId",
                table: "UserLogs",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DictionaryWordId",
                table: "CachedWords",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "DictionaryWords",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Word = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DictionaryWords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RequestWords",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Word = table.Column<string>(nullable: true),
                    UserLogId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestWords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestWords_UserLogs_UserLogId",
                        column: x => x.UserLogId,
                        principalTable: "UserLogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CachedWords_DictionaryWordId",
                table: "CachedWords",
                column: "DictionaryWordId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestWords_UserLogId",
                table: "RequestWords",
                column: "UserLogId");

            migrationBuilder.AddForeignKey(
                name: "FK_CachedWords_DictionaryWords_DictionaryWordId",
                table: "CachedWords",
                column: "DictionaryWordId",
                principalTable: "DictionaryWords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CachedWords_RequestWords_RequestWordId",
                table: "CachedWords",
                column: "RequestWordId",
                principalTable: "RequestWords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CachedWords_DictionaryWords_DictionaryWordId",
                table: "CachedWords");

            migrationBuilder.DropForeignKey(
                name: "FK_CachedWords_RequestWords_RequestWordId",
                table: "CachedWords");

            migrationBuilder.DropTable(
                name: "DictionaryWords");

            migrationBuilder.DropTable(
                name: "RequestWords");

            migrationBuilder.DropIndex(
                name: "IX_CachedWords_DictionaryWordId",
                table: "CachedWords");

            migrationBuilder.DropColumn(
                name: "RequestWordId",
                table: "UserLogs");

            migrationBuilder.DropColumn(
                name: "DictionaryWordId",
                table: "CachedWords");

            migrationBuilder.RenameColumn(
                name: "RequestWordId",
                table: "CachedWords",
                newName: "WordId");

            migrationBuilder.RenameIndex(
                name: "IX_CachedWords_RequestWordId",
                table: "CachedWords",
                newName: "IX_CachedWords_WordId");

            migrationBuilder.AddColumn<string>(
                name: "RequestWord",
                table: "UserLogs",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "RequestWord",
                table: "CachedWords",
                nullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_UserLogs_RequestWord",
                table: "UserLogs",
                column: "RequestWord");

            migrationBuilder.CreateTable(
                name: "Words",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    WordValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CachedWords_RequestWord",
                table: "CachedWords",
                column: "RequestWord");

            migrationBuilder.AddForeignKey(
                name: "FK_CachedWords_UserLogs_RequestWord",
                table: "CachedWords",
                column: "RequestWord",
                principalTable: "UserLogs",
                principalColumn: "RequestWord",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CachedWords_Words_WordId",
                table: "CachedWords",
                column: "WordId",
                principalTable: "Words",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
