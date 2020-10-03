using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SPA_NetCore_Foundation08.Migrations
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Setting_Data_Add : Migration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Setting_Data",
                columns: table => new
                {
                    idSetting_Data = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ValueData = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setting_Data", x => x.idSetting_Data);
                });

            migrationBuilder.InsertData(
                table: "Setting_Data",
                columns: new[] { "idSetting_Data", "Description", "Name", "Number", "ValueData" },
                values: new object[] { 1L, "프로그램 전체에 표시될 이름", "Title", 1, "ASP.NET Core SPA Foundation 08" });

            migrationBuilder.InsertData(
                table: "UserInfo",
                columns: new[] { "idUserInfo", "PlatformInfo", "RefreshDate", "SignInDate", "SignUpDate", "ViewName", "idUser" },
                values: new object[] { 1L, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "root", 1L });

            migrationBuilder.InsertData(
                table: "UserInfo",
                columns: new[] { "idUserInfo", "PlatformInfo", "RefreshDate", "SignInDate", "SignUpDate", "ViewName", "idUser" },
                values: new object[] { 2L, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", 2L });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Setting_Data");

            migrationBuilder.DeleteData(
                table: "UserInfo",
                keyColumn: "idUserInfo",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "UserInfo",
                keyColumn: "idUserInfo",
                keyValue: 2L);
        }
    }
}
