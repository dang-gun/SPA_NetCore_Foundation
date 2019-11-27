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
        //public static string sAuthUrl = "https://localhost:5000/";
        public static string sAuthUrl = "http://localhost:5000/";


        /// <summary>
        /// 테스트용 유저 리스트
        /// </summary>
        public static GlobalUser UserList = new GlobalUser();
    }
}
