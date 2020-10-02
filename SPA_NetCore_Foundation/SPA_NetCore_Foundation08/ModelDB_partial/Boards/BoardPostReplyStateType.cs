using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelDB
{
    /// <summary>
    /// 리플 상태값
    /// </summary>
    public enum BoardPostReplyStateType
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
        /// 삭제 함.
        /// 데몬으로 영구 삭제 예약
        /// </summary>
        Delete = 2,

        /// <summary>
        /// 관리자에 의해 블럭됨
        /// </summary>
        Block = 3,

    }
}
