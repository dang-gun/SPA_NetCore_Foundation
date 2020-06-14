using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPA_NetCore_Foundation.Model.User
{
    /// <summary>
    /// 사인인 아이템
    /// </summary>
    public class SignInItemModel
    {
        /// <summary>
        /// 유저 고유 ID
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// 리플레시 토큰
        /// </summary>
        public string RefreshToken { get; set; }
    }
}
