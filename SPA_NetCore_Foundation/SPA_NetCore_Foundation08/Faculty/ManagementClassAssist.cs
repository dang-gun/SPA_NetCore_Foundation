using IdentityServer4.ResponseHandling;
using ModelDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPA_NetCore_Foundation.Faculty
{
    /// <summary>
    /// 권한 등급 체크 타입
    /// </summary>
    public enum ManagementClassCheckType
    {
        /// <summary>
        /// 
        /// </summary>
        None = 0,

        /// <summary>
        /// 성공
        /// </summary>
        Ok = 1,

        /// <summary>
        /// 요청자 유저를 찾지 못했다.
        /// </summary>
        NoUser = 100,

        /// <summary>
        /// 대상을 찾지 못했다.
        /// </summary>
        NoTarget = 200,
        /// <summary>
        /// 권한이 없다.
        /// </summary>
        NoPer = 300,
    }

    /// <summary>
    /// 관리 권한 지원
    /// </summary>
    public class ManagementClassAssist
    {
        /// <summary>
        /// 지정된 관리 등급보다 내가 높은 권한 등급을 가지고 있는지 확인한다.
        /// </summary>
        /// <param name="nUserId"></param>
        /// <param name="typeMgtClass"></param>
        /// <returns></returns>
        public ManagementClassCheckType MgtClassCheck(
            long nUserId
            , ManagementClassType typeMgtClass)
        {
            ManagementClassCheckType typePCReturn = ManagementClassCheckType.None;

            using (SpaNetCoreFoundationContext db1 = new SpaNetCoreFoundationContext())
            {
                //내정보 찾기
                UserInfo ui
                    = db1.UserInfo
                        .Where(m => m.idUser == nUserId)
                        .FirstOrDefault();

                if (null == ui)
                {//검색 대상이 없다.
                    typePCReturn = ManagementClassCheckType.NoUser;
                }
                else
                {
                    if (ui.MgtClass < typeMgtClass)
                    {//권한이 없다.
                        typePCReturn = ManagementClassCheckType.NoPer;
                    }
                    else
                    {//권한이 있다.
                        typePCReturn = ManagementClassCheckType.Ok;
                    }
                }

            }//end using db1

            return typePCReturn;
        }

        /// <summary>
        /// 자기가 사용할 수 있는 유저 정보를 준다.
        /// 내가 소유자면 uiUse와 uiMy가 같은 정보다.
        /// 내가 직원이면 uiUse와 uiMy가 다른 정보가 들어간다.
        /// </summary>
        /// <param name="nUserId"></param>
        /// <param name="uiUse"></param>
        /// <param name="uiMy"></param>
        public void UserInfoGet(
            long nUserId
            , out UserInfo uiUse
            , out UserInfo uiMy)
        {
            UserInfo uiUseTemp = null;
            UserInfo uiMyTemp = null;

            using (SpaNetCoreFoundationContext db1 = new SpaNetCoreFoundationContext())
            {
                //내정보 찾기
                uiMyTemp
                    = db1.UserInfo
                        .Where(m => m.idUser == nUserId)
                        .FirstOrDefault();

                if (null != uiMyTemp)
                {
                    switch (uiMyTemp.MgtClass)
                    {
                        case ManagementClassType.Root:
                        case ManagementClassType.Admin:
                        case ManagementClassType.User:
                            //내가 소유주다.
                            //검색된 내용이 내 정보다.
                            uiUseTemp = uiMyTemp;
                            break;

                        case ManagementClassType.RootDev:
                        case ManagementClassType.AdminEmployee:
                            //내가 직원이다.
                            //부모 정보를 검색한다.
                            uiUseTemp
                                = db1.UserInfo
                                    .Where(m => m.idUser == uiMyTemp.idUser_Parent)
                                    .FirstOrDefault();
                            break;

                        case ManagementClassType.RootTest:
                        case ManagementClassType.AdminTest:
                        case ManagementClassType.TestUser:
                            //테스트 계정
                            //내 정보를 사용한다.
                            uiUseTemp = uiMyTemp;
                            break;


                            //여긴 디폴트가 필요 없다.
                    }
                }

                //검색 결과 저장
                uiUse = uiUseTemp;
                uiMy = uiMyTemp;
            }//end using db1
        }//end UserInfoGet
    }
}
