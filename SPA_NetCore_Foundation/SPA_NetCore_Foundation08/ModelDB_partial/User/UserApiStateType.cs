using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelDB
{
    /// <summary>
    /// 유저 api 상태값
    /// </summary>
    [Flags]
    public enum UserApiStateType
    {
        /// <summary>
        /// 없음
        /// </summary>
        None = 0,

        /// <summary>
        /// 정상
        /// </summary>
        Normal = 1,

        /// <summary>
        /// 삭제.
        /// </summary>
        Delete = 2,

        /// <summary>
        /// 관리자에 의해 막힘
        /// </summary>
        Block = 3,

        /// <summary>
        /// 기간 만료
        /// </summary>
        Expiration = 4,
    }
}
