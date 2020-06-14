using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SPA_NetCore_Foundation.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public ActionResult Call()
        {
            ObjectResult apiresult = new ObjectResult(200);

            apiresult = StatusCode(200, "성공!");

            return apiresult;
        }
    }
}