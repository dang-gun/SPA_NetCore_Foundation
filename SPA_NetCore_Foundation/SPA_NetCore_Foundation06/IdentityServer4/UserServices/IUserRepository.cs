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
        bool ValidateCredentials(string username, string password);

        UserAuthModel FindById(int nID);

        UserAuthModel FindByEmail(string username);
    }
}
