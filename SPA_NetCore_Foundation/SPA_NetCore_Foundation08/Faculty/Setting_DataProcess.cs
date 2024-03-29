﻿using ListToWwwFile;
using ModelDB;
using SPA_NetCore_Foundation.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SPA_NetCore_Foundation.Faculty
{
    /// <summary>
    /// 세팅 데이터를 처리한다.
    /// 자주읽어야 하는 데이터가 있다면 이 클래스를 베이스로 사용해서 별도의 클래스를 만들어
    /// 별도 변수에 저장하여 사용한다.
    /// </summary>
    public class Setting_DataProcess
    {
        #region 세팅 로드 이벤트
        /// <summary>
        /// 세팅 로드 이벤트 델리게이트
        /// </summary>
        public delegate void SettingLoadEvntDelegate();
        /// <summary>
        /// 세팅 로드가 완료되면 발생하는 이벤트
        /// </summary>
        public event SettingLoadEvntDelegate OnSettingLoad;

        /// <summary>
        /// 세팅 로드 이벤트 발생기
        /// </summary>
        private void SettingLoad()
        {
            OnSettingLoad();
        }

        #endregion


        /// <summary>
        /// 메모리에 올라온 세팅정보
        /// </summary>
        public List<Setting_Data> Setting_Data;

        /// <summary>
        /// DB에서 세팅 정보를 불러 메모리에 저장한다.
        /// DB에서 세팅을 수정하였다면 이 함수를 호출하여 메모리를 갱신해야한다.
        /// </summary>
        public void Setting_Load()
        {
            using (SpaNetCoreFoundationContext db1 = new SpaNetCoreFoundationContext())
            {
                //대상 로드
                this.Setting_Data
                    = db1.Setting_Data
                        .ToList();

                //설정을 읽고나서 추가로 처리할 내용
                this.SettingLoad();


                //json 파일로 출력***********
                //json으로 출력할 데이터리스트만 추린다.
                List<Setting_Data> listS_D
                    = this.Setting_Data
                        .Where(w => w.OpenType == Setting_DataOpenType.Public)
                        .ToList();

                //변환용 모델로 변환
                List<ListToJavascriptModel> listLTJ
                    = listS_D
                        .Select(s => new ListToJavascriptModel
                        {
                            Summary = s.Description,
                            Name = s.Name,
                            Value = s.ValueData
                        })
                        .ToList();

                //json문자열로 변환
                string sJson = GlobalStatic.JsProc.ToJsonString(listLTJ);

                //파일 출력
                GlobalStatic.FileProc
                    .WWW_FileSave(
                        @"Faculty\Setting_Data.json"
                        , sJson);
            }
        }

        /// <summary>
        /// 값을 검색한다. - 문자열
        /// </summary>
        /// <param name="sName"></param>
        /// <returns></returns>
        public string GetValue(string sName)
        {
            string sReturn = string.Empty;

            using (SpaNetCoreFoundationContext db1 = new SpaNetCoreFoundationContext())
            {
                sReturn
                    = db1.Setting_Data
                        .Where(m => m.Name == sName)
                        .FirstOrDefault()
                        .ValueData;

                if (true == string.IsNullOrEmpty(sReturn))
                {//빈값이면 빈값으로 초기화
                    sReturn = string.Empty;
                }
            }

            return sReturn;
        }

        /// <summary>
        /// 값을 검색한다. - 숫자형
        /// </summary>
        /// <param name="sName"></param>
        /// <returns></returns>
        public int GetValueInt(string sName)
        {
            return Convert.ToInt32(this.GetValue(sName));
        }

    }
}
