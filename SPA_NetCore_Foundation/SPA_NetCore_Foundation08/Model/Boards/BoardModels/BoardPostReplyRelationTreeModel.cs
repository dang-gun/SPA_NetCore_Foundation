using ModelDB;
using ApiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boards.Model
{
    
    /// <summary>
    /// 댓글 관계정보
    /// </summary>
    public class BoardPostReplyRelationTreeModel
    {
        /// <summary>
        /// 댓글 고유번호
        /// </summary>
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
