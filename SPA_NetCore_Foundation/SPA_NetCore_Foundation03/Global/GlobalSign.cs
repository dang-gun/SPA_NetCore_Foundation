using SPA_NetCore_Foundation.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPA_NetCore_Foundation.Global
{
    /// <summary>
    /// 임시로 DB를 대신할 사인인 한 리스트
    /// 엔트리 프레임웍이 연결되면 이건 필요없다.
    /// </summary>
    public class GlobalSign
    {
        /// <summary>
        /// 테스트용 로그인 더미 데이터.
        /// </summary>
        public List<UserSignInfoModel> UserSignInfoList { get; set; }
        /// <summary>
        /// 임시 사인인 리스트
        /// </summary>
        public List<SignInItemModel> SignInItemList { get; set; }
    }
}
