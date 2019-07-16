﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace AnagramSolver.EF.CodeFirst.Migrations
{
    public partial class initial5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_CachedWords_RequestWord",
                table: "CachedWords",
                column: "RequestWord");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogs_RequestWord",
                table: "UserLogs",
                column: "RequestWord");

            migrationBuilder.AddForeignKey(
                name: "FK_UserLogs_CachedWords_RequestWord",
                table: "UserLogs",
                column: "RequestWord",
                principalTable: "CachedWords",
                principalColumn: "RequestWord",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserLogs_CachedWords_RequestWord",
                table: "UserLogs");

            migrationBuilder.DropIndex(
                name: "IX_UserLogs_RequestWord",
                table: "UserLogs");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_CachedWords_RequestWord",
                table: "CachedWords");

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
                oldClrType: typeof(string));

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
    }
}
