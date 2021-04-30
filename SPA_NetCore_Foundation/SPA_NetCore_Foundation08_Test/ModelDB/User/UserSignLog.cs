using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelDB
{
    /// <summary>
    /// 사인 관련 로그
    /// </summary>
    public class UserSignLog
    {
        /// <summary>
        /// 고유키
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idUserSignLog { get; set; }

        /// <summary>
        /// 작성일
        /// </summary>
        public DateTime AddDate { get; set; }

        /// <summary>
        /// 로그 레벨
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 사인 로그 타입
        /// </summary>
        public UserSignLogType SignLogType { get; set; }

        /// <summary>
        /// 대상 유저.
        /// 없으면 0
        /// </summary>
        public long idUser { get; set; }




        /// <summary>
        /// 내용
        /// </summary>
        public string Contents { get; set; }
    }
}
