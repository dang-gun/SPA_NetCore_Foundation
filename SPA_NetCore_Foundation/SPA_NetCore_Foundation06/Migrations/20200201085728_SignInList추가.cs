using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SPA_NetCore_Foundation06.Migrations
{
    public partial class SignInList추가 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshDate",
                table: "UserSignIn",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "SignInDate",
                table: "UserSignIn",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshDate",
                table: "UserSignIn");

            migrationBuilder.DropColumn(
                name: "SignInDate",
                table: "UserSignIn");
        }
    }
}
