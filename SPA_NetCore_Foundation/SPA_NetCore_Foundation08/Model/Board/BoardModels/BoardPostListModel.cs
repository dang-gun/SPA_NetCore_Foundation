using ModelDB;
using ApiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BoardModel
{
    
    /// <summary>
    /// 게시물 리스트용 아이템
    /// </summary>
    public class BoardPostListModel
    {
        /// <summary>
        /// 고유키
        /// </summary>
        public long idBoardPost { get; set; }

        /// <summary>
        /// 소속 보드
        /// </summary>
        public long idBoard { get; set; }

        /// <summary>
        /// 카테고리
        /// </summary>
        public long idBoardCategory { get; set; }

        /// <summary>
        /// 제목
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 소유 유저
        /// </summary>
        public long idUser { get; set; }


        /// <summary>
        /// 조회수
        /// </summary>
        public long ViewCount { get; set; }
        /// <summary>
        /// 조회수(비회원)
        /// </summary>
        public long ViewCountNone { get; set; }
        /// <summary>
        /// 댓글 갯수
        /// </summary>
        public int ReplyCount { get; set; }

        /// <summary>
        /// 썸네일이 있는지 여부
        /// </summary>
        public string ThumbnailUrl { get; set; }

        /// <summary>
        /// 작성일
        /// </summary>
        public DateTime WriteDate { get; set; }
        /// <summary>
        /// 수정일
        /// </summary>
        public DateTime EditDate { get; set; }


        /// <summary>
        /// 작성자 이름
        /// </summary>
        public string UserName { get; set; }


        /// <summary>
        /// 포스트 상태
        /// </summary>
        public BoardPostStateType PostState { get; set; }

        /// <summary>
        /// 아이템 타입
        /// </summary>
        public BoardItemType ItemType { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public BoardPostListModel()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bpData"></param>
        /// <param name="uiData"></param>
        /// <param name="typeBoardItem"></param>
        public BoardPostListModel(BoardPost bpData
            , UserInfo uiData
            , BoardItemType typeBoardItem)
        {
            this.Reset(bpData
                , uiData
                , typeBoardItem);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="bpData"></param>
        /// <param name="uiData"></param>
        /// <param name="typeBoardItem"></param>
        public void Reset(BoardPost bpData
            , UserInfo uiData
            , BoardItemType typeBoardItem)
        {
            this.idBoardPost = bpData.idBoardPost;
            this.idBoard = bpData.idBoard;
            this.idBoardCategory = bpData.idBoardCategory;
            this.Title = bpData.Title;
            this.idUser = bpData.idUser;
            this.ViewCount = bpData.ViewCount;
            this.ViewCountNone = bpData.ViewCountNone;
            this.ReplyCount = bpData.ReplyCount;
            this.ThumbnailUrl = bpData.ThumbnailUrl;
            this.PostState = bpData.PostState;
            this.WriteDate = bpData.WriteDate;
            this.EditDate = bpData.EditDate;

            this.UserName = uiData.ViewName;

            this.ItemType = typeBoardItem;
        }

    }
}
