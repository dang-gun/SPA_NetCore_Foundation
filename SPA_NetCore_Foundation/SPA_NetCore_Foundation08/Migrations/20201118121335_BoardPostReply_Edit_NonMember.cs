using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SPA_NetCore_Foundation08.Migrations
{
    /// <summary>
    /// 
    /// </summary>
    public partial class BoardPostReply_Edit_NonMember : Migration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NonMembers_Password",
                table: "BoardPostReply");

            migrationBuilder.DropColumn(
                name: "NonMembers_ViewName",
                table: "BoardPostReply");

            migrationBuilder.AddColumn<string>(
                name: "NonMember_Password",
                table: "BoardPostReply",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NonMember_ViewName",
                table: "BoardPostReply",
                maxLength: 32,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Board",
                keyColumn: "idBoard",
                keyValue: 1L,
                column: "CreateDate",
                value: new DateTime(2020, 11, 18, 21, 13, 34, 677, DateTimeKind.Local).AddTicks(3684));

            migrationBuilder.UpdateData(
                table: "BoardPost",
                keyColumn: "idBoardPost",
                keyValue: 1L,
                column: "WriteDate",
                value: new DateTime(2020, 11, 18, 21, 13, 34, 678, DateTimeKind.Local).AddTicks(6871));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NonMember_Password",
                table: "BoardPostReply");

            migrationBuilder.DropColumn(
                name: "NonMember_ViewName",
                table: "BoardPostReply");

            migrationBuilder.AddColumn<string>(
                name: "NonMembers_Password",
                table: "BoardPostReply",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NonMembers_ViewName",
                table: "BoardPostReply",
                type: "nvarchar(32)",
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
    }
}
