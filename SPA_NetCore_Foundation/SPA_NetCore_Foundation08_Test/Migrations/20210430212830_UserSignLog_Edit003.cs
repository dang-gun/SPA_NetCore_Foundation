using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SPA_NetCore_Foundation08.Migrations
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UserSignLog_Edit003 : Migration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                values: new object[] { 1333, 1, new DateTime(2021, 5, 1, 6, 28, 28, 373, DateTimeKind.Local).AddTicks(2953), (short)10 });

            migrationBuilder.UpdateData(
                table: "BoardPost",
                keyColumn: "idBoardPost",
                keyValue: 1L,
                column: "WriteDate",
                value: new DateTime(2021, 5, 1, 6, 28, 28, 374, DateTimeKind.Local).AddTicks(5196));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSignLog");

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
