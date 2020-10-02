using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelDB
{
    /// <summary>
    /// 
    /// 인덱스 1은 무조건 전체 표시용이다.
    /// </summary>
    public class BoardGroup
    {
        /// <summary>
        /// 고유키
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idBoardGroup { get; set; }

        /// <summary>
        /// 카테고리 타이틀
        /// </summary>
        [MaxLength(64)]
        public string Title { get; set; }

        /// <summary>
        /// 설명
        /// </summary>
        public string Memo { get; set; }
    }
}
