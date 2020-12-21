using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelDB
{
    /// <summary>
    /// 게시판별 카테고리.
    /// 인덱스 1은 무조건 전체 표시용이다.
    /// </summary>
    public class BoardCategory
    {
        /// <summary>
        /// 고유키
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idBoardCategory { get; set; }

        /// <summary>
        /// 연결된 게시판
        /// </summary>
        public long idBoard { get; set; }

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
