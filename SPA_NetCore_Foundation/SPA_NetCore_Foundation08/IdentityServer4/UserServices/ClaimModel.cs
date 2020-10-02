using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityServer4.UserServices
{
    /// <summary>
    /// 클라이언트 정보가 들어있는 Claims 모델
    /// IdentityServer4
    /// </summary>
    public class ClaimModel
    {
        /*
         * 자주 쓰는 것만 선언함.
         * 추가 필요한것은 리스트를 보고 생성한다.
        [0]: {nbf: 1575042197}
        [1]: {exp: 1575042257}
        [2]: {iss: http://localhost:9378}
        [3]: {aud: http://localhost:9378/resources}
        [4]: {aud: dataEventRecords}
        [5]: {client_id: resourceownerclient}
        [6]: {http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier: 1}
        [7]: {auth_time: 1575042197}
        [8]: {http://schemas.microsoft.com/identity/claims/identityprovider: local}
        [9]: {http://schemas.microsoft.com/ws/2008/06/identity/claims/role: dataEventRecords.admin}
        [10]: {http://schemas.microsoft.com/ws/2008/06/identity/claims/role: dataEventRecords.user}
        [11]: {id: 1}
        [12]: {http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress: test01@email.net}
        [13]: {scope: dataEventRecords}
        [14]: {scope: offline_access}
        [15]: {http://schemas.microsoft.com/claims/authnmethodsreferences: pwd}
         */

        /// <summary>
        /// 클라이언트 아이디
        /// </summary>
        public string client_id { get; set; }
        /// <summary>
        /// 권한 유효 시간
        /// </summary>
        public string auth_time { get; set; }
        /// <summary>
        /// 아이디
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 아이디 - long 타입
        /// </summary>
        public long id_int
        {
            get
            {
                return Convert.ToInt64(id);
            }
            set
            {
                this.id = value.ToString();
            }
        }

        /// <summary>
        /// 이메일.
        /// 인증에 사용한 이메일 정보.
        /// 이메일이 아닌경우 고유문자열
        /// </summary>
        public string email { get; set; }

        /// <summary>
        /// 기본값으로 생성
        /// </summary>
        public ClaimModel()
        {
            this.client_id = string.Empty;
            this.auth_time = string.Empty;
            this.id = string.Empty;
            this.email = string.Empty;
        }

        /// <summary>
        /// 인증 서버에서 넘어온 정보로 생성
        /// </summary>
        /// <param name="claims"></param>
        public ClaimModel(IEnumerable<Claim> claims )
        {
            this.Set(claims);
        }

        /// <summary>
        /// 인증 서버에서 넘어온 정보로 초기화
        /// </summary>
        /// <param name="claims"></param>
        public void Set(IEnumerable<Claim> claims)
        {
            //리스트 숫자
            Claim[] arrClaims = claims.ToArray();

            for (int i = 0; i < arrClaims.Length; ++i)
            {
                Claim claimItem = arrClaims[i];

                switch(claimItem.Type)
                {
                    case "iclient_id":
                        this.client_id = claimItem.Value;
                        break;
                    case "auth_time":
                        this.auth_time = claimItem.Value;
                        break;
                    case "id":
                        this.id = claimItem.Value;
                        break;
                    case "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress":
                        this.email = claimItem.Value;
                        break;


                    default:
                        break;
                }

                
            }
            
        }

    }
}
