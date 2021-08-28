using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelDB
{
    /// <summary>
    /// 멀티 사인을 어떻게 허용할지 여부
    /// </summary>
    [Flags]
    public enum UserSignMultiType
    {
        /// <summary>
        /// 없음.
        /// 절대 사용하면 안된다.
        /// </summary>
        None = 0,

        /// <summary>
        /// 무조건 하나만 허용함.
        /// 쿠키가 공유되는(같은 브라우저) 경우 여러창이 허용된다.
        /// 그외에는 플랫폼이나 브라우저와 상관없이 모두 하나의 토큰만 허용되므로
        /// 다른 브라우저에서 토큰을 갱신하면 기존토큰은 제거된다.
        /// </summary>
        OnlyOne = 100,

        /// <summary>
        /// 플랫폼당 하나씩 허용.
        /// PC, 모바일 하나씩만 허용함.
        /// </summary>
        Platform = 200,

        /// <summary>
        /// 브라우저당 하나씩 허용.
        /// 프론트엔드에서 최초 접속시 GUID를 생성하여 쿠키에 저장하고
        /// 이것을 기준으로 토큰을 관리한다.
        /// </summary>
        PerBrowser = 300,

        /// <summary>
        /// IP 하나당 하나씩 허용.
        /// 모바일의 경우 빈번하게 IP가 변경되기 때문에 
        /// </summary>
        IP = 400,
    }
}
