using ApiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiAuth.Model.Sign
{
    /// <summary>
    /// 사인인 성공시 전달할 모델
    /// </summary>
    public class SignInResultModel : ApiResultBaseModel
    {
        /// <summary>
        /// 유저의 고유 아이디
        /// </summary>
        public long idUserSignInfo { get; set; }
        /// <summary>
        /// 이메일
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// 테스트용 레벨
        /// </summary>
        public int lv { get; set; }


        /// <summary>
        /// 엑세스 토큰
        /// </summary>
        public string access_token { get; set; }

        /// <summary>
        /// 리플레시 토큰
        /// </summary>
        public string refresh_token { get; set; }

        

        public SignInResultModel()
            : base()
        {
            this.idUserSignInfo = 0;
            this.email = string.Empty;

            this.lv = 0;

            this.access_token = string.Empty;
            this.refresh_token = string.Empty;
        }
    }
}
