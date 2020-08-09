using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4_Custom.UserServices
{
    /// <summary>
    /// IdentityServer4 인증 정보를 만들기 위한 모델
    /// </summary>
    public class UserRepositoryModel
    {
        /// <summary>
        /// 고유키
        /// </summary>
        public long idUser { get; set; }

        /// <summary>
        /// 사인인에 사용하는 이메일
        /// </summary>
        public string SignEmail { get; set; }

        /// <summary>
        /// 사인인에 사용하는 비밀번호
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 기본 값으로 생성
        /// </summary>
        public UserRepositoryModel()
        {

        }

        /// <summary>
        /// 유저 정보를 이용하여 생성
        /// </summary>
        /// <param name="idUser"></param>
        /// <param name="SignEmail"></param>
        public UserRepositoryModel(long idUser, string SignEmail)
        {
            this.idUser = idUser;
            this.SignEmail = SignEmail;
        }

    }
}
