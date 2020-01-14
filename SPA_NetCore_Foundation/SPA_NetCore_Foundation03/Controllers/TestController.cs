using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjsctThis.Model.ApiModel;
using SPA_NetCore_Foundation.Model;

namespace SPA_NetCore_Foundation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        [Route("Call")]
        public ActionResult<ObjectResult> Call()
        {
            ObjectResult apiresult = new ObjectResult(200);

            apiresult = StatusCode(200, "성공!");

            return apiresult;
        }

        [HttpGet]
        [Route("Test01")]
        public ActionResult<TestModel01> Test01(int nData, string sData)
        {
            //리턴 보조
            ApiResultReadyModel armResult = new ApiResultReadyModel(this);
            //리턴용 모델
            TestModel01 tmResult = new TestModel01();

            if(0 <= nData)
            {//양수다.
                tmResult.nTest = nData;
                tmResult.sTest = sData;
            }
            else
            {
                armResult.StatusCode = StatusCodes.Status500InternalServerError;

                armResult.InfoCode = "1";
                armResult.Message = "'nData'에 음수가 입력되었습니다.";
            }

            return armResult.ToResult(tmResult);
        }


    }
}