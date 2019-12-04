using ProjsctThis.Model.ApiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPA_NetCore_Foundation.Model
{
    /// <summary>
    /// 사인인이 성공하였을때 전달되는 정보(자바스크립트 전달용)
    /// </summary>
    public class SignInModel: ApiResultBaseModel
    {
        /// <summary>
        /// 성공여부
        /// </summary>
        public bool complete { get; set; }
        /// <summary>
        /// 토큰
        /// </summary>
        public string token { get; set; }

        public SignInModel()
            : base()
        {
            this.complete = false;
            this.token = string.Empty;
        }
    }
}
