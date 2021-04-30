using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SPA_NetCore_Foundation08.Migrations
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UserSignLog_Edit002 : Migration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OpenType",
                table: "Setting_Data",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UserSignLog",
                columns: table => new
                {
                    idUserSignLog = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddDate = table.Column<DateTime>(nullable: false),
                    Level = table.Column<int>(nullable: false),
                    SignLogType = table.Column<int>(nullable: false),
                    idUser = table.Column<long>(nullable: false),
                    Contents = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSignLog", x => x.idUserSignLog);
                });

            migrationBuilder.UpdateData(
                table: "Board",
                keyColumn: "idBoard",
                keyValue: 1L,
                columns: new[] { "AuthorityDefault", "BoardFaculty", "CreateDate", "ShowCount" },
                values: new object[] { 1333, 1, new DateTime(2021, 5, 1, 2, 51, 27, 675, DateTimeKind.Local).AddTicks(6716), (short)10 });

            migrationBuilder.UpdateData(
                table: "BoardPost",
                keyColumn: "idBoardPost",
                keyValue: 1L,
                column: "WriteDate",
                value: new DateTime(2021, 5, 1, 2, 51, 27, 676, DateTimeKind.Local).AddTicks(9979));

            migrationBuilder.UpdateData(
                table: "Setting_Data",
                keyColumn: "idSetting_Data",
                keyValue: 1L,
                column: "OpenType",
                value: 11);

            migrationBuilder.InsertData(
                table: "Setting_Data",
                columns: new[] { "idSetting_Data", "Description", "Name", "Number", "OpenType", "ValueData" },
                values: new object[] { 2L, "사인 관련 로그를 어떻게 남기는 레벨.(높을수록 많은 정보를 남긴다.== DB부하가 심해짐)", "SignLog", 2, 21, "0" });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSignLog");

            migrationBuilder.DeleteData(
                table: "Setting_Data",
                keyColumn: "idSetting_Data",
                keyValue: 2L);

            migrationBuilder.DropColumn(
                name: "OpenType",
                table: "Setting_Data");

            migrationBuilder.UpdateData(
                table: "Board",
                keyColumn: "idBoard",
                keyValue: 1L,
                columns: new[] { "AuthorityDefault", "BoardFaculty", "CreateDate", "ShowCount" },
                values: new object[] { 0, 0, new DateTime(2020, 11, 18, 21, 13, 34, 677, DateTimeKind.Local).AddTicks(3684), (short)0 });

            migrationBuilder.UpdateData(
                table: "BoardPost",
                keyColumn: "idBoardPost",
                keyValue: 1L,
                column: "WriteDate",
                value: new DateTime(2020, 11, 18, 21, 13, 34, 678, DateTimeKind.Local).AddTicks(6871));
        }
    }
}
