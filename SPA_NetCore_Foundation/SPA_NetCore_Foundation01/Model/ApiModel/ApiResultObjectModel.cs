using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPA_NetCore_Foundation.Model.ApiModel
{
    /// <summary>
    /// 스웨거에 노출시키지 않고 모델을 리턴할때 사용한다.
    /// 테스트용으로 사용해도된다.(매번 새로운 모델을 만들기 힘들기 때문)
    /// </summary>
    public class ApiResultObjectModel : ApiResultBaseModel
    {
        public object ResultObject { get; set; }

        public ApiResultObjectModel()
            : base()
        {

        }

        /// <summary>
        /// 리턴할 모델 지정
        /// </summary>
        /// <param name="objResult"></param>
        public ApiResultObjectModel(object objResult)
            : base()
        {
            this.ResultObject = objResult;
        }

        public ApiResultObjectModel(string sInfoCode, string sMessage)
            : base(sInfoCode, sMessage)
        {
        }
    }
}
