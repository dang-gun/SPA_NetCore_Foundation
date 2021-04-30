using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelDB
{
    /// <summary>
    /// 유저 사인 타입
    /// </summary>
    [Flags]
    public enum UserSignLogType
    {
        /// <summary>
        /// 없음
        /// </summary>
        None = 0,

        /// <summary>
        /// 인증서버에 인증 요청
        /// </summary>
        RequestToken = 2001,
        /// <summary>
        /// 액세스 토큰 갱신
        /// </summary>
        RefreshToken = 2002,
        /// <summary>
        /// 지정된 토큰 제거
        /// </summary>
        RevocationToken = 2003,
        /// <summary>
        /// 엑세스토큰을 이용하여 유저 정보를 받는다.
        /// </summary>
        UserInfo = 2004,


        /// <summary>
        /// 직접 사인인 시도
        /// </summary>
        SignIn = 3001,
        /// <summary>
        /// 직접 사인아웃 시도
        /// </summary>
        SignOut = 3002,

    }
}
