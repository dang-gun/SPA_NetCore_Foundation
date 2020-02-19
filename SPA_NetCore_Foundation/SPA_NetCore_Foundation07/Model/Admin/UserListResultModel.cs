using SPA_NetCore_Foundation.Model.ApiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPA_NetCore_Foundation.Model.Admin
{
    public class UserListItem
    {
        /// <summary>
        /// 유저 고유키
        /// </summary>
        public long idUser { get; set; }

        /// <summary>
        /// 사인인에 사용하는 이메일
        /// </summary>
        public string SignEmail { get; set; }

        /// <summary>
        /// 표시용 이름
        /// </summary>
        public string ViewName { get; set; }
    }

    public class UserListResultModel : ApiResultBaseModel
    {
        /// <summary>
        /// 유저 정보 리스트
        /// </summary>
        public UserListItem[] UserList { get; set; }

    }
}
