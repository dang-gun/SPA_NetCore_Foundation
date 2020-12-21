
namespace BoardModel
{
    /// <summary>
    /// 유저에게 미리 보낼 게시판 정보 리스트
    /// </summary>
    public class BoardListCallModel
    {
        /// <summary>
        /// 소속 보드
        /// </summary>
        public long nBoardId { get; set; }

        /// <summary>
        /// 소속 보드
        /// </summary>
        public long nPostView { get; set; }

        /// <summary>
        /// 한화면에 보일 컨탠츠 개수.
        /// 사용자 호출이 허용된 게시판만 사용가능.
        /// </summary>
        public int nShowCount { get; set; }

        /// <summary>
        /// 보려고하는 페이지 번호
        /// </summary>
        public int nPageNumber { get; set; }

    }
}
