using IdentityServer4_Custom.IdentityServer4;
using SPA_NetCore_Foundation.Faculty;
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


        /// <summary>
        /// 관리 등급 지원
        /// </summary>
        public static ManagementClassAssist MgtA = new ManagementClassAssist();


        /// <summary>
        /// 메모리에 올라온 세팅정보
        /// </summary>
        public static Setting_DataProcess Setting_DataProc
            = new Setting_DataProcess();


    }
}
