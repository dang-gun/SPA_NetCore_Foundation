using System;
using System.Collections.Generic;
using System.Text;

namespace BoardModel
{
    /// <summary>
    /// 게시판 리스트 공통 맴버
    /// </summary>
    public interface BoardInterface
    {
        /// <summary>
        /// 검색된 게시물 숫자
        /// </summary>
        public long TotalCount { get; set; }

        /// <summary>
        /// 지금 보고 있는 페이지 번호
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// 한페이지의 컨탠츠 개수
        /// </summary>
        public int ShowCount { get; set; }
    }
}
