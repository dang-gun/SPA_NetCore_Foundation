using System.Collections.Generic;
using SPA_NetCore_Foundation.ModelDB;

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
        public List<User> List { get; set; }

        public UserDB()
        {
            this.List = new List<User>();


            List.Add(new User
            {
                idUser = 1,
                Password = "1111",
                SignEmail = "test01@email.net"
            });

            List.Add(new User
            {
                idUser = 2,
                Password = "1111",
                SignEmail = "test02@email.net"
            });
        }
    }
}
