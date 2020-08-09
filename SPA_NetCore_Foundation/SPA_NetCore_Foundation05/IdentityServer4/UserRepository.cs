using SPA_NetCore_Foundation.Global;
using SPA_NetCore_Foundation.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4_Custom.UserServices
{
    /// <summary>
    /// 3. 유저 저장소
    /// 유저 정보 및 데이터 엑세스 기능.
    /// 각 프로젝트에 맞게 수정하여 사용한다.
    /// </summary>
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// 인증정보가 확인
        /// </summary>
        /// <param name="sEmail"></param>
        /// <param name="sPassword"></param>
        /// <returns></returns>
        public bool ValidateCredentials(string sEmail, string sPassword)
        {
            var user = FindByEmail(sEmail);
            if (user != null)
            {
                return user.Password.Equals(sPassword);
            }

            return false;
        }

        /// <summary>
        /// 아이디 검색
        /// </summary>
        /// <param name="nID"></param>
        /// <returns></returns>
        public UserRepositoryModel FindById(int nID)
        {
            return this.ToUserRepositoryModel(
                GlobalStatic.UserList
                    .List.FirstOrDefault(x => x.ID == nID));
        }

        /// <summary>
        /// 이름 검색
        /// </summary>
        /// <param name="sEmail"></param>
        /// <returns></returns>
        public UserRepositoryModel FindByEmail(string sEmail)
        {
            return this.ToUserRepositoryModel(
                GlobalStatic.UserList
                    .List.FirstOrDefault(x => 
                        x.Email.Equals(sEmail, StringComparison.OrdinalIgnoreCase)));
        }

        /// <summary>
        /// 로그인에 사용하는 UserSignInfoModel을
        /// IdentityServer4_Custom.UserServices.UserRepositoryModel 로 변환한다.
        /// </summary>
        /// <param name="insUserDB"></param>
        /// <returns></returns>
        private UserRepositoryModel ToUserRepositoryModel(UserSignInfoModel insUserDB)
        {
            UserRepositoryModel urmReturn = null;

            if(null != insUserDB)
            {//데이터가 있다.
                urmReturn = new UserRepositoryModel();
                urmReturn.idUser = insUserDB.ID;
                urmReturn.SignEmail = insUserDB.Email;
                urmReturn.Password = insUserDB.Password;
            }


            return urmReturn;
        }
    }
}
