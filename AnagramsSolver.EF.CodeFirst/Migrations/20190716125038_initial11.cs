using Microsoft.EntityFrameworkCore.Migrations;

namespace AnagramSolver.EF.CodeFirst.Migrations
{
    public partial class initial11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RequestWords_UserLogs_UserLogId",
                table: "RequestWords");

            migrationBuilder.DropIndex(
                name: "IX_RequestWords_UserLogId",
                table: "RequestWords");

            migrationBuilder.DropColumn(
                name: "UserLogId",
                table: "RequestWords");

            migrationBuilder.AlterColumn<int>(
                name: "RequestWordId",
                table: "UserLogs",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserLogs_RequestWordId",
                table: "UserLogs",
                column: "RequestWordId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogs_RequestWords_RequestWordId",
                table: "UserLogs",
                column: "RequestWordId",
                principalTable: "RequestWords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLogs_RequestWords_RequestWordId",
                table: "UserLogs");

            migrationBuilder.DropIndex(
                name: "IX_UserLogs_RequestWordId",
                table: "UserLogs");

            migrationBuilder.AlterColumn<string>(
                name: "RequestWordId",
                table: "UserLogs",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "UserLogId",
                table: "RequestWords",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_RequestWords_UserLogId",
                table: "RequestWords",
                column: "UserLogId");

            migrationBuilder.AddForeignKey(
                name: "FK_RequestWords_UserLogs_UserLogId",
                table: "RequestWords",
                column: "UserLogId",
                principalTable: "UserLogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
