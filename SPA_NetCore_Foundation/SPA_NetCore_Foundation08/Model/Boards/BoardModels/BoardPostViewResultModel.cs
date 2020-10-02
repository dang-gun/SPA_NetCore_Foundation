using ModelDB;
using ApiModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boards.Model
{
    /// <summary>
    /// 게시물 보기용 모델
    /// </summary>
    public class BoardPostViewResultModel : ApiResultBaseModel
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
        /// 전달한 유저.
        /// 이 글을 다른 사람이 작성해서 소유 유저에게 전달한다.
        /// 0이거나 없으면 소유자가 작성한 글이다.
        /// </summary>
        public long idUser_Forwarding { get; set; }


        /// <summary>
        /// 조회수
        /// </summary>
        public long ViewCount { get; set; }
        /// <summary>
        /// 조회수(비회원)
        /// </summary>
        public long ViewCountNone { get; set; }

        /// <summary>
        /// 작성일
        /// </summary>
        public DateTime WriteDate { get; set; }
        /// <summary>
        /// 수정일
        /// </summary>
        public DateTime EditDate { get; set; }


        /// <summary>
        /// 유저 이름
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 포워딩 유저 이름
        /// </summary>
        public string UserName_Forwarding { get; set; }


        /// <summary>
        /// 내용
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 수정권한이 있는지 여부
        /// </summary>
        public bool EditAuth { get; set; }

        /// <summary>
        /// 삭제권한이 있는지 여부
        /// </summary>
        public bool DeleteAuth { get; set; }


        /// <summary>
        /// 리플 리스트 표시 여부
        /// </summary>
        public bool ReplyList { get; set; }
        /// <summary>
        /// 리플 쓰기 권한이 있는지 여부
        /// </summary>
        public bool ReplyWrite { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public BoardPostViewResultModel()
        {
            this.ReplyList = false;
            this.ReplyWrite = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bpData"></param>
        /// <param name="uiData"></param>
        /// <param name="uiData_Forwarding"></param>
        /// <param name="bcData"></param>
        public BoardPostViewResultModel(BoardPost bpData
            , UserInfo uiData
            , UserInfo uiData_Forwarding
            , BoardContent bcData)
        {
            this.ReplyList = false;
            this.ReplyWrite = false;

            this.Reset(bpData, uiData, uiData_Forwarding, bcData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bpData"></param>
        /// <param name="uiData"></param>
        /// <param name="uiData_Forwarding"></param>
        /// <param name="bcData"></param>
        public void Reset(BoardPost bpData
            , UserInfo uiData
            , UserInfo uiData_Forwarding
            , BoardContent bcData)
        {
            this.idBoardPost = bpData.idBoardPost;
            this.idBoard = bpData.idBoard;
            this.idBoardCategory = bpData.idBoardCategory;
            this.Title = bpData.Title;
            this.idUser = bpData.idUser;
            this.idUser_Forwarding = bpData.idUser_Forwarding;
            this.ViewCount = bpData.ViewCount;
            this.ViewCountNone = bpData.ViewCountNone;
            this.WriteDate = bpData.WriteDate;
            this.EditDate = bpData.EditDate;


            this.UserName = uiData.ViewName;
            if(null != uiData_Forwarding)
            {
                this.UserName_Forwarding = uiData_Forwarding.ViewName;
            }
            


            this.Content = bcData.Content;
        }

    }
}
