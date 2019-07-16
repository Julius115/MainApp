using Microsoft.EntityFrameworkCore.Migrations;

namespace AnagramSolver.EF.CodeFirst.Migrations
{
    public partial class FluentApiUserLogToCachedWords : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "RequestWord",
                table: "UserLogs",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RequestWord",
                table: "CachedWords",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_UserLogs_RequestWord",
                table: "UserLogs",
                column: "RequestWord");

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CachedWords_UserLogs_RequestWord",
                table: "CachedWords");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_UserLogs_RequestWord",
                table: "UserLogs");

            migrationBuilder.DropIndex(
                name: "IX_CachedWords_RequestWord",
                table: "CachedWords");

            migrationBuilder.AlterColumn<string>(
                name: "RequestWord",
                table: "UserLogs",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "RequestWord",
                table: "CachedWords",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
