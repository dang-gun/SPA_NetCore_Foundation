using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SPA_NetCore_Foundation08.Migrations
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UserSignInTokenLog_Add : Migration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IP",
                table: "UserSignIn",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdateDate",
                table: "UserSignIn",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "PlatformInfo",
                table: "UserSignIn",
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                table: "UserSignIn",
                rowVersion: true,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserSignInTokenLog",
                columns: table => new
                {
                    idUserSignInTokenLog = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idUser = table.Column<long>(nullable: false),
                    RefreshToken = table.Column<string>(nullable: true),
                    GUID = table.Column<string>(nullable: true),
                    IP = table.Column<string>(maxLength: 150, nullable: true),
                    PlatformInfo = table.Column<string>(nullable: true),
                    EndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSignInTokenLog", x => x.idUserSignInTokenLog);
                });

            migrationBuilder.UpdateData(
                table: "Board",
                keyColumn: "idBoard",
                keyValue: 1L,
                column: "CreateDate",
                value: new DateTime(2021, 8, 29, 5, 4, 10, 260, DateTimeKind.Local).AddTicks(9466));

            migrationBuilder.UpdateData(
                table: "BoardPost",
                keyColumn: "idBoardPost",
                keyValue: 1L,
                column: "WriteDate",
                value: new DateTime(2021, 8, 29, 5, 4, 10, 261, DateTimeKind.Local).AddTicks(9468));

            migrationBuilder.UpdateData(
                table: "Setting_Data",
                keyColumn: "idSetting_Data",
                keyValue: 2L,
                column: "Name",
                value: "Sign Log");

            migrationBuilder.InsertData(
                table: "Setting_Data",
                columns: new[] { "idSetting_Data", "Description", "Name", "Number", "OpenType", "ValueData" },
                values: new object[] { 3L, "멀티 사인을 어떻게 허용할지 여부", "Multi Sign  Type", 3, 21, "100" });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSignInTokenLog");

            migrationBuilder.DeleteData(
                table: "Setting_Data",
                keyColumn: "idSetting_Data",
                keyValue: 3L);

            migrationBuilder.DropColumn(
                name: "IP",
                table: "UserSignIn");

            migrationBuilder.DropColumn(
                name: "LastUpdateDate",
                table: "UserSignIn");

            migrationBuilder.DropColumn(
                name: "PlatformInfo",
                table: "UserSignIn");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                table: "UserSignIn");

            migrationBuilder.UpdateData(
                table: "Board",
                keyColumn: "idBoard",
                keyValue: 1L,
                column: "CreateDate",
                value: new DateTime(2021, 5, 1, 2, 57, 34, 961, DateTimeKind.Local).AddTicks(8648));

            migrationBuilder.UpdateData(
                table: "BoardPost",
                keyColumn: "idBoardPost",
                keyValue: 1L,
                column: "WriteDate",
                value: new DateTime(2021, 5, 1, 2, 57, 34, 963, DateTimeKind.Local).AddTicks(290));

            migrationBuilder.UpdateData(
                table: "Setting_Data",
                keyColumn: "idSetting_Data",
                keyValue: 2L,
                column: "Name",
                value: "SignLog");
        }
    }
}
