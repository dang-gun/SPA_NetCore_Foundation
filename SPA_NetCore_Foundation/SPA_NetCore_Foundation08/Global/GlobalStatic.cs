using Faculty.File;
using IdentityServer4_Custom.IdentityServer4;
using ListToWwwFile;
using ModelDB;
using PBAuto.Global;
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
        #region 자주사용하는 세팅정보 별도 저장
        /// <summary>
        /// 멀티 사인을 어떻게 허용할지 여부
        /// </summary>
        public static UserSignMultiType Setting_SignMultiType 
            = UserSignMultiType.None;
        #endregion



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

        /// <summary>
        /// 
        /// </summary>
        static GlobalStatic()
        {
            Setting_DataProc.OnSettingLoad += Setting_DataProc_OnSettingLoad;
        }

        /// <summary>
        /// 세팅 데이터를 읽어들이면 처리할 내용
        /// </summary>
        private static void Setting_DataProc_OnSettingLoad()
        {
            GlobalStatic.Setting_SignMultiType
                = (UserSignMultiType)GlobalStatic
                    .Setting_DataProc
                    .GetValueInt(FixString_Setting.MultiSignType);
        }
    }
}
