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
using ApiModel;
using SPA_NetCore_Foundation.Global;
using SPA_NetCore_Foundation.Model;
using SPA_NetCore_Foundation.Model.User;
using WebApiAuth.Model.Sign;

namespace SPA_NetCore_Foundation.Controllers
{
    /// <summary>
    /// 사인 관련(인,아웃,조인)
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SignController : ControllerBase
    {
        /// <summary>
        /// 사인인 시도
        /// </summary>
        /// <param name="sEmail"></param>
        /// <param name="sPW"></param>
        /// <returns></returns>
        [HttpPut]
        public ActionResult<SignInResultModel> SignIn(
            [FromForm]string sEmail
            , [FromForm]string sPW)
        {
            //결과용
            ApiResultReady  rrResult = new ApiResultReady (this);
            //로그인 처리용 모델
            SignInResultModel armResult = new SignInResultModel();
            rrResult.ResultObject = armResult;

            //유저 검색
            UserSignInfoModel user
                = GlobalStatic.UserList
                    .List.FirstOrDefault(m =>
                        m.Email == sEmail
                        && m.Password == sPW);


            if (user != null)
            {
                //토큰 요청
                TokenResponse tr = null;
                //토큰 요청
                tr = GlobalStatic.TokenProc.RequestTokenAsync(sEmail, sPW).Result;

                if (true == tr.IsError)
                {//에러가 있다.
                    rrResult.InfoCode = "1";
                    rrResult.Message = "아이디나 비밀번호가 틀렸습니다.";
                }
                else
                {//에러가 없다.
                    //로그인 되어있는 유저정보 저장
                    GlobalStatic.SignInList.Add(user.ID, tr.RefreshToken);

                    armResult.id = user.ID;
                    armResult.email = user.Email;

                    armResult.lv = 0;

                    armResult.access_token = tr.AccessToken;
                    armResult.refresh_token = tr.RefreshToken;
                }
            }
            else
            {
                rrResult.InfoCode = "1";
                rrResult.Message = "아이디나 비밀번호가 틀렸습니다.";
            }

            return rrResult.ToResult(armResult);
        }

        /// <summary>
        /// 지정된 토큰을 찾아 지운다.
        /// </summary>
        /// <param name="nID"></param>
        /// <param name="sRefreshToken"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]//OAuth2 인증 설정
        public ActionResult<string> SignOut(
            [FromForm]int nID
            , [FromForm]string sRefreshToken)
        {
            ApiResultReady  armResult = new ApiResultReady (this);
            ApiResultBaseModel arbm = new ApiResultBaseModel();

            //사인아웃에 필요한 작업을 한다.
            //사용자
            GlobalStatic.SignInList.Delete(nID, sRefreshToken);

            //리플레시 토큰 제거
            if ((null != sRefreshToken)
                && (string.Empty != sRefreshToken))
            {
                TokenRevocationResponse trr = GlobalStatic.TokenProc.RevocationTokenAsync(sRefreshToken).Result;
            }
            
            //로컬 인증 쿠키 삭제 요청
            HttpContext.SignOutAsync();

            //임시로 아이디를 넘긴다.
            return armResult.ToResult(arbm);
        }

        /// <summary>
        /// 리플레시 토큰을 이용하여 엑세스토큰을 갱신한다.
        /// </summary>
        /// <param name="sRefreshToken"></param>
        /// <returns></returns>
        [HttpPut]
        public ActionResult<SignInResultModel> RefreshToAccess(
            [FromForm]string sRefreshToken)
        {
            //결과용
            ApiResultReady  rrResult = new ApiResultReady (this);
            //엑세스 토큰 갱신용 모델
            SignInResultModel armResult = new SignInResultModel();
            rrResult.ResultObject = armResult;

            //토큰 갱신 요청
            TokenResponse tr = GlobalStatic.TokenProc.RefreshTokenAsync(sRefreshToken).Result;

            if (true == tr.IsError)
            {//에러가 있다.
                rrResult.InfoCode = "1";
                rrResult.Message = "토큰 갱신에 실패하였습니다.";
            }
            else
            {//에러가 없다.
                //유저 정보를 받는다.
                UserInfoResponse inrUser
                    = GlobalStatic.TokenProc.UserInfoAsync(tr.AccessToken).Result;

                //유저 정보 추출
                ClaimModel cm = new ClaimModel(inrUser.Claims);

                //로그인 되어있는 유저정보 저장
                GlobalStatic.SignInList.Add(cm.id_int, tr.RefreshToken);


                //모델에 입력
                armResult.id = cm.id_int;
                armResult.email = cm.email;

                armResult.access_token = tr.AccessToken;
                armResult.refresh_token = tr.RefreshToken;
            }

            return rrResult.ToResult(armResult);
        }



        /// <summary>
        /// 엑세스토큰을 이용하여 유저 정보를 받는다.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]//OAuth2 인증 설정
        public ActionResult<SignInSimpleResultModel> AccessToUserInfo()
        {
            //리턴 보조
            ApiResultReady  armResult = new ApiResultReady (this);
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

    }
}