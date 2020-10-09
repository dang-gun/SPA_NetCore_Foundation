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
                name: "Setting_Data",
                columns: table => new
                {
                    idSetting_Data = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Number = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ValueData = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Setting_Data", x => x.idSetting_Data);
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

            migrationBuilder.CreateTable(
                name: "UserInfo",
                columns: table => new
                {
                    idUserInfo = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    idUser = table.Column<long>(nullable: false),
                    ViewName = table.Column<string>(maxLength: 16, nullable: true),
                    MgtClass = table.Column<int>(nullable: false),
                    idUser_Parent = table.Column<long>(nullable: false),
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
                table: "Board",
                columns: new[] { "idBoard", "AuthorityDefault", "BoardFaculty", "BoardState", "CreateDate", "Memo", "ShowCount", "Title", "UrlStandard", "idBoardGroup" },
                values: new object[] { 1L, 0, 0, 1, new DateTime(2020, 10, 10, 5, 4, 31, 706, DateTimeKind.Local).AddTicks(8425), "테스트용 게시판", (short)0, "Test", null, 0L });

            migrationBuilder.InsertData(
                table: "BoardAuthority",
                columns: new[] { "idBoardAuthority", "AuthState", "Authority", "EditDate", "Memo", "idBoard", "idUser" },
                values: new object[] { 1L, 0, 2147483647, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 1L, 1L });

            migrationBuilder.InsertData(
                table: "BoardCategory",
                columns: new[] { "idBoardCategory", "Memo", "Title", "idBoard" },
                values: new object[] { 1L, "전체 게시판에 표시되는 분류.", "All", 1L });

            migrationBuilder.InsertData(
                table: "BoardContent",
                columns: new[] { "idBoardContent", "Content", "EditIP", "FileList", "WriteIP", "idBoard", "idBoardPost" },
                values: new object[] { 1L, "DB 생성후 테스트용 자동생성 게시물입니다.<br />내용", "", null, "", 1L, 1L });

            migrationBuilder.InsertData(
                table: "BoardPost",
                columns: new[] { "idBoardPost", "DeleteDate", "EditDate", "EditIP", "PostState", "ReplyCount", "ThumbnailUrl", "Title", "ViewCount", "ViewCountNone", "WriteDate", "WriteIP", "idBoard", "idBoardCategory", "idUser", "idUser_Forwarding" },
                values: new object[] { 1L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, 0, null, "DB 생성후 테스트용 자동생성 게시물입니다.", 0L, 0L, new DateTime(2020, 10, 10, 5, 4, 31, 708, DateTimeKind.Local).AddTicks(164), null, 1L, 0L, 1L, 0L });

            migrationBuilder.InsertData(
                table: "BoardPostReply",
                columns: new[] { "idBoardPostReply", "Content", "DeleteDate", "EditDate", "EditIP", "ReReplyCount", "ReplyState", "Title", "WriteDate", "WriteIP", "idBoard", "idBoardPost", "idBoardPostReply_Re", "idBoardPostReply_ReParent", "idUser" },
                values: new object[] { 1L, "DB 생성후 테스트용 자동생성 게시물의 댓글입니다.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 0, 0, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 1L, 0L, 0L, 0L, 0L });

            migrationBuilder.InsertData(
                table: "Setting_Data",
                columns: new[] { "idSetting_Data", "Description", "Name", "Number", "ValueData" },
                values: new object[] { 1L, "프로그램 전체에 표시될 이름", "Title", 1, "ASP.NET Core SPA Foundation 08" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "idUser", "Password", "SignEmail" },
                values: new object[,]
                {
                    { 1L, "1111", "root" },
                    { 2L, "1111", "admin" },
                    { 3L, "1111", "test01@email.net" },
                    { 4L, "1111", "test02@email.net" }
                });

            migrationBuilder.InsertData(
                table: "UserInfo",
                columns: new[] { "idUserInfo", "MgtClass", "PlatformInfo", "RefreshDate", "SignInDate", "SignUpDate", "ViewName", "idUser", "idUser_Parent" },
                values: new object[,]
                {
                    { 1L, 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "root", 1L, 0L },
                    { 2L, 10000, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin", 2L, 0L },
                    { 3L, 1000000, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "테스트01", 3L, 0L },
                    { 4L, 1000000, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "테스트02", 4L, 0L }
                });
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
                name: "BoardPostReplyRelationTreeModels");

            migrationBuilder.DropTable(
                name: "FileData");

            migrationBuilder.DropTable(
                name: "Setting_Data");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "UserApi");

            migrationBuilder.DropTable(
                name: "UserInfo");

            migrationBuilder.DropTable(
                name: "UserSignIn");
        }
    }
}
