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
        /// 리플 리스트를 표시할지 여부
        /// </summary>
        ReplyList = 1 << 1,

        /// <summary>
        /// 자기리스트만 보이는 게시판인지 여부
        /// </summary>
        MyList = 1 << 2,

        /// <summary>
        /// 즐겨찾기 기능 허용 여부
        /// </summary>
        Favorites = 1 << 3,

        /// <summary>
        /// 전체 권한
        /// </summary>
        All = int.MaxValue
    }
}
