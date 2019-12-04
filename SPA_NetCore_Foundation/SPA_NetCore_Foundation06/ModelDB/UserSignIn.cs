using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPA_NetCore_Foundation.ModelDB
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
    }
}
