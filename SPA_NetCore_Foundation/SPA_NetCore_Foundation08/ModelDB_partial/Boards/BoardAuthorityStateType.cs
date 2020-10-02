using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelDB
{
    /// <summary>
    /// 게시판 권한 상태 구분
    /// </summary>
    public enum BoardAuthorityStateType
    {
        /// <summary>
        /// 없음
        /// </summary>
        None = 0,

        /// <summary>
        /// 정상 사용
        /// </summary>
        Use = 1,

        /// <summary>
        /// 정지
        /// </summary>
        Stop = 2,
    }
}
