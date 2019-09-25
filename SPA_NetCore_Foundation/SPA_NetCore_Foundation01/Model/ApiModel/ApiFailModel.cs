using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPA_NetCore_Foundation.ApiModel.Model
{
    /// <summary>
    /// api 실패시 전달할 모델(자바스크립 전달용)
    /// </summary>
    public class ApiFailModel
    {
        /// <summary>
        /// 실패 코드
        /// </summary>
        public string infoCode { get; set; }
        /// <summary>
        /// 전달할 메시지
        /// </summary>
        public string message { get; set; }

        public ApiFailModel()
        {
            this.infoCode = "0";
            this.message = string.Empty;
        }

        public ApiFailModel(string sInfoCode, string sMessage)
        {
            this.infoCode = sInfoCode;
            this.message = sMessage;
        }
    }
}
