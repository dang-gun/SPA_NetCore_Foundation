using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelDB
{
    /// <summary>
    /// 플랫폼 타입
    /// </summary>
    public enum PlatformType
    {
        /// <summary>
        /// 없음
        /// </summary>
        None = 0,

        /// <summary>
        /// pc
        /// </summary>
        PC = 1,

        /// <summary>
        /// 모바일
        /// </summary>
        Mobile = 2,


        /// <summary>
        /// 모름
        /// </summary>
        NotKnow = 255,
    }
}
