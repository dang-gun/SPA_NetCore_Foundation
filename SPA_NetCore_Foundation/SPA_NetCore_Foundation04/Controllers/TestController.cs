using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiModel;
using SPA_NetCore_Foundation.Model;

namespace SPA_NetCore_Foundation.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        /// <summary>
        /// 무조건 성공
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<ObjectResult> Call()
        {
            ObjectResult apiresult = new ObjectResult(200);

            apiresult = StatusCode(200, "성공!");

            return apiresult;
        }

        /// <summary>
        /// 표준 리턴 테스트.
        /// nData에 마이너스 값을 전달하면 오류가남
        /// </summary>
        /// <param name="nData"></param>
        /// <param name="sData"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<TestModel01> Test01(int nData, string sData)
        {
            //리턴 보조
            ApiResultReady rrResult = new ApiResultReady(this);
            //리턴용 모델
            TestModel01 armResult = new TestModel01();
            rrResult.ResultObject = armResult;

            if (0 <= nData)
            {//양수다.
                armResult.nTest = nData;
                armResult.sTest = sData;
            }
            else
            {
                rrResult.InfoCode = "1";
                rrResult.Message = "'nData'에 음수가 입력되었습니다.";
            }

            return rrResult.ToResult(armResult);
        }

        /// <summary>
        /// 사인인 되어 있어야 동작
        /// </summary>
        /// <param name="nData"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]//OAuth2 인증 설정
        public ActionResult<TestModel02> Test02(int nData)
        {
            //리턴 보조
            ApiResultReady rrResult = new ApiResultReady(this);
            //리턴용 모델
            TestModel02 armResult = new TestModel02();
            rrResult.ResultObject = armResult;

            //유저 정보 추출
            ClaimModel cm = new ClaimModel(((ClaimsIdentity)User.Identity).Claims);

            if (0 <= nData)
            {//양수다.
                armResult.nTest001 = nData;
                armResult.sTest002 = "성공 했습니다! : " + cm.id;
            }
            else
            {
                rrResult.InfoCode = "1";
                rrResult.Message = "'nData'에 음수가 입력되었습니다.";
            }

            return rrResult.ToResult(armResult);
        }
    }
}