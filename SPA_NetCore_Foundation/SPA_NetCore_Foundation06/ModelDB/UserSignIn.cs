using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelDB
{
    /// <summary>
    /// 유저 사인인 한 유저의 정보.
    /// </summary>
    public class UserSignIn
    {
        /// <summary>
        /// 고유키
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idUserSignIn { get; set; }

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
        /// 직접 로그인한 시간
        /// </summary>
        public DateTime SignInDate { get; set; }
        /// <summary>
        /// 토큰 갱신 시간
        /// </summary>
        public DateTime RefreshDate { get; set; }
    }
}
