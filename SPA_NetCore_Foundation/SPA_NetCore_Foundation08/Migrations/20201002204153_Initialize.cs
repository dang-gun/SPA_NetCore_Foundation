using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SPA_NetCore_Foundation08.Migrations
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Initialize : Migration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Board",
                columns: table => new
                {
                    idBoard = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 128, nullable: true),
                    UrlStandard = table.Column<string>(maxLength: 128, nullable: true),
                    BoardState = table.Column<int>(nullable: false),
                    BoardFaculty = table.Column<int>(nullable: false),
                    AuthorityDefault = table.Column<int>(nullable: false),
                    ShowCount = table.Column<short>(nullable: false),
                    idBoardGroup = table.Column<long>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    Memo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Board", x => x.idBoard);
                });

            migrationBuilder.CreateTable(
                name: "BoardAuthority",
                columns: table => new
                {
                    idBoardAuthority = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idBoard = table.Column<long>(nullable: false),
                    idUser = table.Column<long>(nullable: false),
                    Authority = table.Column<int>(nullable: false),
                    AuthState = table.Column<int>(nullable: false),
                    Memo = table.Column<string>(nullable: true),
                    EditDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardAuthority", x => x.idBoardAuthority);
                });

            migrationBuilder.CreateTable(
                name: "BoardCategory",
                columns: table => new
                {
                    idBoardCategory = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idBoard = table.Column<long>(nullable: false),
                    Title = table.Column<string>(maxLength: 64, nullable: true),
                    Memo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardCategory", x => x.idBoardCategory);
                });

            migrationBuilder.CreateTable(
                name: "BoardContent",
                columns: table => new
                {
                    idBoardContent = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idBoard = table.Column<long>(nullable: false),
                    idBoardPost = table.Column<long>(nullable: false),
                    Content = table.Column<string>(nullable: true),
                    WriteIP = table.Column<string>(maxLength: 256, nullable: true),
                    EditIP = table.Column<string>(maxLength: 256, nullable: true),
                    FileList = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardContent", x => x.idBoardContent);
                });

            migrationBuilder.CreateTable(
                name: "BoardGroup",
                columns: table => new
                {
                    idBoardGroup = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 64, nullable: true),
                    Memo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardGroup", x => x.idBoardGroup);
                });

            migrationBuilder.CreateTable(
                name: "BoardPost",
                columns: table => new
                {
                    idBoardPost = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idBoard = table.Column<long>(nullable: false),
                    idBoardCategory = table.Column<long>(nullable: false),
                    Title = table.Column<string>(maxLength: 128, nullable: true),
                    idUser = table.Column<long>(nullable: false),
                    idUser_Forwarding = table.Column<long>(nullable: false),
                    ViewCount = table.Column<long>(nullable: false),
                    ViewCountNone = table.Column<long>(nullable: false),
                    ReplyCount = table.Column<int>(nullable: false),
                    ThumbnailUrl = table.Column<string>(nullable: true),
                    PostState = table.Column<int>(nullable: false),
                    WriteDate = table.Column<DateTime>(nullable: false),
                    EditDate = table.Column<DateTime>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: false),
                    WriteIP = table.Column<string>(maxLength: 256, nullable: true),
                    EditIP = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardPost", x => x.idBoardPost);
                });

            migrationBuilder.CreateTable(
                name: "BoardPostReply",
                columns: table => new
                {
                    idBoardPostReply = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idBoard = table.Column<long>(nullable: false),
                    idBoardPost = table.Column<long>(nullable: false),
                    idBoardPostReply_Re = table.Column<long>(nullable: false),
                    idBoardPostReply_ReParent = table.Column<long>(nullable: false),
                    ReReplyCount = table.Column<int>(nullable: false),
                    idUser = table.Column<long>(nullable: false),
                    ReplyState = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    WriteDate = table.Column<DateTime>(nullable: false),
                    EditDate = table.Column<DateTime>(nullable: false),
                    DeleteDate = table.Column<DateTime>(nullable: false),
                    WriteIP = table.Column<string>(maxLength: 256, nullable: true),
                    EditIP = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardPostReply", x => x.idBoardPostReply);
                });

            migrationBuilder.CreateTable(
                name: "FileInfo",
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
                    table.PrimaryKey("PK_FileInfo", x => x.idFileList);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    idUser = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SignEmail = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.idUser);
                });

            migrationBuilder.CreateTable(
                name: "UserInfo",
                columns: table => new
                {
                    idUserInfo = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idUser = table.Column<long>(nullable: false),
                    ViewName = table.Column<string>(maxLength: 16, nullable: true),
                    SignUpDate = table.Column<DateTime>(nullable: false),
                    SignInDate = table.Column<DateTime>(nullable: false),
                    RefreshDate = table.Column<DateTime>(nullable: false),
                    PlatformInfo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfo", x => x.idUserInfo);
                });

            migrationBuilder.CreateTable(
                name: "UserSignIn",
                columns: table => new
                {
                    idUserSignIn = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idUser = table.Column<long>(nullable: false),
                    RefreshToken = table.Column<string>(nullable: true),
                    SignInDate = table.Column<DateTime>(nullable: false),
                    RefreshDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSignIn", x => x.idUserSignIn);
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "idUser", "Password", "SignEmail" },
                values: new object[] { 1L, "1111", "test01@email.net" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "idUser", "Password", "SignEmail" },
                values: new object[] { 2L, "1111", "test02@email.net" });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Board");

            migrationBuilder.DropTable(
                name: "BoardAuthority");

            migrationBuilder.DropTable(
                name: "BoardCategory");

            migrationBuilder.DropTable(
                name: "BoardContent");

            migrationBuilder.DropTable(
                name: "BoardGroup");

            migrationBuilder.DropTable(
                name: "BoardPost");

            migrationBuilder.DropTable(
                name: "BoardPostReply");

            migrationBuilder.DropTable(
                name: "FileInfo");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "UserInfo");

            migrationBuilder.DropTable(
                name: "UserSignIn");
        }
    }
}
