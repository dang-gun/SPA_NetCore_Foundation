using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelDB
{
    /// <summary>
    /// 메니저 권한 리스트
    /// </summary>
    [Flags]
    public enum TestType
    {
        /// <summary>
        /// 권한 없음.
        /// 권한이 없으면 일반 유저임.
        /// </summary>
        None = 0,

        /// <summary>
        /// 테스트 0
        /// </summary>
        Test00 = 1 << 0,


        Test01 = 1 << 1,
        Test02 = 1 << 2,
        Test03 = 1 << 3,
        Test10 = 1 << 10,

        /// <summary>
        /// 모든 권한
        /// </summary>
        All = int.MaxValue
    }
}
