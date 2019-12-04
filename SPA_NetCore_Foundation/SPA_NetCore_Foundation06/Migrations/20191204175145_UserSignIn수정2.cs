using Microsoft.EntityFrameworkCore.Migrations;

namespace SPA_NetCore_Foundation06.Migrations
{
    public partial class UserSignIn수정2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSignIn_User_idUser1",
                table: "UserSignIn");

            migrationBuilder.RenameColumn(
                name: "idUser1",
                table: "UserSignIn",
                newName: "idUserForeignKey");

            migrationBuilder.RenameIndex(
                name: "IX_UserSignIn_idUser1",
                table: "UserSignIn",
                newName: "IX_UserSignIn_idUserForeignKey");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSignIn_User_idUserForeignKey",
                table: "UserSignIn",
                column: "idUserForeignKey",
                principalTable: "User",
                principalColumn: "idUser",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSignIn_User_idUserForeignKey",
                table: "UserSignIn");

            migrationBuilder.RenameColumn(
                name: "idUserForeignKey",
                table: "UserSignIn",
                newName: "idUser1");

            migrationBuilder.RenameIndex(
                name: "IX_UserSignIn_idUserForeignKey",
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
    }
}
