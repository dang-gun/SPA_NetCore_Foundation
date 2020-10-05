using ModelDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPA_NetCore_Foundation.Faculty
{
    /// <summary>
    /// 세팅 데이터를 처리한다.
    /// </summary>
    public class Setting_DataProcess
    {
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
                Setting_Data
                    = db1.Setting_Data
                        .ToList();
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
