using ModelDB;
using SPA_NetCore_Foundation.Model.ApiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPA_NetCore_Foundation.Model.Admin
{
    /// <summary>
    /// 세팅 리스트 정보
    /// </summary>
    public class SettingListResultModel : ApiResultBaseModel
    {
        /// <summary>
        /// 세팅 리스트
        /// </summary>
        public Setting_Data[] SettingList { get; set; }

    }
}
