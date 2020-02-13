using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelDB
{
    /// <summary>
    /// 유저 사인인 정보.
    /// 유저기준 정보임.
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// 고유키
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idUserInfo { get; set; }

        /// <summary>
        /// 연결된 유저 고유키
        /// </summary>
        public long idUser { get; set; }

        /// <summary>
        /// 표시용 이름
        /// </summary>
        [MaxLength(16)]
        public string ViewName { get; set; }

        /// <summary>
        /// 관리 권한
        /// </summary>
        public ManagerPermissionType ManagerPermission { get; set; }
    }
}
