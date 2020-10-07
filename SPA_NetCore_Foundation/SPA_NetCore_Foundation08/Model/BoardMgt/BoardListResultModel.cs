using ModelDB;
using ApiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardMgtModel
{
    /// <summary>
    /// 게시판 리스트 전달용
    /// </summary>
    public class BoardListResultModel : ApiResultBaseModel
    {
        /// <summary>
        /// 게시판 리스트
        /// </summary>
        public List<Board> List { get; set; }

    }
}
