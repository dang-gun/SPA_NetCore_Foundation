using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiAuth.Model.Sign
{
    /// <summary>
    /// IdentityServer4 인증 정보를 만들기 위한 모델
    /// </summary>
    public class UserAuthModel
    {
        /// <summary>
        /// 고유키
        /// </summary>
        public long idUser { get; set; }

        /// <summary>
        /// 사인인에 사용하는 이메일
        /// </summary>
        public string SignEmail { get; set; }

        public UserAuthModel()
        {

        }

        public UserAuthModel(ModelDB.User user)
        {
            this.ManagerAuth_Set(user);
        }


        public void ManagerAuth_Set(ModelDB.User user)
        {
            this.idUser = user.idUser;
            this.SignEmail = user.SignEmail;
        }
    }
}
