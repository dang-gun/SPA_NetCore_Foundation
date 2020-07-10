using ModelDB;
using WebApiAuth.Model.Sign;

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
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool ValidateCredentials(string username, string password);

        /// <summary>
        /// 아이디로 유저를 찾는다.
        /// </summary>
        /// <param name="nID"></param>
        /// <returns></returns>
        UserAuthModel FindById(int nID);

        /// <summary>
        /// 이메일로 유저를 찾는다.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        UserAuthModel FindByEmail(string username);
    }
}
