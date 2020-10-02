using ModelDB;
using ApiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boards.Model
{
    /// <summary>
    /// 게시물 리스트 정보
    /// </summary>
    public class BoardPostResultModel : ApiResultBaseModel
    {
        /// <summary>
        /// 검색된 게시물 리스트
        /// </summary>
        public List<BoardPostListModel> List { get; set; }


        /// <summary>
        /// 지정한 보드 id
        /// </summary>
        public long idBoard { get; set; }
        /// <summary>
        /// 선택한 포스트 id
        /// </summary>
        public long idBoardPost { get; set; }

        /// <summary>
        /// 소속된 보드의 게시물 숫자
        /// </summary>
        public long TotalCount { get; set; }

        /// <summary>
        /// 지금 보고 있는 페이지 번호
        /// </summary>
        public int PageNumber  { get; set; }

        /// <summary>
        /// 한페이지의 컨탠츠 개수
        /// </summary>
        public int ShowCount { get; set; }


    }
}
