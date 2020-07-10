using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using ModelDB;
using WebApiAuth.Model.Sign;
using System.Security.Claims;

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
            ApiResultReadyModel armResult = new ApiResultReadyModel(this);
            //로그인 처리용 모델
            SignInResultModel smResult = new SignInResultModel();

            //API 호출 시간
            DateTime dtNow = DateTime.Now;


            //검색된 유저
            User user = null;

            using (SpaNetCoreFoundationContext db1 = new SpaNetCoreFoundationContext())
            {
                //유저 검색
                user
                    = db1.User
                        .FirstOrDefault(m =>
                            m.SignEmail == sEmail
                            && m.Password == sPW);
            }


            if (user != null)
            {
                //토큰 요청
                TokenResponse tr = GlobalStatic.TokenProc.RequestTokenAsync(sEmail, sPW).Result;

                if (true == tr.IsError)
                {//에러가 있다.
                    armResult.InfoCode = "1";
                    armResult.Message = "아이디나 비밀번호가 틀렸습니다.";

                    armResult.StatusCode = StatusCodes.Status401Unauthorized;
                }
                else
                {//에러가 없다.
                    using (SpaNetCoreFoundationContext db1 = new SpaNetCoreFoundationContext())
                    {
                        //기존 로그인한 유저 검색
                        UserSignIn[] arrSL
                            = db1.UserSignIn
                                .Where(m => m.idUser == user.idUser)
                                .ToArray();

                        //기존 로그인한 유저 정보 제거
                        db1.UserSignIn.RemoveRange(arrSL);
                        //db 적용
                        db1.SaveChanges();


                        //로그인 되어있는 유저정보 저장
                        UserSignIn slItem = new UserSignIn();
                        slItem.idUser = user.idUser;
                        slItem.RefreshToken = tr.RefreshToken;
                        slItem.SignInDate = dtNow;
                        slItem.RefreshDate = dtNow;

                        //기존 로그인한 유저 정보 제거
                        db1.UserSignIn.Add(slItem);
                        //db 적용
                        db1.SaveChanges();



                        //연결된 유저 정보 검색
                        UserInfo uiToss
                            = db1.UserInfo
                                .Where(m => m.idUser == user.idUser)
                                .FirstOrDefault();

                        //로그인한 유저에게 전달할 정보
                        smResult.idUser = user.idUser;
                        smResult.Email = user.SignEmail;

                        smResult.ViewName = uiToss.ViewName;

                        smResult.access_token = tr.AccessToken;
                        smResult.refresh_token = tr.RefreshToken;
                    }//end using db1
                }//end if tr.IsError
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
        [HttpPut]
        [Authorize]//OAuth2 인증 설정
        public ActionResult<string> SignOut(
            [FromForm]string sRefreshToken)
        {
            ApiResultReadyModel armResult = new ApiResultReadyModel(this);
            ApiResultBaseModel arbm = new ApiResultBaseModel();

            //API 호출 시간
            DateTime dtNow = DateTime.Now;

            //인증 정보에서 유저 정보 추출
            var identity = (ClaimsIdentity)User.Identity;
            ClaimModel cm = new ClaimModel(identity.Claims);

            using (SpaNetCoreFoundationContext db1 = new SpaNetCoreFoundationContext())
            {
                //기존 로그인한 유저 검색
                UserSignIn[] arrSL
                    = db1.UserSignIn
                        .Where(m => m.idUser == cm.id_int)
                        .ToArray();

                //기존 로그인한 유저 정보 제거
                db1.UserSignIn.RemoveRange(arrSL);
                //db 적용
                db1.SaveChanges();
            }


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
            ApiResultReadyModel armResult = new ApiResultReadyModel(this);
            //엑세스 토큰 갱신용 모델
            SignInResultModel smResult = new SignInResultModel();

            //API 호출 시간
            DateTime dtNow = DateTime.Now;

            //토큰 갱신 요청
            TokenResponse tr = GlobalStatic.TokenProc.RefreshTokenAsync(sRefreshToken).Result;

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
                    = GlobalStatic.TokenProc.UserInfoAsync(tr.AccessToken).Result;

                //유저 정보 추출
                ClaimModel cm = new ClaimModel(inrUser.Claims);

                using (SpaNetCoreFoundationContext db1 = new SpaNetCoreFoundationContext())
                {
                    //기존 로그인한 유저 검색
                    UserSignIn itemUSI
                        = db1.UserSignIn
                            .Where(m => m.idUser == cm.id_int)
                            .FirstOrDefault();

                    if (null == itemUSI)
                    {//기존 로그인 정보가 없다,
                        //이러면 강제로 토큰이 상실된 것일 수 있다.
                        armResult.InfoCode = "1";
                        armResult.Message = "토큰 갱신에 실패하였습니다.";

                        armResult.StatusCode = StatusCodes.Status401Unauthorized;
                    }
                    else
                    {
                        //로그인 되어있는 유저정보 수정
                        itemUSI.RefreshToken = tr.RefreshToken;
                        itemUSI.RefreshDate = dtNow;

                        //db 적용
                        db1.SaveChanges();


                        //유저 정보 검색
                        UserInfo uiFind
                            = db1.UserInfo
                                .Where(m => m.idUser == cm.id_int)
                                .FirstOrDefault();
                        //유저에게 전달할 정보 만들기
                        smResult.idUser = cm.id_int;
                        smResult.Email = cm.email;
                        smResult.ViewName = uiFind.ViewName;

                        smResult.access_token = tr.AccessToken;
                        smResult.refresh_token = tr.RefreshToken;
                    }
                }   
            }

            return armResult.ToResult(smResult);
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
            ApiResultReadyModel armResult = new ApiResultReadyModel(this);
            //리턴용 모델
            SignInSimpleResultModel tmResult = new SignInSimpleResultModel();

            //유저 정보 추출
            ClaimModel cm = new ClaimModel(((ClaimsIdentity)User.Identity).Claims);


            using (SpaNetCoreFoundationContext db1 = new SpaNetCoreFoundationContext())
            {

                //유저 검색
                User user
                    = db1.User
                        .FirstOrDefault(m =>
                            m.idUser == cm.id_int);

                UserInfo userinfo
                    = db1.UserInfo
                        .FirstOrDefault(m =>
                            m.idUser == user.idUser);


                if (null != user)
                {//유저 정보가 있다.
                    tmResult.idUser = user.idUser;
                    tmResult.Email = user.SignEmail;

                    tmResult.ViewName = userinfo.ViewName;
                }
                else
                {//유저 정보가 없다.
                    armResult.InfoCode = "1";
                    armResult.Message = "엑세스 토큰이 유효하지 않습니다.[로그인 필요]";
                }
            }

            return armResult.ToResult(tmResult);
        }




    }
}