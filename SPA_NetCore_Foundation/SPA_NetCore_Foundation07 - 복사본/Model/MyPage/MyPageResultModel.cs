using ModelDB;
using SPA_NetCore_Foundation.Model.ApiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPA_NetCore_Foundation.Model.MyPage
{
    /// <summary>
    /// 마이 페이지용
    /// </summary>
    public class MyPageResultModel : ApiResultBaseModel
    {
        /// <summary>
        /// 관리자 권한 있는지 여부
        /// </summary>
        public bool AdminPer { get; set; }
    }
}
