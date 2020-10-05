using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardModel
{
    /// <summary>
    /// 게시판 리스트 정렬 방향
    /// </summary>
    public enum BoardCommonSortType
    {
        /// <summary>
        /// 없음
        /// </summary>
        None = 0,

        /// <summary>
        /// 오름 차순
        /// 1 -> 2 -> 3 (↗)
        /// </summary>
        Asc = 1,
        /// <summary>
        /// 내림 차순
        /// 3 -> 2 -> 1 (↘)
        /// </summary>
        Desc = 2,
    }
}
