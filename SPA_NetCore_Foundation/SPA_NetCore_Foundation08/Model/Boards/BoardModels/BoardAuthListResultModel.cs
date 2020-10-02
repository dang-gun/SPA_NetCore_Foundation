using ModelDB;
using ApiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boards.Model
{
    
    /// <summary>
    /// 게시판 권한 리스트
    /// </summary>
    public class BoardAuthListResultModel : ApiResultBaseModel
    {
        /// <summary>
        /// 해당 게시판에 연결된 권한 리스트
        /// </summary>
        public List<BoardAuthItemModel> List { get; set; }

    }
}
