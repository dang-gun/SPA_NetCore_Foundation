using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardModel
{
    /// <summary>
    /// 게시판 권한 체크 타입
    /// </summary>
    public enum BoardAuthCheckType
    {
        /// <summary>
        /// 없음
        /// </summary>
        None = 0,
        /// <summary>
        /// 성공
        /// </summary>
        Success,

        /// <summary>
        /// 보드 아이디가 없다.
        /// </summary>
        NoBoardId,

        /// <summary>
        /// 게시판을 찾지 못했다.
        /// </summary>
        NotFindBoard,
        /// <summary>
        /// 게시판을 사용할 수 없는 상테다.
        /// </summary>
        NotUseBoard,
    }
}
