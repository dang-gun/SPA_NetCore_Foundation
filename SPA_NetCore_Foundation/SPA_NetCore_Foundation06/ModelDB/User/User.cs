using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelDB
{
    /// <summary>
    /// 유저 사인인 정보.
    /// 유저기준 정보임.
    /// </summary>
    public class User
    {
        /// <summary>
        /// 고유키
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idUser { get; set; }

        /// <summary>
        /// 사인인에 사용하는 이메일
        /// </summary>
        public string SignEmail { get; set; }

        /// <summary>
        /// 비밀번호
        /// </summary>
        public string Password { get; set; }
    }
}
