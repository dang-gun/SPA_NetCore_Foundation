using ProjsctThis.Model.ApiModel;
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
        public long id { get; set; }
        /// <summary>
        /// 이메일
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// 테스트용 레벨
        /// </summary>
        public int lv { get; set; }
        

        public SignInSimpleResultModel()
            : base()
        {
            this.id = 0;
            this.email = string.Empty;

            this.lv = 0;
        }
    }
}
