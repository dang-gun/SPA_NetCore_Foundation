

namespace BoardModel
{
    /// <summary>
    /// 게시판의 리스트 아이템의 타입
    /// </summary>
    public enum BoardItemType
    {
        /// <summary>
        /// 없음. 기본
        /// </summary>
        None = 0,
        
        /// <summary>
        /// 공지
        /// </summary>
        NoticeAll = 1,

        /// <summary>
        /// 공지(그룹)
        /// </summary>
        NoticeGroup = 2,
        /// <summary>
        /// 공지(게시판)
        /// </summary>
        NoticeBoard = 3,
    }
}
