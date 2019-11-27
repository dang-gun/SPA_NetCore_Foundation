using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPA_NetCore_Foundation.Model.User
{
    /// <summary>
    /// 1. 임의 유저 정보 모델
    /// 유저 로그인용 정보.
    /// 엔트리 프레임워크를 쓴다면 이건 필요없다.
    /// </summary>
    public class UserSignInfoModel
    {
        /// <summary>
        /// 유저 고유 ID
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 이메일
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 비밀 번호
        /// </summary>
        public string Password { get; set; }
    }
}
