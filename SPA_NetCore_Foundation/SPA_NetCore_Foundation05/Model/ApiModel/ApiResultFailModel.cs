using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiModel
{
    /// <summary>
    /// api 실패시 전달할 모델(자바스크립 전달용)
    /// </summary>
    public class ApiResultFailModel: ApiResultBaseModel
    {
        /// <summary>
        /// 기본 초기화
        /// </summary>
        public ApiResultFailModel()
            : base()
        {
            
        }

        /// <summary>
        /// 인포코드와 메시지를 넣고 생성
        /// </summary>
        /// <param name="sInfoCode"></param>
        /// <param name="sMessage"></param>
        public ApiResultFailModel(string sInfoCode, string sMessage)
            :base(sInfoCode, sMessage)
        {
        }
    }
}
