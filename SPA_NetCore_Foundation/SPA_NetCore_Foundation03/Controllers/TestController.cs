﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
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
        [HttpGet]
        [Route("Call")]
        public ActionResult<ObjectResult> Call()
        {
            ObjectResult apiresult = new ObjectResult(200);

            apiresult = StatusCode(200, "성공!");

            return apiresult;
        }

        [HttpGet]
        public ActionResult<TestModel01> Test01(int nData, string sData)
        {
            //리턴 보조
            ApiResultReady rrResult = new ApiResultReady(this);
            //리턴용 모델
            TestModel01 armResult = new TestModel01();

            if(0 <= nData)
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


    }
}