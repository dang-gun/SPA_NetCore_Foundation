using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ModelDB
{
    /// <summary>
    /// 게시물의 댓글
    /// </summary>
    public class BoardPostReply
    {
        /// <summary>
        /// 고유키
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long idBoardPostReply { get; set; }

        /// <summary>
        /// 연결된 보드 아이디
        /// </summary>
        public long idBoard { get; set; }
        /// <summary>
        /// 연결된 포스트 아이디
        /// </summary>
        public long idBoardPost { get; set; }


        /// <summary>
        /// 다른 리플을 대상으로 했을 경우 해당 리플의 아이디.
        /// 없으면 0.
        /// </summary>
        public long idBoardPostReply_Re { get; set; }

        /// <summary>
        /// 다른 리플을 대상으로 했을 경우 최상위 댓글 아이디.
        /// 최상위에 idBoardReply_Re가 0인 댓글을 찾아 넣어준다.
        /// </summary>
        public long idBoardPostReply_ReParent { get; set; }

        /// <summary>
        /// 이 댓글 아래에 있는 댓글 개수
        /// </summary>
        public int ReReplyCount { get; set; }



        /// <summary>
        /// 작성자 아이디
        /// </summary>
        public long idUser { get; set; }

        #region 비회원 추가 정보
        /// <summary>
        /// 비회원 - 표시 이름
        /// </summary>
        [MaxLength(32)]
        public string NonMember_ViewName { get; set; }
        /// <summary>
        /// 비회원 - 비밀번호
        /// </summary>
        [MaxLength(64)]
        public string NonMember_Password { get; set; }
        #endregion


        /// <summary>
        /// 리플 상태
        /// </summary>
        public BoardPostReplyStateType ReplyState { get; set; }

        /// <summary>
        /// 타이틀
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 내용
        /// </summary>
        public string Content { get; set; }

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
