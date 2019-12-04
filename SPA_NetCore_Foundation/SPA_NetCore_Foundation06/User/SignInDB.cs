using SPA_NetCore_Foundation.ModelDB;
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
        
        public SignInDB()
        {
        }


        /// <summary>
        /// 사인인 한 유저의 정보를 리스트에 추가한다.
        /// 이미 추가되있으면 토큰만 갱신한다.
        /// </summary>
        /// <param name="nID"></param>
        /// <param name="sRefreshToken"></param>
        public void Add(long nID, string sRefreshToken)
        {
            //아이디나 리플레시 토큰 둘중하나만 같으면 추출한다.
            //메모리에는 없는데 리플레시토큰을 있을 수 있기 때문.
            UserSignIn usi = null;

            using (SpaNetCoreFoundationContext db1 = new SpaNetCoreFoundationContext(GlobalStatic.DBMgr.DbContext_Opt()))
            {
                //사인인 리스트 검색
                usi
                    = db1.UserSignIn
                        .Where(m => m.idUser == nID
                            || m.RefreshToken == sRefreshToken)
                        .FirstOrDefault();

                if (usi != null)
                {//검색된 정보가 있다.
                    //리플레시 토큰 수정
                    usi.RefreshToken = sRefreshToken;
                }
                else
                {//정보가 없다.
                 //추가한다.
                    usi = new UserSignIn
                    {
                        idUser = nID
                        , RefreshToken = sRefreshToken
                    };

                    db1.UserSignIn.Add(usi);
                }

                //DB 적용
                db1.SaveChanges();
            }//end using db1

        }//end SignInItemList_Add

        /// <summary>
        /// 사인인 리스트에서 지운다.
        /// </summary>
        /// <param name="nID"></param>
        /// <param name="sRefreshToken"></param>
        public void Delete(long nID, string sRefreshToken)
        {
            using (SpaNetCoreFoundationContext db1 = new SpaNetCoreFoundationContext(GlobalStatic.DBMgr.DbContext_Opt()))
            {
                //아이디나 리플레시 토큰 둘중하나만 같으면 추출한다.
                //메모리에는 없는데 리플레시토큰을 있을 수 있기 때문.
                UserSignIn usi
                    = db1.UserSignIn
                        .Where(m => m.idUser == nID
                            || m.RefreshToken == sRefreshToken)
                        .FirstOrDefault();

                if (usi != null)
                {//찾았다.
                 //지운다!
                    db1.UserSignIn.Remove(usi);
                }

                //DB 적용
                db1.SaveChanges();
            }//end using db1
        }


    }//end class GlobalSign
}
