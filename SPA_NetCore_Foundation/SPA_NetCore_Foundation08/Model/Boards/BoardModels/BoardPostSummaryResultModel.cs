using ModelDB;
using ApiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boards.Model
{
    
    /// <summary>
    /// 요약 게시판 게시물 리스트
    /// </summary>
    public class BoardPostSummaryResultModel : ApiResultBaseModel
    {
        /// <summary>
        /// 게시물 리스트
        /// </summary>
        public List<BoardPostListModel> List { get; set; }
        
        /// <summary>
        /// 게시판 제목
        /// </summary>
        public string BoardTitle { get; set; }
    }
}
