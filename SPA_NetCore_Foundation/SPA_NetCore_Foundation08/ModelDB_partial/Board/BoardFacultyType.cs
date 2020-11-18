using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelDB
{
    /// <summary>
    /// 게시판 기능 옵션 구분
    /// </summary>
    [Flags]
    public enum BoardFacultyType
    {
        /// <summary>
        /// 없음
        /// </summary>
        None = 0,

        /// <summary>
        /// 한페이지 컨탠츠 수 - 서버 데이터 사용.
        /// 이 값이 없으면 유저 데이터 사용
        /// 디폴트.
        /// </summary>
        ShowCount_Server = 1 << 0,

        /// <summary>
        /// 리플 기능 사용여부
        /// </summary>
        ReplyList = 1 << 1,
        /// <summary>
        /// 대댓글 분리 기능 사용여부
        /// 이것을 사용하지 않으면 댓글리스트를 불러올때 전체를 불러오게 됩니다.
        /// 대댓글을 분리하면 댓글별로 대댓글을 별도로 호출해야합니다.
        /// </summary>
        ReReplyDiv = 1 << 2,


        /// <summary>
        /// 전체 권한
        /// </summary>
        All = int.MaxValue
    }
}
