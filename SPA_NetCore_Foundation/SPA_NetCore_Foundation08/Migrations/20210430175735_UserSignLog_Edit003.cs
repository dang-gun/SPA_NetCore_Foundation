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
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Board",
                keyColumn: "idBoard",
                keyValue: 1L,
                column: "CreateDate",
                value: new DateTime(2021, 5, 1, 2, 51, 27, 675, DateTimeKind.Local).AddTicks(6716));

            migrationBuilder.UpdateData(
                table: "BoardPost",
                keyColumn: "idBoardPost",
                keyValue: 1L,
                column: "WriteDate",
                value: new DateTime(2021, 5, 1, 2, 51, 27, 676, DateTimeKind.Local).AddTicks(9979));
        }
    }
}
