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
using ApiModel;
using SPA_NetCore_Foundation.Global;
using SPA_NetCore_Foundation.Model;
using ModelDB;
using WebApiAuth.Model.Sign;
using System.Security.Claims;
using System.Text.RegularExpressions;

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
            ApiResultReady rrResult = new ApiResultReady(this);
            //로그인 처리용 모델
            SignInResultModel rmResult = new SignInResultModel();
            rrResult.ResultObject = rmResult;

            //API 호출 시간
            DateTime dtNow = DateTime.Now;

            //사인인 시도 기록
            GlobalSign.LogAdd_DB(
                1
                , ModelDB.UserSignLogType.SignIn
                , 0
                , string.Format("SignIn 시도 - {0}, {1}", sEmail, sPW));


            //검색된 유저
            User findUser = null;

            using (SpaNetCoreFoundationContext db1 = new SpaNetCoreFoundationContext())
            {
                //유저 검색
                findUser
                    = db1.User
                        .FirstOrDefault(m =>
                            m.SignEmail == sEmail
                            && m.Password == sPW);
            }


            if (findUser != null)
            {
                //토큰 요청
                TokenResponse tr = GlobalStatic.TokenProc.RequestTokenAsync(sEmail, sPW).Result;

                if (true == tr.IsError)
                {//에러가 있다.
                    rrResult.InfoCode = "1";
                    rrResult.Message = "아이디나 비밀번호가 틀렸습니다.";
                }
                else
                {//에러가 없다.
                    using (SpaNetCoreFoundationContext db1 = new SpaNetCoreFoundationContext())
                    {
                        //기존 로그인한 유저 검색
                        UserSignIn[] arrSL
                            = db1.UserSignIn
                                .Where(m => m.idUser == findUser.idUser)
                                .ToArray();

                        //기존 로그인한 유저 정보 제거
                        db1.UserSignIn.RemoveRange(arrSL);
                        //db 적용
                        db1.SaveChanges();

                        //사인인 한 유저의 정보
                        UserInfo findUI
                            = db1.UserInfo
                                .Where(m => m.idUser == findUser.idUser)
                                .FirstOrDefault();

                        //로그인 되어있는 유저정보 저장
                        UserSignIn slItem = new UserSignIn();
                        slItem.idUser = findUser.idUser;
                        slItem.RefreshToken = tr.RefreshToken;
                        slItem.SignInDate = dtNow;
                        slItem.RefreshDate = dtNow;

                        //기존 로그인한 유저 정보 제거
                        db1.UserSignIn.Add(slItem);
                        //db 적용
                        db1.SaveChanges();

                        //로그인한 유저에게 전달할 정보
                        rmResult.idUser = findUser.idUser;
                        rmResult.Email = findUser.SignEmail;
                        rmResult.ViewName = findUI.ViewName;

                        rmResult.MgtClass = findUI.MgtClass;

                        rmResult.access_token = tr.AccessToken;
                        rmResult.refresh_token = tr.RefreshToken;

                        //성공 로그
                        //사인인 성공 기록
                        GlobalSign.LogAdd_DB(
                            1
                            , ModelDB.UserSignLogType.SignIn
                            , findUser.idUser
                            , string.Format("SignIn 성공 - {0}", sEmail));
                    }//end using db1
                }
            }
            else
            {
                rrResult.InfoCode = "1";
                rrResult.Message = "아이디나 비밀번호가 틀렸습니다.";
            }

            return rrResult.ToResult(rmResult);
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
            ApiResultReady rrResult = new ApiResultReady(this);

            //API 호출 시간
            DateTime dtNow = DateTime.Now;

            //인증 정보에서 유저 정보 추출
            var identity = (ClaimsIdentity)User.Identity;
            ClaimModel cm = new ClaimModel(identity.Claims);

            //사인아웃 시도 기록
            GlobalSign.LogAdd_DB(
                1
                , ModelDB.UserSignLogType.SignOut
                , cm.id_int
                , string.Format("SignOut 시도 : {0}", cm.email));


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
            return rrResult.ToResult();
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
            ApiResultReady rrResult = new ApiResultReady(this);
            //엑세스 토큰 갱신용 모델
            SignInResultModel rmResult = new SignInResultModel();
            rrResult.ResultObject = rmResult;

            //API 호출 시간
            DateTime dtNow = DateTime.Now;

            //리플레시 토큰 갱신 시도 기록
            GlobalSign.LogAdd_DB(
                1
                , ModelDB.UserSignLogType.RefreshToken
                , 0
                , string.Format("RefreshToAccess 시도 : {0}", sRefreshToken));


            //토큰 갱신 요청
            TokenResponse tr = GlobalStatic.TokenProc.RefreshTokenAsync(sRefreshToken).Result;

            //기존 로그인한 유저 검색
            UserSignIn itemUSI = null;


            if (true == tr.IsError)
            {//토큰 갱신 실패
                //DB에 있는 리플레시 토큰은 수동으로 확인해서 갱신해준다.
                //토큰 정보는 메모리에 저장되기 때문에 서버가 내려갔다 올라오면 토큰정보가 날아간다.
                //이런 예외를 처리하기위해 수동으로 리플레시 토큰을 갱신해야한다.
                using (SpaNetCoreFoundationContext db1 = new SpaNetCoreFoundationContext())
                {
                    //기존 로그인한 유저 검색
                    itemUSI
                        = db1.UserSignIn
                            .Where(m => m.RefreshToken == sRefreshToken)
                            .FirstOrDefault();

                    if (null == itemUSI)
                    {//정보 자체가 없다.
                        rrResult.InfoCode = "-101";
                        rrResult.Message = "갱신실패 : 인증 정보가 없습니다.";
                    }
                    else if (dtNow > itemUSI.RefreshDate)
                    {//인증정보의 유효기간이 지났다.
                        rrResult.InfoCode = "-102";
                        rrResult.Message = "갱신실패 : 인증가능 기간이 지났습니다.";
                    }
                    else
                    {//토큰이 살아있다.
                        //유저를 검색한다.
                        User findUser
                            = db1.User
                                .Where(w => w.idUser == itemUSI.idUser)
                                .FirstOrDefault();

                        //토큰을 갱신한다.
                        tr
                            = GlobalStatic.TokenProc
                                .RequestTokenAsync(findUser.SignEmail, findUser.Password)
                                .Result;
                    }
                }//end using db1 
            }//end if (true == tr.IsError)


            if (true == rrResult.IsSuccess())
            {
                if (true == tr.IsError)
                {
                    rrResult.InfoCode = "1";
                    rrResult.Message = "토큰 갱신에 실패하였습니다.";
                }
            }


            if (true == rrResult.IsSuccess())
            {//에러가 없다.
                //유저 정보를 받는다.
                UserInfoResponse inrUser
                    = GlobalStatic.TokenProc.UserInfoAsync(tr.AccessToken).Result;

                //유저 정보 추출
                ClaimModel cm = new ClaimModel(inrUser.Claims);

                using (SpaNetCoreFoundationContext db1 = new SpaNetCoreFoundationContext())
                {
                    //기존 로그인한 유저 검색
                    itemUSI
                        = db1.UserSignIn
                            .Where(m => m.idUser == cm.id_int)
                            .FirstOrDefault();

                    if (null == itemUSI)
                    {//기존 로그인 정보가 없다,
                        //이러면 강제로 토큰이 상실된 것일 수 있다.
                        rrResult.InfoCode = "1";
                        rrResult.Message = "토큰 갱신에 실패하였습니다.";
                    }
                    else
                    {
                        //로그인 되어있는 유저정보 수정
                        itemUSI.RefreshToken = tr.RefreshToken;
                        itemUSI.RefreshDate = dtNow.AddDays(30);

                        //db 적용
                        db1.SaveChanges();


                        //유저에게 전달할 정보 만들기
                        rmResult.idUser = cm.id_int;
                        rmResult.Email = cm.email;
                        rmResult.ViewName = rmResult.Email;

                        rmResult.access_token = tr.AccessToken;
                        rmResult.refresh_token = tr.RefreshToken;


                        //기록
                        GlobalSign.LogAdd_DB(
                            1
                            , ModelDB.UserSignLogType.RefreshToken
                            , cm.id_int
                            , string.Format("RefreshToAccess 성공 : {0}", rmResult.Email));
                    }
                }//end using db1 
            }

            return rrResult.ToResult(rmResult);
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
            ApiResultReady armResult = new ApiResultReady(this);
            //리턴용 모델
            SignInSimpleResultModel tmResult = new SignInSimpleResultModel();

            //유저 정보 추출
            ClaimModel cm = new ClaimModel(((ClaimsIdentity)User.Identity).Claims);

            //검색된 유저
            User findUser = null;

            using (SpaNetCoreFoundationContext db1 = new SpaNetCoreFoundationContext())
            {
                //유저 검색
                findUser
                    = db1.User
                        .FirstOrDefault(m =>
                            m.idUser == cm.id_int);

                if(null != findUser)
                {//유저 정보가 있다.
                    UserInfo fundUI
                        = db1.UserInfo
                            .Where(m => m.idUser == findUser.idUser)
                            .FirstOrDefault();

                    tmResult.idUser = findUser.idUser;
                    tmResult.Email = findUser.SignEmail;
                    tmResult.ViewName = fundUI.ViewName;

                    tmResult.MgtClass = fundUI.MgtClass;
                }
                else
                {//유저 정보가 없다.
                    armResult.InfoCode = "1";
                    armResult.Message = "엑세스 토큰이 유효하지 않습니다.[로그인 필요]";
                }
            }

            return armResult.ToResult(tmResult);
        }



        /// <summary>
        /// 사인에 사용할 이메일 체크
        /// </summary>
        /// <param name="sEmail"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<ApiResultBaseModel> SignEmailCheck(string sEmail)
        {
            //리턴 보조
            ApiResultReady armResult = new ApiResultReady(this);

            using (SpaNetCoreFoundationContext db1 = new SpaNetCoreFoundationContext())
            {
                User findUser
                    = db1.User
                        .Where(m => m.SignEmail == sEmail)
                        .FirstOrDefault();

                if (null == findUser)
                {//성공
                    armResult.InfoCode = "0";
                }
                else
                {//이미 있음
                    armResult.InfoCode = "-1";
                    armResult.Message = "이미 사용중인 아이디 입니다.";
                }
            }

            return armResult.ToResult(null);
        }

        /// <summary>
        /// 표시이름 중복 확인
        /// </summary>
        /// <param name="sViewName"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<ApiResultBaseModel> ViewNameCheck(string sViewName)
        {
            //리턴 보조
            ApiResultReady armResult = new ApiResultReady(this);

            //유저 정보 추출
            ClaimModel cm = new ClaimModel(((ClaimsIdentity)User.Identity).Claims);

            using (SpaNetCoreFoundationContext db1 = new SpaNetCoreFoundationContext())
            {
                UserInfo findUserInfo
                    = db1.UserInfo
                        .Where(m => m.ViewName == sViewName)
                        .FirstOrDefault();

                if (null == findUserInfo)
                {//성공

                    armResult.InfoCode = "0";
                }
                else
                {//이미 있음
                    if (findUserInfo.idUser == cm.id_int)
                    {//내 아이디다.
                        //내가 쓰는 내 닉네임은 중복검사에서 제외이므로
                        //성공으로 취급한다.
                        armResult.InfoCode = "0";
                    }
                    else
                    {
                        armResult.InfoCode = "-1";
                        armResult.Message = "이미 사용중인 닉네임 입니다.";
                    }

                }
            }

            return armResult.ToResult(null);
        }


        /// <summary>
        /// 가입
        /// </summary>
        /// <param name="sEmail"></param>
        /// <param name="sViewName"></param>
        /// <param name="sPassword"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<ApiResultBaseModel> SignUp(
            [FromForm] string sEmail
            , [FromForm] string sViewName
            , [FromForm] string sPassword)
        {
            //리턴 보조
            ApiResultReady armResult = new ApiResultReady(this);

            DateTime dtNow = DateTime.Now;


            //인증 정보에서 유저 정보 추출
            //유저 정보 추출
            ClaimModel cm = new ClaimModel(((ClaimsIdentity)User.Identity).Claims);


            using (SpaNetCoreFoundationContext db1 = new SpaNetCoreFoundationContext())
            {
                User findUser
                    = db1.User
                        .Where(m => m.SignEmail == sEmail)
                        .FirstOrDefault();

                //표시 이름 검색
                UserInfo findViewName
                    = db1.UserInfo
                        .Where(m => m.ViewName == sViewName)
                        .FirstOrDefault();



                if ("0" == armResult.InfoCode
                    && null != findUser)
                {//이미 있음
                    armResult.InfoCode = "-1";
                    armResult.Message = "이미 사용중인 아이디 입니다.";
                }


                if ("0" == armResult.InfoCode
                    && null != findViewName)
                {
                    armResult.InfoCode = "-3";
                    armResult.Message = "표시이름을 사용하는 사용자가 있습니다.";
                }


                if ("0" == armResult.InfoCode)
                {//성공

                    //사인인 정보 추가
                    User newUser = new User();
                    newUser.SignEmail = sEmail;
                    newUser.Password = sPassword;
                    db1.User.Add(newUser);
                    db1.SaveChanges();

                    //사용자 정보 추가
                    UserInfo newUI = new UserInfo();
                    newUI.idUser = newUser.idUser;
                    newUI.ViewName = newUser.SignEmail;
                    newUI.ViewName = sViewName;
                    newUI.MgtClass = ManagementClassType.User;
                    newUI.SignUpDate = dtNow;
                    db1.UserInfo.Add(newUI);

                    db1.SaveChanges();
                }
            }

            return armResult.ToResult(null);
        }


    }
}