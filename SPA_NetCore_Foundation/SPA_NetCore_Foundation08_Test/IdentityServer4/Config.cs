using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.OpenApi.Writers;
using System.Collections.Generic;

namespace IdentityServer4_Custom.IdentityServer4
{
    /// <summary>
    /// 0. 'IdentityServer4' 설정
    /// </summary>
    public class Config
    {
        /// <summary>
        /// 신원 자원
        /// </summary>
        /// <returns></returns>
        public static List<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId()
            };
        }

        /// <summary>
        /// API의 인증 범위를 정의한다.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("customer", "Customer API")
                {
                    //ApiSecrets = { new Secret("dataEventRecordsSecret".Sha256()) }
                     Scopes = { "customer.read", "customer.contact" }
                }//end new ApiResource
            };//end return
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
             {
                 // customer API specific scopes
                new ApiScope(name: "customer.read",    displayName: "Reads you customers information."),
                new ApiScope(name: "customer.contact", displayName: "Allows contacting one of your customers."),
             };
        }

        /// <summary>
        /// 클라이언트 접근 범위를 설정한다.
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "resourceownerclient",

                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials
                    , AccessTokenType = AccessTokenType.Jwt
                    , AccessTokenLifetime = 60
                    , IdentityTokenLifetime = 3600
                    , UpdateAccessTokenClaimsOnRefresh = true
                    , SlidingRefreshTokenLifetime = 30
                    , AllowOfflineAccess = true
                    , RefreshTokenExpiration = TokenExpiration.Absolute
                    , RefreshTokenUsage = TokenUsage.OneTimeOnly
                    , AlwaysSendClientClaims = true
                    , Enabled = true
                    , ClientSecrets =  new List<Secret> { new Secret("dataEventRecordsSecret".Sha256()) }
                    , AllowedScopes = {
                        "customer"
                        , IdentityServerConstants.StandardScopes.OpenId
                        , IdentityServerConstants.StandardScopes.OfflineAccess
                        
                    }
                }//end new Client
            };//end return
        }


    }//end class Config
}
