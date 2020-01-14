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
        //public static string sAuthUrl = "https://localhost:44335/";
        public static string AuthUrl = "http://localhost:2476/";


        /// <summary>
        /// 테스트용 유저 리스트
        /// </summary>
        public static UserDB UserList = new UserDB();
        /// <summary>
        /// 테스트용 사인인 리스트
        /// </summary>
        public static SignInDB SignInList = new SignInDB();
    }
}
