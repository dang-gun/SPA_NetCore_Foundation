using ModelDB;
using ApiModel;

namespace BoardModel
{
    /// <summary>
    /// 게시물 작성 요청 완료용
    /// </summary>
    public class BoardPostCreateResultModel : ApiResultBaseModel
    {
        /// <summary>
        /// 선택된 포스트 아이디
        /// </summary>
        public long PostID { get; set; }

    }
}
