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
using SPA_NetCore_Foundation.Global;
using SPA_NetCore_Foundation.Model.Admin;
using SPA_NetCore_Foundation.Model.ApiModel;

namespace SPA_NetCore_Foundation07.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]//OAuth2 인증 설정
    public class AdminController : ControllerBase
    {
        /// <summary>
        /// DB에 있는 세팅정보를 메모리로 읽어들인다.
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("SettingLoad")]
        public ActionResult<ApiResultObjectModel> SettingLoad()
        {
            ApiResultReadyModel armResult = new ApiResultReadyModel(this);
            ApiResultObjectModel slrmReturn = new ApiResultObjectModel();


            //유저 정보 추출
            ClaimModel cm = new ClaimModel(((ClaimsIdentity)User.Identity).Claims);

            //이 유저가 어드민 권한이 있는지 확인한다.
            PermissionCheckType typePC
                = GlobalPermission.Permission_Check(cm.id_int
                    , ManagerPermissionType.Admin);

            if (typePC == PermissionCheckType.Ok)
            {
                //세팅 로드
                GlobalStatic.Setting_Load();
            }
            else
            {
                //에러
                armResult.InfoCode = PermissionCheckType.NoUser.ToString();
            }

            return armResult.ToResult(slrmReturn);
        }

        /// <summary>
        /// DB에 있는 세팅 내용을 확인한다.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("SettingList")]
        public ActionResult<SettingListResultModel> SettingList()
        {
            ApiResultReadyModel armResult = new ApiResultReadyModel(this);
            SettingListResultModel slrmReturn = new SettingListResultModel();

            //유저 정보 추출
            ClaimModel cm = new ClaimModel(((ClaimsIdentity)User.Identity).Claims);

            //이 유저가 어드민 권한이 있는지 확인한다.
            PermissionCheckType typePC
                = GlobalPermission.Permission_Check(cm.id_int
                    , ManagerPermissionType.Admin);

            if (typePC == PermissionCheckType.Ok)
            {
                using (SpaNetCoreFoundationContext db1 = new SpaNetCoreFoundationContext())
                {
                    //세팅 리스트
                    slrmReturn.SettingList
                        = db1.Setting_Data
                            .OrderBy(m => m.Number)
                            .ToArray();

                }//end using db1
            }
            else
            {
                //에러
                armResult.InfoCode = PermissionCheckType.NoUser.ToString();
            }

            return armResult.ToResult(slrmReturn);
        }

        /// <summary>
        /// 메모리에 로드되어 있는 설정 정보를 확인한다.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("SettingApply")]
        public ActionResult<SettingListResultModel> SettingApply()
        {
            ApiResultReadyModel armResult = new ApiResultReadyModel(this);
            SettingListResultModel slrmReturn = new SettingListResultModel();

            //유저 정보 추출
            ClaimModel cm = new ClaimModel(((ClaimsIdentity)User.Identity).Claims);

            //이 유저가 어드민 권한이 있는지 확인한다.
            PermissionCheckType typePC
                = GlobalPermission.Permission_Check(cm.id_int
                    , ManagerPermissionType.Admin);

            if (typePC == PermissionCheckType.Ok)
            {
                //적용동 세팅 리스트
                slrmReturn.SettingList
                    = GlobalStatic.Setting_Data.ToArray();
                
            }
            else
            {
                //에러
                armResult.InfoCode = PermissionCheckType.NoUser.ToString();
            }

            return armResult.ToResult(slrmReturn);
        }

        [HttpPost]
        [Route("SettingSet")]
        public ActionResult<ApiResultObjectModel> SettingSet(
            [FromForm]Setting_Data s_dTossData)
        {
            ApiResultReadyModel armResult = new ApiResultReadyModel(this);
            ApiResultObjectModel slrmReturn = new ApiResultObjectModel();

            //유저 정보 추출
            ClaimModel cm = new ClaimModel(((ClaimsIdentity)User.Identity).Claims);

            //이 유저가 어드민 권한이 있는지 확인한다.
            PermissionCheckType typePC
                = GlobalPermission.Permission_Check(cm.id_int
                    , ManagerPermissionType.Admin);

            if (typePC == PermissionCheckType.Ok)
            {
                using (SpaNetCoreFoundationContext db1 = new SpaNetCoreFoundationContext())
                {
                    //수정할 개체 선택
                    Setting_Data s_dSelect
                        = db1.Setting_Data
                            .Where(m => m.idSetting_Data == s_dTossData.idSetting_Data)
                            .FirstOrDefault();

                    if(null != s_dSelect)
                    {
                        //데이터 수정
                        s_dSelect.Number = s_dTossData.Number;
                        s_dSelect.Name = s_dTossData.Name;
                        s_dSelect.ValueData = s_dTossData.ValueData;
                        s_dSelect.Description = s_dTossData.Description;

                        //DB 저장
                        db1.SaveChanges();
                    }
                    else
                    {
                        armResult.InfoCode = "1";
                        armResult.Message = "수정할 대상이 없습니다.";
                    }
                    

                }//end using db1
            }
            else
            {
                //에러
                armResult.InfoCode = PermissionCheckType.NoUser.ToString();
            }

            return armResult.ToResult(slrmReturn);
        }


        [HttpGet]
        [Route("UserList")]
        public ActionResult<UserListResultModel> UserList()
        {
            ApiResultReadyModel armResult = new ApiResultReadyModel(this);
            UserListResultModel arbmReturn = new UserListResultModel();

            //인증 정보에서 유저 정보 추출
            //유저 정보 추출
            ClaimModel cm = new ClaimModel(((ClaimsIdentity)User.Identity).Claims);

            //이 유저가 어드민 권한이 있는지 확인한다.
            PermissionCheckType typePC 
                = GlobalPermission.Permission_Check(cm.id_int
                    , ManagerPermissionType.Admin);

            if(typePC == PermissionCheckType.Ok)
            {
                using (SpaNetCoreFoundationContext db1 = new SpaNetCoreFoundationContext())
                {
                    //내정보 찾기
                    //이건 null이 될수 없다.
                    //위에서 체크하고 오기 때문
                    UserInfo ui
                        = db1.UserInfo
                            .Where(m => m.idUser == cm.id_int)
                            .FirstOrDefault();

                    //유저 정보 검색
                    UserListItem[] arrULI
                        = (from user in db1.User
                           join userinfo in db1.UserInfo
                               on user.idUser equals userinfo.idUser
                           select new UserListItem
                           {
                               idUser = user.idUser
                               , SignEmail = user.SignEmail
                               , ViewName = userinfo.ViewName
                           }).ToArray();

                    //유저 리스트 기록
                    arbmReturn.UserList = arrULI;

                }//end using db1
            }
            else
            {
                //에러
                armResult.InfoCode = PermissionCheckType.NoUser.ToString();
            }
            

            //임시로 아이디를 넘긴다.
            return armResult.ToResult(arbmReturn);
        }
    }
}