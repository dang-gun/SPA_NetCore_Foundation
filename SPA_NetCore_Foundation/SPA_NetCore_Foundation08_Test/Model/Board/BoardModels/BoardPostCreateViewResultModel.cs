using ModelDB;
using ApiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardModel
{
    /// <summary>
    /// 게시물 작성 화면 요청시 리턴용
    /// </summary>
    public class BoardPostCreateViewResultModel : ApiResultBaseModel
    {
        /// <summary>
        /// 이 게시판이 가지고 있는 카테고리
        /// </summary>
        public List<BoardCategoryModel> BoardCategory { get; set; }

        /// <summary>
        /// 공지 권한 - 전체
        /// </summary>
        public bool NoticeAll { get; set; }
        /// <summary>
        /// 공지 권한 - 그룹
        /// </summary>
        public bool NoticeGroup { get; set; }
        /// <summary>
        /// 공지 권한 - 게시판
        /// </summary>
        public bool NoticeBoard { get; set; }
    }
}
