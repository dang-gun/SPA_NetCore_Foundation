using ApiModel;
using IdentityServer4.UserServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelDB;
using SettingDataModel;
using SPA_NetCore_Foundation.Faculty;
using SPA_NetCore_Foundation.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SPA_NetCore_Foundation.Controllers
{
    /// <summary>
    /// 세팅 데이터 관리 컨트롤러
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SettingDataController : ControllerBase
    {
        /// <summary>
        /// DB에 있는 세팅정보를 메모리로 읽어들인다.
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public ActionResult<ApiResultObjectModel> SettingLoad()
        {
            ApiResultReady rrResult = new ApiResultReady(this);
            ApiResultObjectModel armResult = new ApiResultObjectModel();
            rrResult.ResultObject = armResult;

            //유저 정보 추출
            ClaimModel cm = new ClaimModel(((ClaimsIdentity)User.Identity).Claims);
            //이 유저가 해당 관리 등급이 있는지 확인한다.
            ManagementClassCheckType typePC
                = GlobalStatic.MgtA.MgtClassCheck(cm.id_int
                    , ManagementClassType.Root);


            if (typePC == ManagementClassCheckType.Ok)
            {
                //세팅 로드
                GlobalStatic.Setting_DataProc.Setting_Load();
            }
            else
            {
                //에러
                rrResult.InfoCode = ApiResultType.PermissionCheckError.ToString();
            }

            return rrResult.ToResult();
        }

        /// <summary>
        /// DB에 있는 세팅 내용을 확인한다.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<SettingListResultModel> SettingList()
        {
            ApiResultReady rrResult = new ApiResultReady(this);
            SettingListResultModel armResult = new SettingListResultModel();
            rrResult.ResultObject = armResult;

            //유저 정보 추출
            ClaimModel cm = new ClaimModel(((ClaimsIdentity)User.Identity).Claims);
            //이 유저가 해당 관리 등급이 있는지 확인한다.
            ManagementClassCheckType typePC
                = GlobalStatic.MgtA.MgtClassCheck(cm.id_int
                    , ManagementClassType.Root);

            if (typePC == ManagementClassCheckType.Ok)
            {
                using (SpaNetCoreFoundationContext db1 = new SpaNetCoreFoundationContext())
                {
                    //세팅 리스트
                    armResult.SettingList
                        = db1.Setting_Data
                            .OrderBy(m => m.Number)
                            .ToArray();

                }//end using db1
            }
            else
            {
                //에러
                rrResult.InfoCode = ApiResultType.PermissionCheckError.ToString();
            }

            return rrResult.ToResult(armResult);
        }

        /// <summary>
        /// 메모리에 로드되어 있는 설정 정보를 확인한다.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<SettingListResultModel> SettingApply()
        {
            ApiResultReady rrResult = new ApiResultReady(this);
            SettingListResultModel armResult = new SettingListResultModel();
            rrResult.ResultObject = armResult;

            //유저 정보 추출
            ClaimModel cm = new ClaimModel(((ClaimsIdentity)User.Identity).Claims);
            //이 유저가 해당 관리 등급이 있는지 확인한다.
            ManagementClassCheckType typePC
                = GlobalStatic.MgtA.MgtClassCheck(cm.id_int
                    , ManagementClassType.Root);

            if (typePC == ManagementClassCheckType.Ok)
            {
                //적용동 세팅 리스트
                armResult.SettingList
                    = GlobalStatic.Setting_DataProc.Setting_Data.ToArray();
            }
            else
            {
                //에러
                rrResult.InfoCode = ApiResultType.PermissionCheckError.ToString();
            }

            return rrResult.ToResult(armResult);
        }

        /// <summary>
        /// 세팅 정보를 수정한다.
        /// </summary>
        /// <param name="s_dTossData"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<ApiResultObjectModel> SettingSet(
            [FromForm] Setting_Data s_dTossData)
        {
            ApiResultReady rrResult = new ApiResultReady(this);
            ApiResultObjectModel armResult = new ApiResultObjectModel();
            rrResult.ResultObject = armResult;

            //유저 정보 추출
            ClaimModel cm = new ClaimModel(((ClaimsIdentity)User.Identity).Claims);
            //이 유저가 해당 관리 등급이 있는지 확인한다.
            ManagementClassCheckType typePC
                = GlobalStatic.MgtA.MgtClassCheck(cm.id_int
                    , ManagementClassType.Root);

            if (typePC == ManagementClassCheckType.Ok)
            {
                using (SpaNetCoreFoundationContext db1 = new SpaNetCoreFoundationContext())
                {
                    //수정할 개체 선택
                    Setting_Data s_dSelect
                        = db1.Setting_Data
                            .Where(m => m.idSetting_Data == s_dTossData.idSetting_Data)
                            .FirstOrDefault();

                    if (null != s_dSelect)
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
                        rrResult.InfoCode = "1";
                        rrResult.Message = "수정할 대상이 없습니다.";
                    }

                }//end using db1
            }
            else
            {
                //에러
                rrResult.InfoCode = ApiResultType.PermissionCheckError.ToString();
            }

            return rrResult.ToResult(armResult);
        }
        
    }
}
