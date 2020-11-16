using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelDB
{
    /// <summary>
    /// 게시물 기본 정보
    /// </summary>
    public class BoardPost
    {
        /// <summary>
        /// 고유키
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idBoardPost { get; set; }

        /// <summary>
        /// 소속 보드
        /// </summary>
        public long idBoard { get; set; }


        /// <summary>
        /// 카테고리
        /// </summary>
        public long idBoardCategory { get; set; }

        /// <summary>
        /// 제목
        /// </summary>
        [MaxLength(128)]
        public string Title { get; set; }

        /// <summary>
        /// 소유 유저
        /// </summary>
        public long idUser { get; set; }

        /// <summary>
        /// 조회수
        /// </summary>
        public long ViewCount { get; set; }
        /// <summary>
        /// 조회수(비회원 조회수)
        /// </summary>
        public long ViewCountNone { get; set; }
        /// <summary>
        /// 댓글 갯수
        /// </summary>
        public int ReplyCount { get; set; }

        /// <summary>
        /// 썸네일이 있으면 이미지 주소.
        /// 파일명은 빠저있다.
        /// </summary>
        public string ThumbnailUrl { get; set; }

        /// <summary>
        /// 포스트 상태
        /// </summary>
        public BoardPostStateType PostState { get; set; }

        /// <summary>
        /// 작성일
        /// </summary>
        public DateTime WriteDate { get; set; }
        /// <summary>
        /// 수정일
        /// </summary>
        public DateTime EditDate { get; set; }
        /// <summary>
        /// 삭제/블럭일
        /// </summary>
        public DateTime DeleteDate { get; set; }

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
    }
}
