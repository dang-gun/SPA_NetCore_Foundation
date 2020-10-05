using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelDB
{
    /// <summary>
    /// 관리 권한 타입
    /// </summary>
    public enum ManagementClassType
    {
        /// <summary>
        /// 권한 없음.
        /// 권한이 없으면 사용할 수 없는 계정이다.
        /// </summary>
        None = 0,

        /// <summary>
        /// 최상위 개발자
        /// </summary>
        Root = 1,
        /// <summary>
        /// 하위 개발자
        /// </summary>
        RootDev = 500,
        /// <summary>
        /// 루트 테스트 전용 권한(외부 공개 겸용)
        /// </summary>
        RootTest = 9999,

        /// <summary>
        /// 관리자.
        /// </summary>
        Admin = 10000,
        /// <summary>
        /// 관리자 - 직원
        /// </summary>
        AdminEmployee = 15000,
        /// <summary>
        /// 관리자 - 테스트용(외부 공개 겸용)
        /// </summary>
        AdminTest = 99999,


        


        /// <summary>
        /// 유저
        /// </summary>
        User = 1000000,

        /// <summary>
        /// 테스트 유저
        /// </summary>
        TestUser = 9999999,
    }
}
