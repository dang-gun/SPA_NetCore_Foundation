using Microsoft.EntityFrameworkCore.Migrations;

namespace SPA_NetCore_Foundation06.Migrations
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UserInfo_Add : Migration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserInfo",
                columns: table => new
                {
                    idUserInfo = table.Column<long>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    idUser = table.Column<long>(nullable: false),
                    ViewName = table.Column<string>(maxLength: 16, nullable: true),
                    ManagerPermission = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfo", x => x.idUserInfo);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "idUser", "Password", "SignEmail" },
                values: new object[] { 3L, "1111", "testuser@email.net" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "idUser", "Password", "SignEmail" },
                values: new object[] { 4L, "1111", "user@email.net" });

            migrationBuilder.InsertData(
                table: "UserInfo",
                columns: new[] { "idUserInfo", "ManagerPermission", "ViewName", "idUser" },
                values: new object[] { 1L, 2147483647, "root", 1L });

            migrationBuilder.InsertData(
                table: "UserInfo",
                columns: new[] { "idUserInfo", "ManagerPermission", "ViewName", "idUser" },
                values: new object[] { 2L, 1, "admin", 2L });

            migrationBuilder.InsertData(
                table: "UserInfo",
                columns: new[] { "idUserInfo", "ManagerPermission", "ViewName", "idUser" },
                values: new object[] { 3L, 1, "test User", 3L });

            migrationBuilder.InsertData(
                table: "UserInfo",
                columns: new[] { "idUserInfo", "ManagerPermission", "ViewName", "idUser" },
                values: new object[] { 4L, 0, "User", 4L });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserInfo");

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "idUser",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "idUser",
                keyValue: 4L);
        }
    }
}
