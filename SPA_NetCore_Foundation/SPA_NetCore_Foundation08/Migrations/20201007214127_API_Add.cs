using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SPA_NetCore_Foundation08.Migrations
{
    /// <summary>
    /// 
    /// </summary>
    public partial class API_Add : Migration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileInfo");

            migrationBuilder.CreateTable(
                name: "ApiLog",
                columns: table => new
                {
                    idApiLog = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApiKey = table.Column<string>(nullable: true),
                    idUser = table.Column<long>(nullable: false),
                    CallDate = table.Column<DateTime>(nullable: false),
                    Step01 = table.Column<string>(maxLength: 512, nullable: true),
                    Contents = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiLog", x => x.idApiLog);
                });

            migrationBuilder.CreateTable(
                name: "BoardPostReplyRelationTreeModels",
                columns: table => new
                {
                    idBoardPostReply = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idBoardPostReply_Re = table.Column<long>(nullable: false),
                    Depth = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardPostReplyRelationTreeModels", x => x.idBoardPostReply);
                });

            migrationBuilder.CreateTable(
                name: "FileData",
                columns: table => new
                {
                    idFileList = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(nullable: true),
                    Ext = table.Column<string>(nullable: true),
                    Size = table.Column<int>(nullable: false),
                    FileDir = table.Column<string>(maxLength: 512, nullable: true),
                    FileUrl = table.Column<string>(maxLength: 512, nullable: true),
                    Description = table.Column<string>(maxLength: 1024, nullable: true),
                    EditorDivision = table.Column<string>(maxLength: 1024, nullable: true),
                    FileState = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileData", x => x.idFileList);
                });

            migrationBuilder.CreateTable(
                name: "UserApi",
                columns: table => new
                {
                    idUserApi = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApiKey = table.Column<string>(maxLength: 64, nullable: true),
                    idUser = table.Column<long>(nullable: false),
                    UserApiState = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserApi", x => x.idUserApi);
                });

            migrationBuilder.UpdateData(
                table: "Board",
                keyColumn: "idBoard",
                keyValue: 1L,
                column: "CreateDate",
                value: new DateTime(2020, 10, 8, 6, 41, 26, 241, DateTimeKind.Local).AddTicks(2473));

            migrationBuilder.UpdateData(
                table: "BoardPost",
                keyColumn: "idBoardPost",
                keyValue: 1L,
                column: "WriteDate",
                value: new DateTime(2020, 10, 8, 6, 41, 26, 242, DateTimeKind.Local).AddTicks(5689));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApiLog");

            migrationBuilder.DropTable(
                name: "BoardPostReplyRelationTreeModels");

            migrationBuilder.DropTable(
                name: "FileData");

            migrationBuilder.DropTable(
                name: "UserApi");

            migrationBuilder.CreateTable(
                name: "FileInfo",
                columns: table => new
                {
                    idFileList = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    EditorDivision = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: true),
                    Ext = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileDir = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileState = table.Column<int>(type: "int", nullable: false),
                    FileUrl = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    Size = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileInfo", x => x.idFileList);
                });

            migrationBuilder.UpdateData(
                table: "Board",
                keyColumn: "idBoard",
                keyValue: 1L,
                column: "CreateDate",
                value: new DateTime(2020, 10, 7, 6, 10, 17, 564, DateTimeKind.Local).AddTicks(2133));

            migrationBuilder.UpdateData(
                table: "BoardPost",
                keyColumn: "idBoardPost",
                keyValue: 1L,
                column: "WriteDate",
                value: new DateTime(2020, 10, 7, 6, 10, 17, 565, DateTimeKind.Local).AddTicks(5182));
        }
    }
}
