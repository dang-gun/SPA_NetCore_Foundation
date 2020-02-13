using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel.Client;
using IdentityServer4.UserServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SPA_NetCore_Foundation.Model.ApiModel;
using SPA_NetCore_Foundation.Global;
using SPA_NetCore_Foundation.Model;
using SPA_NetCore_Foundation.Model.User;
using WebApiAuth.Model.Sign;

namespace SPA_NetCore_Foundation.Controllers
{
    /// <summary>
    /// 사인 관련(인,아웃,조인)
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class SignController : ControllerBase
    {
        private readonly string ClientId = "resourceownerclient";
        private readonly string ClientSecret = "dataEventRecordsSecret";
        private readonly string Scope = "dataEventRecords offline_access openid";


        /// <summary>
        /// 사인인 시도
        /// </summary>
        /// <param name="sEmail"></param>
        /// <param name="sPW"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("SignIn")]
        public ActionResult<SignInResultModel> SignIn(
            [FromForm]string sEmail
            , [FromForm]string sPW)
        {
            //결과용
            ApiResultReadyModel armResult = new ApiResultReadyModel(this);
            //로그인 처리용 모델
            SignInResultModel smResult = new SignInResultModel();

            //유저 검색
            UserSignInfoModel user
                = GlobalStatic.UserList
                    .List.FirstOrDefault(m =>
                        m.Email == sEmail
                        && m.Password == sPW);


            if (user != null)
            {
                //토큰 요청
                TokenResponse tr = RequestTokenAsync(sEmail, sPW).Result;

                if (true == tr.IsError)
                {//에러가 있다.
                    armResult.InfoCode = "1";
                    armResult.Message = "아이디나 비밀번호가 틀렸습니다.";

                    armResult.StatusCode = StatusCodes.Status401Unauthorized;
                }
                else
                {//에러가 없다.
                    //로그인 되어있는 유저정보 저장
                    GlobalStatic.SignInList.Add(user.ID, tr.RefreshToken);

                    smResult.id = user.ID;
                    smResult.email = user.Email;

                    smResult.lv = 0;

                    smResult.access_token = tr.AccessToken;
                    smResult.refresh_token = tr.RefreshToken;
                }
            }
            else
            {
                armResult.InfoCode = "1";
                armResult.Message = "아이디나 비밀번호가 틀렸습니다.";

                armResult.StatusCode = StatusCodes.Status401Unauthorized;
            }

            return armResult.ToResult(smResult);
        }

        /// <summary>
        /// 지정된 토큰을 찾아 지운다.
        /// </summary>
        /// <param name="sRefreshToken"></param>
        /// <returns></returns>
        [Authorize]//OAuth2 인증 설정
        [HttpPut]
        [Route("SignOut")]
        public ActionResult<string> SignOut(
            [FromForm]int nID
            , [FromForm]string sRefreshToken)
        {
            ApiResultReadyModel armResult = new ApiResultReadyModel(this);
            ApiResultBaseModel arbm = new ApiResultBaseModel();

            //사인아웃에 필요한 작업을 한다.
            //사용자
            GlobalStatic.SignInList.Delete(nID, sRefreshToken);

            //리플레시 토큰 제거
            if ((null != sRefreshToken)
                && (string.Empty != sRefreshToken))
            {
                TokenRevocationResponse trr = RevocationTokenAsync(sRefreshToken).Result;
            }
            
            //로컬 인증 쿠키 삭제 요청
            HttpContext.SignOutAsync();

            //임시로 아이디를 넘긴다.
            return armResult.ToResult(arbm);
        }



        /// <summary>
        /// 인증에 사용할  http클라이언트
        /// </summary>
        private HttpClient hcAuthClient = new HttpClient();
        /// <summary>
        /// IdentityServer4로 구현된 서버 주소
        /// </summary>
        private string sIdentityServer4_Url = GlobalStatic.AuthUrl;

        

        [HttpPut]
        [Route("RefreshToAccess")]
        public ActionResult<SignInResultModel> RefreshToAccess(
            [FromForm]string sRefreshToken)
        {
            //결과용
            ApiResultReadyModel armResult = new ApiResultReadyModel(this);
            //엑세스 토큰 갱신용 모델
            SignInResultModel smResult = new SignInResultModel();

            //토큰 갱신 요청
            TokenResponse tr = RefreshTokenAsync(sRefreshToken).Result;

            if (true == tr.IsError)
            {//에러가 있다.
                armResult.InfoCode = "1";
                armResult.Message = "토큰 갱신에 실패하였습니다.";

                armResult.StatusCode = StatusCodes.Status401Unauthorized;
            }
            else
            {//에러가 없다.
                //유저 정보를 받는다.
                UserInfoResponse inrUser 
                    = UserInfoAsync(tr.AccessToken).Result;

                //유저 정보 추출
                ClaimModel cm = new ClaimModel(inrUser.Claims);

                //로그인 되어있는 유저정보 저장
                GlobalStatic.SignInList.Add(cm.id_int, tr.RefreshToken);


                //모델에 입력
                smResult.id = cm.id_int;
                smResult.email = cm.email;

                smResult.access_token = tr.AccessToken;
                smResult.refresh_token = tr.RefreshToken;
            }

            return armResult.ToResult(smResult);
        }

        /// <summary>
        /// 엑세스토큰을 이용하여 유저 정보를 받는다.
        /// </summary>
        /// <returns></returns>
        [Authorize]//OAuth2 인증 설정
        [HttpGet]
        [Route("AccessToUserInfo")]
        public ActionResult<SignInSimpleResultModel> AccessToUserInfo()
        {
            //리턴 보조
            ApiResultReadyModel armResult = new ApiResultReadyModel(this);
            //리턴용 모델
            SignInSimpleResultModel tmResult = new SignInSimpleResultModel();

            //유저 정보 추출
            ClaimModel cm = new ClaimModel(((ClaimsIdentity)User.Identity).Claims);

            //검색된 유저
            UserSignInfoModel user
                    = GlobalStatic.UserList.List
                        .FirstOrDefault(m =>
                            m.ID == cm.id_int);

            if (null != user)
            {//유저 정보가 있다.
                tmResult.id = user.ID;
                tmResult.email = user.Email;
            }
            else
            {//유저 정보가 없다.
                armResult.InfoCode = "1";
                armResult.Message = "엑세스 토큰이 유효하지 않습니다.[로그인 필요]";
            }

            return armResult.ToResult(tmResult);
        }

        /// <summary>
        /// 인증서버에 인증을 요청한다.
        /// </summary>
        /// <param name="sID"></param>
        /// <param name="sPassword"></param>
        /// <returns></returns>
        private async Task<TokenResponse> RequestTokenAsync(string sID, string sPassword)
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

            return trRequestToken;
        }

        /// <summary>
        /// 액세스 토큰 갱신
        /// </summary>
        /// <param name="sRefreshToken">리플레시토큰</param>
        /// <returns></returns>
        private async Task<TokenResponse> RefreshTokenAsync(string sRefreshToken)
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

            return trRequestToken;
        }


        /// <summary>
        /// 지정된 토큰 제거
        /// </summary>
        /// <param name="sRefreshToken"></param>
        /// <returns></returns>
        private async Task<TokenRevocationResponse> RevocationTokenAsync(string sRefreshToken)
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

            return trRequestToken;
        }

        /// <summary>
        /// 엑세스토큰을 이용하여 유저 정보를 받는다.
        /// </summary>
        /// <param name="sAccessToken"></param>
        /// <returns></returns>
        private async Task<UserInfoResponse> UserInfoAsync(string sAccessToken)
        {
            UserInfoResponse uirUser
                = await hcAuthClient
                        .GetUserInfoAsync(new UserInfoRequest
                        {
                            Address = this.sIdentityServer4_Url + "connect/userinfo"

                            , Token = sAccessToken,
                        });

            return uirUser;
        }


    }
}