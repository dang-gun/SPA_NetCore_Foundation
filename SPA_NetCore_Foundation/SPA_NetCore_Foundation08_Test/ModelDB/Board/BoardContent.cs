using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelDB
{
    /// <summary>
    /// 게시물 상세 정보
    /// </summary>
    public class BoardContent
    {
        /// <summary>
        /// 고유키
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idBoardContent { get; set; }

        /// <summary>
        /// 연결된 보드 키
        /// </summary>
        public long idBoard { get; set; }

        /// <summary>
        /// 연결된 포스트 번호
        /// </summary>
        public long idBoardPost { get; set; }

        /// <summary>
        /// 내용
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 작성시 ip
        /// </summary>
        [MaxLength(256)]
        public string WriteIP { get; set; }

        /// <summary>
        /// 수정시 ip
        /// </summary>
        [MaxLength(256)]
        public string EditIP { get; set; }

        /// <summary>
        /// 본문에 가지고 있는 파일 리스트.
        /// 구분자로 구분되어 있는 문자열이다.
        /// </summary>
        public string FileList { get; set; }
    }
}
