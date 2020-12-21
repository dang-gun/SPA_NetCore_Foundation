using ModelDB;
using ApiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardModel
{
    
    /// <summary>
    /// 게시물 보기시 댓글 아이템 정보
    /// </summary>
    public class BoardPostViewReplyModel: BoardPostReply
    {
        
        /// <summary>
        /// 유저 이름
        /// </summary>
        public string UserName { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public BoardPostViewReplyModel()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uiData"></param>
        /// <param name="bcData"></param>
        public BoardPostViewReplyModel(
            UserInfo uiData
            , BoardPostReply bcData)
        {
            this.Reset(uiData, bcData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uiData"></param>
        /// <param name="bcData"></param>
        public void Reset(
            UserInfo uiData
            , BoardPostReply bcData)
        {
            base.idBoard = bcData.idBoard;
            base.idBoardPost = bcData.idBoardPost;
            base.idBoardPostReply = bcData.idBoardPostReply;
            base.idBoardPostReply_Re = bcData.idBoardPostReply_Re;
            base.idBoardPostReply_ReParent = bcData.idBoardPostReply_ReParent;

            base.Title = bcData.Title;
            base.idUser = bcData.idUser;
            if (null != uiData)
            {
                this.UserName = uiData.ViewName;
            }
            else
            {
                this.UserName = string.Empty;
            }

            base.NonMember_ViewName = bcData.NonMember_ViewName;

            base.ReplyState = bcData.ReplyState;

            base.WriteDate = bcData.WriteDate;
            base.EditDate = bcData.EditDate;

            base.ReReplyCount = bcData.ReReplyCount;



            base.Content = bcData.Content;
        }

        /// <summary>
        /// 리플의 상태값에 따라 다른 정보를 준다.
        /// </summary>
        /// <param name="uiData"></param>
        /// <param name="bcData"></param>
        public void Reset_ViewType(
            UserInfo uiData
            , BoardPostReply bcData)
        {
            base.idBoardPost = bcData.idBoardPost;
            base.idBoardPostReply = bcData.idBoardPostReply;
            base.idBoardPostReply_Re = bcData.idBoardPostReply_Re;
            base.idBoardPostReply_ReParent = bcData.idBoardPostReply_ReParent;

            if (BoardPostReplyStateType.Delete == bcData.ReplyState)
            {
                base.Title = "관리자에 의해 블럭된 글입니다.";
                base.Content = "관리자에 의해 블럭된 글입니다.";
            }
            else if (BoardPostReplyStateType.Delete == bcData.ReplyState)
            {
                base.Title = "사용자에 의해 삭제된 글입니다.";
                base.Content = "사용자에 의해 삭제된 글입니다.";
            }
            else
            {//정상
                base.Title = bcData.Title;
                base.Content = bcData.Content;
            }


            base.ReReplyCount = bcData.ReReplyCount;
            base.idUser = bcData.idUser;
            if (null != uiData)
            {
                this.UserName = uiData.ViewName;
            }
            else
            {
                this.UserName = string.Empty;
            }

            base.NonMember_ViewName = bcData.NonMember_ViewName;

            base.WriteDate = bcData.WriteDate;
            base.EditDate = bcData.EditDate;

        }

    }
}
