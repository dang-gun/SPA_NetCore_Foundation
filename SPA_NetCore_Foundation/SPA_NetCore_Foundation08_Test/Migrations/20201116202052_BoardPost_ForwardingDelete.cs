using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SPA_NetCore_Foundation08.Migrations
{
    /// <summary>
    /// 
    /// </summary>
    public partial class BoardPost_ForwardingDelete : Migration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "idUser_Forwarding",
                table: "BoardPost");

            migrationBuilder.UpdateData(
                table: "Board",
                keyColumn: "idBoard",
                keyValue: 1L,
                column: "CreateDate",
                value: new DateTime(2020, 11, 17, 5, 20, 50, 962, DateTimeKind.Local).AddTicks(4518));

            migrationBuilder.UpdateData(
                table: "BoardPost",
                keyColumn: "idBoardPost",
                keyValue: 1L,
                column: "WriteDate",
                value: new DateTime(2020, 11, 17, 5, 20, 50, 963, DateTimeKind.Local).AddTicks(6284));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "idUser_Forwarding",
                table: "BoardPost",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.UpdateData(
                table: "Board",
                keyColumn: "idBoard",
                keyValue: 1L,
                column: "CreateDate",
                value: new DateTime(2020, 10, 10, 5, 4, 31, 706, DateTimeKind.Local).AddTicks(8425));

            migrationBuilder.UpdateData(
                table: "BoardPost",
                keyColumn: "idBoardPost",
                keyValue: 1L,
                column: "WriteDate",
                value: new DateTime(2020, 10, 10, 5, 4, 31, 708, DateTimeKind.Local).AddTicks(164));
        }
    }
}
