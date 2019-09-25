using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SPA_NetCore_Foundation.ApiModel.Model;
using SPA_NetCore_Foundation.Model;

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
        /// <param name="sID"></param>
        /// <param name="sPW"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("SignIn")]
        public ActionResult<SignInModel> SignIn(
            [FromForm]string sID
            , [FromForm]string sPW)
        {
            ApiReadyModel armResult = new ApiReadyModel(this);

            //로그인 처리용 모델
            SignInModel smResult = new SignInModel();

            if(sID == "test" && sPW == "test")
            {
                smResult.complete = true;
                
                //이 프로젝트에서는 사인인한 유저의 정보를 어디에도 저장하지 않는다.
                //그래서 토큰으로 유저를 구분할 수 있게 만든다.
                smResult.token 
                    = string.Format("{0}▩{1}"
                                    , sID
                                    , Guid.NewGuid().ToString());
            }
            else
            {
                armResult.StatusCode = StatusCodes.Status403Forbidden;

                armResult.InfoCode = "-1";
                armResult.Message = "일치하는 정보가 없습니다.";

                smResult.complete = false;
            }

            return armResult.ToResult(smResult);
        }

        [HttpPut]
        [Route("SignOut")]
        public ActionResult<string> SignOut(
            [FromForm]string sToken)
        {
            ApiReadyModel armResult = new ApiReadyModel(this);

            //토큰의 앞이 유저 정보다.
            string[] sCutToken = sToken.Split("▩");

            //임시로 아이디를 넘긴다.
            //return armResult.ToResult("{email:'" + sCutToken[0] + "'}");
            return armResult.ToResult(sCutToken[0]);
        }

    }
}