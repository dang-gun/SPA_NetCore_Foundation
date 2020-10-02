using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelDB
{
    /// <summary>
    /// 게시판 개별권한.
    /// 유저 기준으로
    /// </summary>
    public class BoardAuthority
    {
        /// <summary>
        /// 고유키
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idBoardAuthority { get; set; }

        /// <summary>
        /// 연결된 게시판의 고유번호
        /// </summary>
        public long idBoard { get; set; }

        /// <summary>
        /// 소유 유저
        /// </summary>
        public long idUser { get; set; }

        /// <summary>
        /// 가지고 있는 권한.
        /// </summary>
        public BoardAuthorityType Authority { get; set; }
        /// <summary>
        /// 권한 상태
        /// </summary>
        public BoardAuthorityStateType AuthState { get; set; }

        /// <summary>
        /// 설명
        /// </summary>
        public string Memo { get; set; }
        
        /// <summary>
        /// 수정 날짜
        /// </summary>
        public DateTime EditDate { get; set; }
    }
}
