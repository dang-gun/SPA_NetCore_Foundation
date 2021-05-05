using Faculty.File;
using IdentityServer4_Custom.IdentityServer4;
using ListToWwwFile;
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
        /// 로컬 root 경로
        /// </summary>
        public static string Dir_LocalRoot = "";

        /// <summary>
        /// DB 타입
        /// </summary>
        public static string DBType = "";
        /// <summary>
        /// DB 컨낵션 스트링 저장
        /// </summary>
        public static string DBString = "";

        /// <summary>
        /// 게시판 제목 길이
        /// Board.Title 기준.
        /// 이걸 변경하려면 Board.Title을 같이 변경해야 한다.
        /// </summary>
        public static int BoardTitleMexLength = 128;

        /// <summary>
        /// 관리 등급 지원
        /// </summary>
        public static ManagementClassAssist MgtA = new ManagementClassAssist();


        /// <summary>
        /// 메모리에 올라온 세팅정보
        /// </summary>
        public static Setting_DataProcess Setting_DataProc
            = new Setting_DataProcess();



        /// <summary>
        /// 전역에서 사용할 랜덤 함수
        /// </summary>
        public static Random Rand = new Random();


        /// <summary>
        /// 파일 처리 유틸
        /// </summary>
        public static FileProcess FileProc = new FileProcess();
        /// <summary>
        /// 리스트를 자바스크립트 용으로 변환하는 유틸
        /// </summary>
        public static ListToJavascript JsProc = new ListToJavascript();
    }
}
