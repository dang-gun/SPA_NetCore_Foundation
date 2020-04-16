using ModelDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPA_NetCore_Foundation.Global
{
    /// <summary>
    /// 권한 체크 타입
    /// </summary>
    public enum PermissionCheckType
    {
        
        None = 0,

        /// <summary>
        /// 성공
        /// </summary>
        Ok = 1,

        /// <summary>
        /// 유저를 찾지 못했다.
        /// </summary>
        NoUser = 2,
        /// <summary>
        /// 권한이 없다.
        /// </summary>
        NoPer = 3,
    }

    /// <summary>
    /// 퍼미션 관련 공통 작업
    /// </summary>
    public static class GlobalPermission
    {
        public static PermissionCheckType Permission_Check(
            long nUserId
            , ManagerPermissionType typeManagerPermission)
        {
            PermissionCheckType typePCReturn = PermissionCheckType.None;

            using (SpaNetCoreFoundationContext db1 = new SpaNetCoreFoundationContext())
            {
                //내정보 찾기
                UserInfo ui
                    = db1.UserInfo
                        .Where(m => m.idUser == nUserId)
                        .FirstOrDefault();

                if(null == ui)
                {
                    typePCReturn = PermissionCheckType.NoUser;
                }
                else
                {
                    if(false == ui.ManagerPermission.HasFlag(typeManagerPermission))
                    {
                        typePCReturn = PermissionCheckType.NoPer;
                    }
                    else
                    {
                        typePCReturn = PermissionCheckType.Ok;
                    }
                }

            }//end using db1

            return typePCReturn;
        }
    }
}
