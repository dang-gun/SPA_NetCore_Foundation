using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelDB
{
    /// <summary>
    /// 파일 상태 타입
    /// </summary>
    public enum FileStateType
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
        /// 삭제 예약.
        /// 데몬이돌면서 제거한다.
        /// </summary>
        DeleteReservation = 2,

    }
}
