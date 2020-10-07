using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SPA_NetCore_Foundation08.Migrations
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Board_DefultDataAdd : Migration
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Board",
                columns: new[] { "idBoard", "AuthorityDefault", "BoardFaculty", "BoardState", "CreateDate", "Memo", "ShowCount", "Title", "UrlStandard", "idBoardGroup" },
                values: new object[] { 1L, 0, 0, 1, new DateTime(2020, 10, 7, 6, 10, 17, 564, DateTimeKind.Local).AddTicks(2133), "테스트용 게시판", (short)0, "Test", null, 0L });

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
                values: new object[] { 1L, "DB 생성후 테스트용 자동생성 게시물입니다.<br />내용", "", null, "", 1L, 0L });

            migrationBuilder.InsertData(
                table: "BoardPost",
                columns: new[] { "idBoardPost", "DeleteDate", "EditDate", "EditIP", "PostState", "ReplyCount", "ThumbnailUrl", "Title", "ViewCount", "ViewCountNone", "WriteDate", "WriteIP", "idBoard", "idBoardCategory", "idUser", "idUser_Forwarding" },
                values: new object[] { 1L, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 0, 0, null, "DB 생성후 테스트용 자동생성 게시물입니다.", 0L, 0L, new DateTime(2020, 10, 7, 6, 10, 17, 565, DateTimeKind.Local).AddTicks(5182), null, 1L, 0L, 1L, 0L });

            migrationBuilder.InsertData(
                table: "BoardPostReply",
                columns: new[] { "idBoardPostReply", "Content", "DeleteDate", "EditDate", "EditIP", "ReReplyCount", "ReplyState", "Title", "WriteDate", "WriteIP", "idBoard", "idBoardPost", "idBoardPostReply_Re", "idBoardPostReply_ReParent", "idUser" },
                values: new object[] { 1L, "DB 생성후 테스트용 자동생성 게시물의 댓글입니다.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 0, 0, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 1L, 0L, 0L, 0L, 0L });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="migrationBuilder"></param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Board",
                keyColumn: "idBoard",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "BoardAuthority",
                keyColumn: "idBoardAuthority",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "BoardCategory",
                keyColumn: "idBoardCategory",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "BoardContent",
                keyColumn: "idBoardContent",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "BoardPost",
                keyColumn: "idBoardPost",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "BoardPostReply",
                keyColumn: "idBoardPostReply",
                keyValue: 1L);
        }
    }
}
