using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

                armResult.infoCode = "1";
                armResult.message = "'nData'에 음수가 입력되었습니다.";
            }

            return armResult.ToResult(tmResult);
        }

        [HttpGet]
        [Route("Test02")]
        public ActionResult<TestModel02> Test02(int nData)
        {
            //리턴 보조
            ApiResultReadyModel armResult = new ApiResultReadyModel(this);
            //리턴용 모델
            TestModel02 tmResult = new TestModel02();

            if (0 <= nData)
            {//양수다.
                tmResult.nTest001 = nData;
                tmResult.sTest002 = "성공 했습니다!";
            }
            else
            {
                armResult.StatusCode = StatusCodes.Status500InternalServerError;

                armResult.infoCode = "1";
                armResult.message = "'nData'에 음수가 입력되었습니다.";
            }

            return armResult.ToResult(tmResult);
        }
    }
}