using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AnagramSolver.EF.CodeFirst.Migrations
{
    public partial class AddUserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserIp",
                table: "UserLogs");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UserLogs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserIp = table.Column<string>(nullable: true),
                    SearchesLeft = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserLogs_UserId",
                table: "UserLogs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogs_Users_UserId",
                table: "UserLogs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLogs_Users_UserId",
                table: "UserLogs");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_UserLogs_UserId",
                table: "UserLogs");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserLogs");

            migrationBuilder.AddColumn<string>(
                name: "UserIp",
                table: "UserLogs",
                nullable: true);
        }
    }
}
