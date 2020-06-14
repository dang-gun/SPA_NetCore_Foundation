using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPA_NetCore_Foundation.Global
{
    /// <summary>
    /// 전역변수 모음
    /// </summary>
    public static class GlobalStatic
    {
        /// <summary>
        /// 인증 서버 주소
        /// </summary>
        public static string AuthUrl = "";

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
