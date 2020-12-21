using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SPA_NetCore_Foundation08.Migrations
{
    /// <summary>
    /// 
    /// </summary>
    public partial class BoardPostReply_Edit_NonMembers : Migration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NonMembers_Password",
                table: "BoardPostReply",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NonMembers_ViewName",
                table: "BoardPostReply",
                maxLength: 32,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Board",
                keyColumn: "idBoard",
                keyValue: 1L,
                column: "CreateDate",
                value: new DateTime(2020, 11, 18, 17, 3, 21, 981, DateTimeKind.Local).AddTicks(7868));

            migrationBuilder.UpdateData(
                table: "BoardPost",
                keyColumn: "idBoardPost",
                keyValue: 1L,
                column: "WriteDate",
                value: new DateTime(2020, 11, 18, 17, 3, 21, 983, DateTimeKind.Local).AddTicks(132));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NonMembers_Password",
                table: "BoardPostReply");

            migrationBuilder.DropColumn(
                name: "NonMembers_ViewName",
                table: "BoardPostReply");

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
    }
}
