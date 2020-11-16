using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SPA_NetCore_Foundation07.Migrations
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Initialize : Migration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    idUser = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SignEmail = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.idUser);
                });

            migrationBuilder.CreateTable(
                name: "UserInfo",
                columns: table => new
                {
                    idUserInfo = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    idUser = table.Column<long>(nullable: false),
                    ViewName = table.Column<string>(maxLength: 16, nullable: true),
                    SignUpDate = table.Column<DateTime>(nullable: false),
                    SignInDate = table.Column<DateTime>(nullable: false),
                    RefreshDate = table.Column<DateTime>(nullable: false),
                    PlatformInfo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfo", x => x.idUserInfo);
                });

            migrationBuilder.CreateTable(
                name: "UserSignIn",
                columns: table => new
                {
                    idUserSignIn = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    idUser = table.Column<long>(nullable: false),
                    RefreshToken = table.Column<string>(nullable: true),
                    SignInDate = table.Column<DateTime>(nullable: false),
                    RefreshDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSignIn", x => x.idUserSignIn);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "idUser", "Password", "SignEmail" },
                values: new object[] { 1L, "1111", "test01@email.net" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "idUser", "Password", "SignEmail" },
                values: new object[] { 2L, "1111", "test02@email.net" });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "UserInfo");

            migrationBuilder.DropTable(
                name: "UserSignIn");
        }
    }
}
