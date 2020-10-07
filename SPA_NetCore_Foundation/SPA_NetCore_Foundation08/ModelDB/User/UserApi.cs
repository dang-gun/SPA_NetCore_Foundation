using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelDB
{
    /// <summary>
    /// 유저 API 정보
    /// 외부에서 api를 사용할때 필요한 정보
    /// </summary>
    public class UserApi
    {
        /// <summary>
        /// 고유키
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idUserApi { get; set; }

        /// <summary>
        /// 사인인에 사용하는 키
        /// </summary>
        [MaxLength(64)]
        public string ApiKey { get; set; }

        /// <summary>
        /// 사용 계정
        /// </summary>
        public long idUser { get; set; }

        /// <summary>
        /// 상태
        /// </summary>
        public UserApiStateType UserApiState { get; set; }

        /// <summary>
        /// 시작 날짜
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// 끝 날짜(만료 예정일)
        /// </summary>
        public DateTime EndDate { get; set; }
    }
}
