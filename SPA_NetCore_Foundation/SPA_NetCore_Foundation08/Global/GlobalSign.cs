using IdentityServer4_Custom.IdentityServer4;
using ModelDB;
using PBAuto.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPA_NetCore_Foundation.Global
{
    /// <summary>
    /// 사인 관련
    /// </summary>
    public static class GlobalSign
    {
        /// <summary>
        /// DB에 로그 남기기
        /// </summary>
        /// <param name="nLogLevel">로그 수준</param>
        /// <param name="typeSignLog"></param>
        /// <param name="idUser"></param>
        /// <param name="sMessage"></param>
        public static void LogAdd_DB(
            int nLogLevel
            , UserSignLogType typeSignLog
            , long idUser
            , string sMessage)
        {
            if (nLogLevel <= GlobalStatic.Setting_DataProc.GetValueInt(FixString_Setting.SignLog))
            {
                //기준 날짜
                DateTime dtNow = DateTime.Now;

                using (SpaNetCoreFoundationContext db1 = new SpaNetCoreFoundationContext())
                {
                    UserSignLog newSL = new UserSignLog();
                    newSL.AddDate = dtNow;
                    newSL.Level = nLogLevel;
                    newSL.SignLogType = typeSignLog;

                    newSL.idUser = idUser;
                    newSL.Contents = sMessage;

                    db1.UserSignLog.Add(newSL);
                    db1.SaveChanges();
                }//end using db1
            }

        }//end LogAdd_DB

    }
}
