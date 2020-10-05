using ModelDB;
using ApiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardModel
{
    /// <summary>
    /// 게시물 보기시 댓글 리스트 전달용
    /// </summary>
    public class BoardPostViewReplyResultModel : ApiResultBaseModel
    {
        /// <summary>
        /// 리플 리스트
        /// </summary>
        public List<BoardPostViewReplyModel> List { get; set; }

        /// <summary>
        /// 리플 작성 권한이 있는지 여부
        /// </summary>
        public bool WriteReply { get; set; }

    }
}
