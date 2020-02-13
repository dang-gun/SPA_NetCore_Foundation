using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityServer4.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelDB;
using SPA_NetCore_Foundation.Model.ApiModel;
using SPA_NetCore_Foundation.Model.MyPage;

namespace SPA_NetCore_Foundation.Controllers
{
    /// <summary>
    /// 마이 페이지
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]//OAuth2 인증 설정
    public class MyPageController : ControllerBase
    {
        [HttpGet]
        [Route("MyPageInfo")]
        public ActionResult<MyPageResultModel> MyPageInfo() 
        {
            ApiResultReadyModel armResult = new ApiResultReadyModel(this);
            MyPageResultModel arbmReturn = new MyPageResultModel();

            //인증 정보에서 유저 정보 추출
            var identity = (ClaimsIdentity)User.Identity;
            ClaimModel cm = new ClaimModel(identity.Claims);

            using (SpaNetCoreFoundationContext db1 = new SpaNetCoreFoundationContext())
            {
                //연결된 유저 정보 검색
                UserInfo uiToss
                    = db1.UserInfo
                        .Where(m => m.idUser == cm.id_int)
                        .FirstOrDefault();

                if(true == uiToss.ManagerPermission.HasFlag(ManagerPermissionType.Admin))
                {//관리자 권한이 있다.
                    arbmReturn.AdminPer = true;
                }
            }

            //임시로 아이디를 넘긴다.
            return armResult.ToResult(arbmReturn);
        }
    }
}