using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer4_Custom.UserServices
{
    /// <summary>
    /// 2. 유저 저장소 인터페이스
    /// 'IdentityServerBuilder'에 전달될 인터페이스
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// 자격 증명 확인
        /// </summary>
        /// <param name="sSignEmail"></param>
        /// <param name="sPassword"></param>
        /// <returns></returns>
        bool ValidateCredentials(string sSignEmail, string sPassword);

        /// <summary>
        /// 아이디로 유저를 찾는다.
        /// </summary>
        /// <param name="nID"></param>
        /// <returns></returns>
        UserRepositoryModel FindById(int nID);

        /// <summary>
        /// 이메일로 유저를 찾는다.
        /// </summary>
        /// <param name="sSignEmail"></param>
        /// <returns></returns>
        UserRepositoryModel FindByEmail(string sSignEmail);
    }
}
