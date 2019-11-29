using SPA_NetCore_Foundation.Model.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SPA_NetCore_Foundation.Global
{
    /// <summary>
    /// 임시로 DB를 대신할 유저 사인인 정보 리스트
    /// 엔트리 프레임웍이 연결되면 이건 필요없다.
    /// </summary>
    public class UserDB
    {
        /// <summary>
        /// 테스트용 더미 데이터.
        /// </summary>
        public List<UserSignInfoModel> List { get; set; }

        public UserDB()
        {
            this.List = new List<UserSignInfoModel>();


            List.Add(new UserSignInfoModel {
                ID = 1,
                Password = "1111",
                Email = "test01@email.net"
            });

            List.Add(new UserSignInfoModel
            {
                ID = 2,
                Password = "1111",
                Email = "test02@email.net"
            });
        }
    }
}
