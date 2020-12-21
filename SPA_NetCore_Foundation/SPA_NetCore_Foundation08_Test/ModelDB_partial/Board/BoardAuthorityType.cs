using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelDB
{
    /// <summary>
    /// 게시판 권한 구분
    /// </summary>
    [Flags]
    public enum BoardAuthorityType
    {
        /// <summary>
        /// 없음
        /// </summary>
        None = 0,

        /// <summary>
        /// 리스트 보기 권한.
        /// </summary>
        ReadList = 1 << 0,
        /// <summary>
        /// 리스트 보기 권한.(비회원)
        /// </summary>
        ReadListNonMember = 1 << 1,
        /// <summary>
        /// 게시물 읽기 권한.
        /// 리스트 보기권한이 없는 상태로 이 권한이 있으면
        /// 직접 주소를 넣어야 접근 가능하다.
        /// </summary>
        Read = 1 << 2,
        /// <summary>
        /// 비회원 게시물 읽기 가능
        /// </summary>
        ReadNonMember = 1 << 3,


        /// <summary>
        /// 쓰기 권한.
        /// </summary>
        Write = 1 << 4,
        /// <summary>
        /// 댓글 쓰기 권한
        /// </summary>
        WriteReply = 1 << 5,
        /// <summary>
        /// 비회원은 쓰기 가능
        /// </summary>
        WriteNonMember = 1 << 6,
        /// <summary>
        /// 비회원은 댓글 쓰기 가능
        /// </summary>
        WriteReplyNonMember = 1 << 7,


        /// <summary>
        /// 지우기 권한.
        /// 자기 글만 지울 수 있는 권한이다.
        /// </summary>
        Delete = 1 << 8,
        /// <summary>
        /// 다른 사람 글을 지울수 있는지 권한
        /// </summary>
        DeleteOther = 1 << 9,


        /// <summary>
        /// 수정 권한.
        /// 자기글만 수정할 수 있다.
        /// </summary>
        Edit = 1 << 10,
        /// <summary>
        /// 다른사람 글을 수정할 수 있는 권한.
        /// </summary>
        EditOther = 1 << 11,

        /// <summary>
        /// 공지 권한 - 전체글 작성권한
        /// </summary>
        NoticeAll = 1 << 12,
        /// <summary>
        /// 공지 권한 - 게시판 그룹 공지 작성권한
        /// </summary>
        NoticeGroup = 1 << 13,
        /// <summary>
        /// 공지 권한 - 게시판 공지 작성 권한
        /// </summary>
        NoticeBoard = 1 << 14,

        /// <summary>
        /// 전체 권한
        /// </summary>
        All = int.MaxValue
    }
}
