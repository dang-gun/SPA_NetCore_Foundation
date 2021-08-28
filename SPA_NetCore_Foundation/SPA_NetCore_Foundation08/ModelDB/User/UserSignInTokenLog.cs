using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelDB
{
    /// <summary>
    /// 말료된 리플레시토큰을 저장해 둔다.
    /// 'UserSignIn'와 분리한 이유는 만료되지 않은 토큰을 빠르게 검색하기 위함이다.
    /// </summary>
    public class UserSignInTokenLog
    {
        /// <summary>
        /// 고유키
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idUserSignInTokenLog { get; set; }

        /// <summary>
        /// 연결된 User.idUser
        /// </summary>
        [Required]
        public long idUser { get; set; }

        /// <summary>
        /// 리플레시 토큰
        /// </summary>
        public string RefreshToken { get; set; }


        /// <summary>
        /// 만료 당시 플랫폼
        /// </summary>
        public PlatformType PlatformType { get; set; }
        /// <summary>
        /// 만료 당시 ip
        /// IPv6 대비하여 150칸 지정
        /// </summary>
        [MaxLength(150)]
        public string IP { get; set; }
        /// <summary>
        /// 브라우저에서 생성한 고유값
        /// </summary>
        [MaxLength(36)]
        public string GUID { get; set; }


        /// <summary>
        /// 만료 시간
        /// </summary>
        public DateTime EndDate { get; set; }

    }
}
