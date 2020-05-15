using IdentityServer4_Custom.IdentityServer4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPA_NetCore_Foundation.Global
{
    public static class GlobalStatic
    {
        /// <summary>
        /// 토큰 처리관련
        /// </summary>
        public static TokenProcess TokenProc = null;

        /// <summary>
        /// DB 타입
        /// </summary>
        public static string DBType = "";
        /// <summary>
        /// DB 컨낵션 스트링 저장
        /// </summary>
        public static string DBString = "";

    }
}
