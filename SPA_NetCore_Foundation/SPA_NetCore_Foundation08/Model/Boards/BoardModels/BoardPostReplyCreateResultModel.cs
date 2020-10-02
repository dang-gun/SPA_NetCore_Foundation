using ModelDB;
using ApiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boards.Model
{
    /// <summary>
    /// 리플 작성 완료 리절트 모델
    /// </summary>
    public class BoardPostReplyCreateResultModel : ApiResultBaseModel
    {
        /// <summary>
        /// 완성된 리플 개체
        /// </summary>
        public BoardPostViewReplyModel NewItem { get; set; }
    }
}
