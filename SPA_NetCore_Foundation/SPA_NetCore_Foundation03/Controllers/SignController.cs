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
using ProjsctThis.Model.ApiModel;
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
                //에러가 없다.
                smResult.message = user.Email;
            }
            else
            {
                armResult.infoCode = "1";
                armResult.message = "아이디나 비밀번호가 틀렸습니다.";

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
            [FromForm]string sRefreshToken)
        {
            ApiResultReadyModel armResult = new ApiResultReadyModel(this);
            ApiResultBaseModel arbm = new ApiResultBaseModel();

            //사인아웃에 필요한 작업을 한다.

            //임시로 아이디를 넘긴다.
            return armResult.ToResult(arbm);
        }

    }
}