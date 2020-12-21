using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelDB
{
    /// <summary>
    /// API 요청 로그
    /// </summary>
    public class ApiLog
    {
        /// <summary>
        /// 고유키
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idApiLog { get; set; }

        /// <summary>
        /// 사인인에 사용하는 키
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// 사용 계정
        /// </summary>
        public long idUser { get; set; }

        /// <summary>
        /// 호출 날짜
        /// </summary>
        public DateTime CallDate { get; set; }

        /// <summary>
        /// 분류 단계01
        /// </summary>
        [MaxLength(512)]
        public string Step01 { get; set; }

        /// <summary>
        /// 호출 내용
        /// </summary>
        public string Contents { get; set; }
    }
}
