

using ModelDB;

namespace BoardModel
{
 
    /// <summary>
    /// 게시판 카테고리를 유저에게 제공하기위한 모델
    /// </summary>
    public class BoardCategoryModel
    {
        /// <summary>
        /// 고유키
        /// </summary>
        public long idBoardCategory { get; set; }

        /// <summary>
        /// 카테고리 타이틀
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public BoardCategoryModel()
        {

        }

        /// <summary>
        /// 카테고리 데이터를 이용하여 생성
        /// </summary>
        /// <param name="dataBoardCategory"></param>
        public BoardCategoryModel(BoardCategory dataBoardCategory)
        {
            this.idBoardCategory = dataBoardCategory.idBoardCategory;
            this.Title = dataBoardCategory.Title;
        }
    }
}
