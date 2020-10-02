using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ModelDB;
using ApiModel;
using FileList.Model;

namespace Boards.Model
{
    /// <summary>
    /// 게시물 수정 요청시 리턴용
    /// </summary>
    public class BoardPostEditResultModel : ApiResultBaseModel
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
        /// 포스트 상태
        /// </summary>
        public BoardPostStateType PostState { get; set; }

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
        /// 내용
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 파일 정보 리스트
        /// </summary>
        public List<FileInfoModel> FileInfoList { get; set; }



        #region 작성용 정보들
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
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public BoardPostEditResultModel()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bpData"></param>
        /// <param name="uiData"></param>
        /// <param name="bcData"></param>
        public BoardPostEditResultModel(BoardPost bpData
            , UserInfo uiData
            , BoardContent bcData)
        {
            this.Reset(bpData, uiData, bcData);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bpData"></param>
        /// <param name="uiData"></param>
        /// <param name="bcData"></param>
        public void Reset(BoardPost bpData
            , UserInfo uiData
            , BoardContent bcData)
        {
            this.idBoardPost = bpData.idBoardPost;
            this.idBoard = bpData.idBoard;
            this.idBoardCategory = bpData.idBoardCategory;
            this.Title = bpData.Title;
            this.idUser = bpData.idUser;
            this.ViewCount = bpData.ViewCount;

            this.PostState = bpData.PostState;
            this.WriteDate = bpData.WriteDate;
            this.EditDate = bpData.EditDate;


            this.UserName = uiData.ViewName;


            this.Content = bcData.Content;


            this.FileInfoList = new List<FileInfoModel>();
        }

    }
}
