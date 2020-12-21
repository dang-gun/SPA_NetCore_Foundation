using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ModelDB
{
    /// <summary>
    /// 게시판 기본 정보
    /// </summary>
    public class Board
    {
        /// <summary>
        /// 고유키
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idBoard { get; set; }

        /// <summary>
        /// 게시판 제목
        /// </summary>
        [MaxLength(128)]
        public string Title { get; set; }

        /// <summary>
        /// 기준 Url
        /// </summary>
        [MaxLength(128)]
        public string UrlStandard { get; set; }

        /// <summary>
        /// 보드 상태
        /// </summary>

        public BoardStateType BoardState { get; set; }

        /// <summary>
        /// 게시판 기능 설정
        /// </summary>
        public BoardFacultyType BoardFaculty { get; set; }

        /// <summary>
        /// 권한이 없을때 사용하는 기본권한
        /// </summary>
        public BoardAuthorityType AuthorityDefault { get; set; }

        

        /// <summary>
        /// 한페이지 컨탠츠 수.
        /// BoardFaculty의 ShowCount_Server를 사용할때 사용할 정보.
        /// </summary>
        [DefaultValue(10)]
        public Int16 ShowCount { get; set; }

        /// <summary>
        /// 소속 그룹 아이디
        /// </summary>
        public long idBoardGroup { get; set; }


        /// <summary>
        /// 작성일
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 설명
        /// </summary>
        public string Memo { get; set; }

    }
}
