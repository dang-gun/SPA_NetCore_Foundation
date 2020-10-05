using Microsoft.EntityFrameworkCore.Migrations;

namespace SPA_NetCore_Foundation08.Migrations
{
    /// <summary>
    /// 
    /// </summary>
    public partial class UserInfo_Edit001 : Migration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MgtClass",
                table: "UserInfo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<long>(
                name: "idUser_Parent",
                table: "UserInfo",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.UpdateData(
                table: "UserInfo",
                keyColumn: "idUserInfo",
                keyValue: 1L,
                column: "MgtClass",
                value: 1);

            migrationBuilder.UpdateData(
                table: "UserInfo",
                keyColumn: "idUserInfo",
                keyValue: 2L,
                column: "MgtClass",
                value: 10000);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MgtClass",
                table: "UserInfo");

            migrationBuilder.DropColumn(
                name: "idUser_Parent",
                table: "UserInfo");
        }
    }
}
