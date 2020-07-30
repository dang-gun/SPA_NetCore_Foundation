using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
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
            ApiResultReady rrResult = new ApiResultReady(this);
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
                //에러가 없다.
                rrResult.Message = user.Email;

                armResult.access_token = "dasdflcc090fkkc";
                armResult.refresh_token = "das54340fl8fd";
            }
            else
            {
                rrResult.InfoCode = "1";
                rrResult.Message = "아이디나 비밀번호가 틀렸습니다.";
            }

            return rrResult.ToResult();
        }

        /// <summary>
        /// 지정된 토큰을 찾아 지운다.
        /// </summary>
        /// <param name="sRefreshToken"></param>
        /// <returns></returns>
        [HttpPut]
        public ActionResult<string> SignOut(
            [FromForm]string sRefreshToken)
        {
            ApiResultReady rrResult = new ApiResultReady(this);

            //사인아웃에 필요한 작업을 한다.

            //임시로 아이디를 넘긴다.
            return rrResult.ToResult();
        }

    }
}