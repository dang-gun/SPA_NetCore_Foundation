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
        //public static string sAuthUrl = "https://localhost:44302/";
        public static string AuthUrl = "http://localhost:10845/";

        /// <summary>
        /// DB 컨낵션 스트링 저장
        /// </summary>
        public static string DBString = "";
        /// <summary>
        /// DB 컨택스트
        /// </summary>
        public static DbContext_SpaNetCoreFoundation DBMgr 
            = new DbContext_SpaNetCoreFoundation();


        /// <summary>
        /// 사인인 리스트
        /// </summary>
        public static SignInDB SignInList = new SignInDB();
    }
}
