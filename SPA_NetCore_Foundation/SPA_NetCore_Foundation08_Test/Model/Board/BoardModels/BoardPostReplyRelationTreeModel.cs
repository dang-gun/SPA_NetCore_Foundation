﻿
using System.ComponentModel.DataAnnotations;

namespace BoardModel
{

    /// <summary>
    /// 댓글 관계정보
    /// </summary>
    public class BoardPostReplyRelationTreeModel
    {
        /// <summary>
        /// 댓글 고유번호
        /// </summary>
        [Key]
        public long idBoardPostReply { get; set; }
        /// <summary>
        /// 댓글 대상
        /// </summary>
        public long idBoardPostReply_Re { get; set; }
        /// <summary>
        /// 깊이
        /// </summary>
        public int Depth { get; set; }

    }
}
