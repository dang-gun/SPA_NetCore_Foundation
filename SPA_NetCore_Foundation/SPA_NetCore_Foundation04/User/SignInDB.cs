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
    public class SignInDB
    {
        /// <summary>
        /// 임시 사인인 리스트
        /// </summary>
        public List<SignInItemModel> SignInItemList { get; set; }

        public SignInDB()
        {
            this.SignInItemList = new List<SignInItemModel>();
        }


        /// <summary>
        /// 임시
        /// 사인인 한 유저의 정보를 리스트에 추가한다.
        /// 이미 추가되있으면 토큰만 갱신한다.
        /// </summary>
        /// <param name="nID"></param>
        /// <param name="sRefreshToken"></param>
        public void SignInItemList_Add(long nID, string sRefreshToken)
        {
            //아이디나 리플레시 토큰 둘중하나만 같으면 추출한다.
            //메모리에는 없는데 리플레시토큰을 있을 수 있기 때문.
            SignInItemModel sim
                = this.SignInItemList
                    .Where(m => m.ID == nID
                        || m.RefreshToken == sRefreshToken)
                    .FirstOrDefault();

            if(sim != null)
            {//검색된 정보가 있다.
                //리플레시 토큰 수정
                sim.RefreshToken = sRefreshToken;
            }
            else
            {//정보가 없다.
             //추가한다.
                sim = new SignInItemModel { 
                    ID = nID
                    , RefreshToken = sRefreshToken
                };
            }

        }//end SignInItemList_Add


    }//end class GlobalSign
}
