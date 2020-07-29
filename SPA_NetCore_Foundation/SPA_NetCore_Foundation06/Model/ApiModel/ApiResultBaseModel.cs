using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiModel
{
    /// <summary>
    /// API 결과 공통 베이스.
    /// </summary>
    public class ApiResultBaseModel
    {
        /// <summary>
        /// 실패시 전달한 코드
        /// 0 : 성공.
        /// 다른 값은 모두 실패
        /// </summary>
        public string InfoCode { get; set; }
        /// <summary>
        /// 전달할 메시지
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 기본 생성.
        /// InfoCode가 "0"로 초기화됨
        /// </summary>
        public ApiResultBaseModel()
        {
            this.InfoCode = "0";
            this.Message = string.Empty;
        }

        /// <summary>
        /// 인포코드와 메시지를 넣고 생성
        /// </summary>
        /// <param name="sInfoCode"></param>
        /// <param name="sMessage"></param>
        public ApiResultBaseModel(string sInfoCode, string sMessage)
        {
            this.InfoCode = sInfoCode;
            this.Message = sMessage;
        }
    }
}
