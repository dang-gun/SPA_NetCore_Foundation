using Microsoft.EntityFrameworkCore.Migrations;

namespace SPA_NetCore_Foundation06.Migrations
{
    public partial class UserSignIn수정4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserSignIn_User_idUser",
                table: "UserSignIn");

            migrationBuilder.DropIndex(
                name: "IX_UserSignIn_idUser",
                table: "UserSignIn");

            migrationBuilder.AlterColumn<long>(
                name: "idUser",
                table: "UserSignIn",
                nullable: false,
                oldClrType: typeof(long),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "idUser",
                table: "UserSignIn",
                nullable: true,
                oldClrType: typeof(long));

            migrationBuilder.CreateIndex(
                name: "IX_UserSignIn_idUser",
                table: "UserSignIn",
                column: "idUser");

            migrationBuilder.AddForeignKey(
                name: "FK_UserSignIn_User_idUser",
                table: "UserSignIn",
                column: "idUser",
                principalTable: "User",
                principalColumn: "idUser",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
