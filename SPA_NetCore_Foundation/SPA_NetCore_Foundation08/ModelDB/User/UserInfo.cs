using System;
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
        /// 관리 등급
        /// </summary>
        public ManagementClassType MgtClass { get; set; }
        /// <summary>
        /// 부모의 고유키
        /// 관리 등급이 직원이면 위임받은 부모의 고유키를 넣는다.
        /// </summary>
        public long idUser_Parent { get; set; }

        /// <summary>
        /// 가입일
        /// </summary>
        public DateTime SignUpDate { get; set; }

        /// <summary>
        /// 직접 로그인한 마지막 날짜
        /// </summary>
        public DateTime SignInDate { get; set; }
        /// <summary>
        /// 토큰으로 로그인한 마지막 날짜
        /// </summary>
        public DateTime RefreshDate { get; set; }

        /// <summary>
        /// 접속 플랫폼
        /// </summary>
        public string PlatformInfo { get; set; }

    }
}
