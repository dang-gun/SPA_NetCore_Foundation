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

        /// <summary>
        /// 새로운 파일.
        /// 상태구분을 위해 사용한다.
        /// 동작은 Normal과 같으나 상태값을 다시 확인할때 'Normal'로 변경한다.
        /// </summary>
        NewFile = 3,

        /// <summary>
        /// 수정.
        /// 상태구분을 위해 사용한다.
        /// 동작은 Normal과 같으나 상태값을 다시 확인할때 'Normal'로 변경한다.
        /// </summary>
        Edit = 4,

    }
}
