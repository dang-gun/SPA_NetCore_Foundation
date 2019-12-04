using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SPA_NetCore_Foundation06.Migrations
{
    public partial class UserSignIn생성 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserSignIn",
                columns: table => new
                {
                    idUserSignIn = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UseridUser = table.Column<long>(nullable: true),
                    RefreshToken = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSignIn", x => x.idUserSignIn);
                    table.ForeignKey(
                        name: "FK_UserSignIn_User_UseridUser",
                        column: x => x.UseridUser,
                        principalTable: "User",
                        principalColumn: "idUser",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "idUser",
                keyValue: 1L,
                column: "SignEmail",
                value: "test01@email.net");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "idUser",
                keyValue: 2L,
                column: "SignEmail",
                value: "test02@email.net");

            migrationBuilder.CreateIndex(
                name: "IX_UserSignIn_UseridUser",
                table: "UserSignIn",
                column: "UseridUser");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSignIn");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "idUser",
                keyValue: 1L,
                column: "SignEmail",
                value: "test01@test.com");

            migrationBuilder.UpdateData(
                table: "User",
                keyColumn: "idUser",
                keyValue: 2L,
                column: "SignEmail",
                value: "test02@test.com");
        }
    }
}
