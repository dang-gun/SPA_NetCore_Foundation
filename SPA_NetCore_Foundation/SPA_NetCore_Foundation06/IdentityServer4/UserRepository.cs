using ModelDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiAuth.Model.Sign;

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
            bool bReturn = false;

            using (SpaNetCoreFoundationContext db1 = new SpaNetCoreFoundationContext())
            {
                User mgrItem
                    = db1.User
                        .FirstOrDefault(x => x.SignEmail == sEmail
                                        && x.Password == sPassword);

                if (mgrItem != null)
                {
                    bReturn = true;
                }
            }

            return bReturn;
        }

        /// <summary>
        /// 아이디 검색
        /// </summary>
        /// <param name="nID"></param>
        /// <returns></returns>
        public UserRepositoryModel FindById(int nID)
        {
            UserAuthModel userReturn = new UserAuthModel();

            using (SpaNetCoreFoundationContext db1 = new SpaNetCoreFoundationContext())
            {
                User mgrItem
                    = db1.User
                        .FirstOrDefault(x => x.idUser == nID);

                userReturn.ManagerAuth_Set(mgrItem);
            }

            return this.ToUserRepositoryModel(userReturn);
        }

        /// <summary>
        /// 이름 검색
        /// </summary>
        /// <param name="sEmail"></param>
        /// <returns></returns>
        public UserRepositoryModel FindByEmail(string sEmail)
        {

            UserAuthModel userAuth = null;

            using (SpaNetCoreFoundationContext db1 = new SpaNetCoreFoundationContext())
            {
                User mgrItem
                    = db1.User
                        .FirstOrDefault(x => x.SignEmail == sEmail);

                if (mgrItem != null)
                {
                    userAuth = new UserAuthModel(mgrItem);
                }
            }

            return this.ToUserRepositoryModel(userAuth);
        }

        /// <summary>
        /// 로그인에 사용하는 UserSignInfoModel을
        /// IdentityServer4_Custom.UserServices.UserRepositoryModel 로 변환한다.
        /// </summary>
        /// <param name="insUserDB"></param>
        /// <returns></returns>
        private UserRepositoryModel ToUserRepositoryModel(UserAuthModel insUserDB)
        {
            UserRepositoryModel urmReturn = null;

            if (null != insUserDB)
            {//데이터가 있다.
                urmReturn = new UserRepositoryModel();
                urmReturn.idUser = insUserDB.idUser;
                urmReturn.SignEmail = insUserDB.SignEmail;
                urmReturn.Password = string.Empty;
            }


            return urmReturn;
        }
    }
}
