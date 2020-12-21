
using ModelDB;

namespace BoardModel
{
 
    /// <summary>
    /// 게시판 리스트 정보.
    /// 게시판의 기본 연결 주소를 유저에게 전달하기위한 모델이다.
    /// </summary>
    public class BoardListInfoModel
    {
        /// <summary>
        /// 소속 보드
        /// </summary>
        public long idBoard { get; set; }

        /// <summary>
        /// 게시판 이름
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 기준 Url
        /// </summary>
        public string UrlStandard { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbBoard"></param>
        public BoardListInfoModel(Board dbBoard)
        {
            this.idBoard = dbBoard.idBoard;
            this.Title = dbBoard.Title;
            this.UrlStandard = dbBoard.UrlStandard;
        }
    }
}
