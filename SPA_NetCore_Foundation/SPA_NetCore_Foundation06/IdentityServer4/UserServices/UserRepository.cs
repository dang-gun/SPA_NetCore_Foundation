using SPA_NetCore_Foundation.Global;
using SPA_NetCore_Foundation.ModelDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4_Custom.UserServices
{
    /// <summary>
    /// 3. 유저 저장소
    /// 유저 정보 및 데이터 엑세스 기능
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
        public User FindById(int nID)
        {
            User userReturn = new User();
            
            using (SpaNetCoreFoundationContext db1 = new SpaNetCoreFoundationContext(GlobalStatic.DBMgr.DbContext_Opt()))
            {
                userReturn 
                    = db1.User
                        .FirstOrDefault(x => x.idUser == nID);
            }

            return userReturn;
        }

        /// <summary>
        /// 이름 검색
        /// </summary>
        /// <param name="sEmail"></param>
        /// <returns></returns>
        public User FindByEmail(string sEmail)
        {

            User userReturn = new User();

            using (SpaNetCoreFoundationContext db1 = new SpaNetCoreFoundationContext(GlobalStatic.DBMgr.DbContext_Opt()))
            {
                userReturn
                    = db1.User
                        .FirstOrDefault(x =>
                            x.SignEmail.Equals(sEmail, StringComparison.OrdinalIgnoreCase));
            }

            return userReturn;
        }
    }
}
