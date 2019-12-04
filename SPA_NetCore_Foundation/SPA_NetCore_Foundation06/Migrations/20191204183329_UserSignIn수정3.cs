using Microsoft.EntityFrameworkCore.Migrations;

namespace SPA_NetCore_Foundation06.Migrations
{
    public partial class UserSignIn수정3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSignIn_User_idUserForeignKey",
                table: "UserSignIn");

            migrationBuilder.RenameColumn(
                name: "idUserForeignKey",
                table: "UserSignIn",
                newName: "idUser");

            migrationBuilder.RenameIndex(
                name: "IX_UserSignIn_idUserForeignKey",
                table: "UserSignIn",
                newName: "IX_UserSignIn_idUser");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSignIn_User_idUser",
                table: "UserSignIn",
                column: "idUser",
                principalTable: "User",
                principalColumn: "idUser",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSignIn_User_idUser",
                table: "UserSignIn");

            migrationBuilder.RenameColumn(
                name: "idUser",
                table: "UserSignIn",
                newName: "idUserForeignKey");

            migrationBuilder.RenameIndex(
                name: "IX_UserSignIn_idUser",
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
    }
}
