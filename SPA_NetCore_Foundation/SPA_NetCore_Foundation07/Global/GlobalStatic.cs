using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPA_NetCore_Foundation.Global
{
    public static class GlobalStatic
    {
        /// <summary>
        /// 인증 서버 주소
        /// </summary>
        public static string AuthUrl = "http://localhost:12007/";

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
