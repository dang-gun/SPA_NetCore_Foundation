using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using SPA_NetCore_Foundation.Global;

namespace IdentityServer4_Custom.IdentityServer4
{
    /// <summary>
    /// 토큰 처리 관련 기능들
    /// </summary>
    public class TokenProcess
    {
        private readonly string ClientId = "resourceownerclient";
        private readonly string ClientSecret = "dataEventRecordsSecret";
        private readonly string Scope = "dataEventRecords offline_access openid";

        /// <summary>
        /// 인증에 사용할  http클라이언트
        /// </summary>
        private HttpClient hcAuthClient = new HttpClient();
        /// <summary>
        /// IdentityServer4로 구현된 서버 주소
        /// </summary>
        private string sIdentityServer4_Url = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sAuthUrl"></param>
        public TokenProcess(string sAuthUrl)
        {
            this.sIdentityServer4_Url = sAuthUrl;
        }

        /// <summary>
        /// 인증서버에 인증을 요청한다.
        /// </summary>
        /// <param name="sID"></param>
        /// <param name="sPassword"></param>
        /// <returns></returns>
        public async Task<TokenResponse> RequestTokenAsync(string sID, string sPassword)
        {
            TokenResponse trRequestToken
                = await hcAuthClient
                        .RequestPasswordTokenAsync(new PasswordTokenRequest
                        {
                            Address = this.sIdentityServer4_Url + "connect/token",

                            ClientId = this.ClientId,
                            ClientSecret = this.ClientSecret,
                            Scope = this.Scope,

                            //유저 인증정보 : 아이디
                            UserName = sID,
                            //유저 인증정보 : 비밀번호
                            Password = sPassword
                        });

            GlobalSign.LogAdd_DB(
                2
                , ModelDB.UserSignLogType.RequestToken
                , 0
                , string.Format("RequestTokenAsync = {2} : {0}, {1}"
                                , sID
                                , sPassword
                                , trRequestToken.HttpErrorReason));

            return trRequestToken;
        }

        /// <summary>
        /// 액세스 토큰 갱신
        /// </summary>
        /// <param name="sRefreshToken">리플레시토큰</param>
        /// <returns></returns>
        public async Task<TokenResponse> RefreshTokenAsync(string sRefreshToken)
        {
            TokenResponse trRequestToken
                = await hcAuthClient
                        .RequestRefreshTokenAsync(new RefreshTokenRequest
                        {
                            Address = this.sIdentityServer4_Url + "connect/token",

                            ClientId = this.ClientId,
                            ClientSecret = this.ClientSecret,
                            Scope = this.Scope,

                            RefreshToken = sRefreshToken
                        });
            
            GlobalSign.LogAdd_DB(
                2
                , ModelDB.UserSignLogType.RefreshToken
                , 0
                , string.Format("RefreshTokenAsync = {0} : {1}"
                                , trRequestToken.HttpErrorReason
                                , sRefreshToken));

            return trRequestToken;
        }


        /// <summary>
        /// 지정된 토큰 제거
        /// </summary>
        /// <param name="sRefreshToken"></param>
        /// <returns></returns>
        public async Task<TokenRevocationResponse> RevocationTokenAsync(string sRefreshToken)
        {
            //엑세스 토큰도 제거가 가능하지만
            //이 시나리오에서는 리플레시 토큰만 제거하면 된다.
            TokenRevocationResponse trRequestToken
                = await hcAuthClient
                        .RevokeTokenAsync(new TokenRevocationRequest
                        {
                            Address = this.sIdentityServer4_Url + "connect/revocation",
                            ClientId = this.ClientId,
                            ClientSecret = this.ClientSecret,

                            Token = sRefreshToken,
                            TokenTypeHint = "refresh_token"
                        });

            GlobalSign.LogAdd_DB(
                2
                , ModelDB.UserSignLogType.RevocationToken
                , 0
                , string.Format("RevocationTokenAsync = {0} : "
                                , trRequestToken.HttpErrorReason));

            return trRequestToken;
        }

        /// <summary>
        /// 엑세스토큰을 이용하여 유저 정보를 받는다.
        /// </summary>
        /// <param name="sAccessToken"></param>
        /// <returns></returns>
        public async Task<UserInfoResponse> UserInfoAsync(string sAccessToken)
        {
            UserInfoResponse uirUser
                = await hcAuthClient
                        .GetUserInfoAsync(new UserInfoRequest
                        {
                            Address = this.sIdentityServer4_Url + "connect/userinfo"

                            ,
                            Token = sAccessToken,
                        });

            if (3 < uirUser.Claims.Count())
            {
                GlobalSign.LogAdd_DB(
                2
                , ModelDB.UserSignLogType.UserInfo
                , 0
                , string.Format("UserInfoAsync  = {0} : {1}"
                                , uirUser.HttpErrorReason
                                , uirUser.Claims.ToArray()[2].Value));
            }
            else
            {
                GlobalSign.LogAdd_DB(
                2
                , ModelDB.UserSignLogType.UserInfo
                , 0
                , string.Format("UserInfoAsync  = {0} : {1}"
                                , uirUser.HttpErrorReason
                                , "실패"));
            }

            return uirUser;
        }
    }
}
