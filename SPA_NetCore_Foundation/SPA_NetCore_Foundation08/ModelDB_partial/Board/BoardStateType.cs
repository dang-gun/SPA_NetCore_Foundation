using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelDB
{
    /// <summary>
    /// 게시판 상태
    /// </summary>
    public enum BoardStateType
    {
        /// <summary>
        /// 없음
        /// </summary>
        None = 0,

        /// <summary>
        /// 사용중
        /// </summary>
        Use = 1,

        /// <summary>
        /// 사용안함.
        /// </summary>
        NotUse = 2,

    }
}
