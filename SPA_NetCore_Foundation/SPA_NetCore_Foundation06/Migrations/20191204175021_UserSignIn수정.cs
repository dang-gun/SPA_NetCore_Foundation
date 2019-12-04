using Microsoft.EntityFrameworkCore.Migrations;

namespace SPA_NetCore_Foundation06.Migrations
{
    public partial class UserSignIn수정 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSignIn_User_UseridUser",
                table: "UserSignIn");

            migrationBuilder.RenameColumn(
                name: "UseridUser",
                table: "UserSignIn",
                newName: "idUser1");

            migrationBuilder.RenameIndex(
                name: "IX_UserSignIn_UseridUser",
                table: "UserSignIn",
                newName: "IX_UserSignIn_idUser1");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSignIn_User_idUser1",
                table: "UserSignIn",
                column: "idUser1",
                principalTable: "User",
                principalColumn: "idUser",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSignIn_User_idUser1",
                table: "UserSignIn");

            migrationBuilder.RenameColumn(
                name: "idUser1",
                table: "UserSignIn",
                newName: "UseridUser");

            migrationBuilder.RenameIndex(
                name: "IX_UserSignIn_idUser1",
                table: "UserSignIn",
                newName: "IX_UserSignIn_UseridUser");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSignIn_User_UseridUser",
                table: "UserSignIn",
                column: "UseridUser",
                principalTable: "User",
                principalColumn: "idUser",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
