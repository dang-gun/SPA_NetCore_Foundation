using SPA_NetCore_Foundation.Model.ApiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiAuth.Model.Sign
{
    /// <summary>
    /// 사인인 성공시 전달할 모델
    /// </summary>
    public class SignInSimpleResultModel : ApiResultBaseModel
    {
        /// <summary>
        /// 유저의 고유 아이디
        /// </summary>
        public long idUser { get; set; }
        /// <summary>
        /// 이메일
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 표시 이름
        /// </summary>
        public string ViewName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public SignInSimpleResultModel()
            : base()
        {
            this.idUser = 0;
            this.Email = string.Empty;

            this.ViewName = string.Empty;
        }
    }
}
