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
        /// 테스트용 유저 리스트
        /// </summary>
        public static UserDB UserList = new UserDB();
        /// <summary>
        /// 테스트용 사인인 리스트
        /// </summary>
        public static SignInDB SignInList = new SignInDB();
    }
}
