using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelDB
{
    /// <summary>
    /// 메니저 권한 리스트
    /// </summary>
    public enum ManagerPermissionType
    {
        /// <summary>
        /// 권한 없음.
        /// 권한이 없으면 일반 유저임.
        /// </summary>
        None = 0,

        /// <summary>
        /// 관리자
        /// </summary>
        Admin = 1 << 0,


        /// <summary>
        /// 테스트 유저
        /// </summary>
        TestUser = 1 << 40000,

        /// <summary>
        /// 모든 권한
        /// </summary>
        All = int.MaxValue
    }
}
