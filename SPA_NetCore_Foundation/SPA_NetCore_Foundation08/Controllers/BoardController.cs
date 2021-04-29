using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

using BoardModel;
using ApiModel;
using IdentityServer4.UserServices;
using System.Security.Claims;
using ModelDB;
using SPA_NetCore_Foundation.Global;
using SPA_NetCore_Foundation.Faculty;
using FileListModel;
using Microsoft.EntityFrameworkCore;
using Faculty;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using Newtonsoft.Json;
using HtmlAgilityPack;

namespace SPA_NetCore_Foundation.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BoardController : ControllerBase
    {
        #region 리스트 보기

        /// <summary>
        /// 대상 게시판의 리스트를 리턴한다. - 권한 전달 필수
        /// </summary>
        /// <param name="idBoard"></param>
        /// <param name="nShowCount">한화면에 보일 컨탠츠 개수. 사용자 선택이 허용된 게시판만 사용가능.</param>
        /// <param name="nPageNumber">보려고하는 페이지 번호</param>
        /// <param name="bAdminMode">관리자 모드 사용여부</param>
        /// <param name="sSearchWord">검색</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult<BoardPostResultModel> List_Auth(
            int idBoard
            , int nShowCount
            , int nPageNumber
            , bool bAdminMode
            , string sSearchWord)
        {
            return this.List(idBoard, nShowCount, nPageNumber, bAdminMode, sSearchWord);
        }

        /// <summary>
        /// 대상 게시판의 리스트를 리턴한다.
        /// </summary>
        /// <param name="idBoard"></param>
        /// <param name="nShowCount">한화면에 보일 컨탠츠 개수. 사용자 선택이 허용된 게시판만 사용가능.</param>
        /// <param name="nPageNumber">보려고하는 페이지 번호</param>
        /// <param name="bAdminMode">관리자 모드 사용여부</param>
        /// <param name="sSearchWord">검색 단어</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<BoardPostResultModel> List(
            int idBoard
            , int nShowCount
            , int nPageNumber
            , bool bAdminMode
            , string sSearchWord)
        {
            ApiResultReady rrResult = new ApiResultReady(this);
            BoardPostResultModel armResult = new BoardPostResultModel();
            rrResult.ResultObject = armResult;

            //입력값 검사
            if (0 >= nShowCount)
            {
                //쇼카운트의 최소값은 1이다.
                nShowCount = 1;
            }
            
            //페이지 넘버
            if(0 >= nPageNumber)
            {//0이하다
                //최소값 설정
                nPageNumber = 1;
            }


            //유저 정보 추출
            ClaimModel cm = new ClaimModel(((ClaimsIdentity)User.Identity).Claims);
            //찾은 게시판 권한
            BoardAuthorityType TypeBoardAuth;
            //권한 체크 결과
            BoardAuthCheckType typeCheck;
            //게시판 권한 찾기
            rrResult.Message
                = this.AuthGet(idBoard
                                , cm.id_int
                                , out TypeBoardAuth
                                , out typeCheck);

            if (BoardAuthCheckType.Success == typeCheck)
            {
                //권한 체크
                if ("0" == armResult.InfoCode)
                {
                    if ((0 >= cm.id_int)
                        && (false == TypeBoardAuth.HasFlag(BoardAuthorityType.ReadListNonMember)))
                    {//비회원인데
                     //리스트 보기(비회원) 권한이 없다.
                        armResult.InfoCode = "-2";
                        armResult.Message = "로그인해야 볼수 있는 게시판입니다.";
                    }
                    else if ((0 < cm.id_int)
                        && (false == TypeBoardAuth.HasFlag(BoardAuthorityType.ReadList)))
                    {//회원인데
                     //리스트 보기 권한이 없다.
                        armResult.InfoCode = "-3";
                        armResult.Message = "게시판을 볼 수 없습니다.";
                    }
                    else if ((true == bAdminMode)
                        && (ManagementClassCheckType.Ok 
                            != GlobalStatic.MgtA.MgtClassCheck(cm.id_int, ManagementClassType.Admin)))
                    {//관리자 모드인데
                        //관리자 권한이 없다.
                        armResult.InfoCode = "-4";
                        armResult.Message = "관리자 권한이 없습니다.";
                    }
                }

                if ("0" == armResult.InfoCode)
                {
                    using (SpaNetCoreFoundationContext db1 = new SpaNetCoreFoundationContext())
                    {
                        //게시판 정보 받기************************************
                        Board findBoard
                            = db1.Board
                                .Where(m => m.idBoard == idBoard)
                                .FirstOrDefault();


                        //한화면 컨탠츠 수
                        if (true == findBoard.BoardFaculty.HasFlag(BoardFacultyType.ShowCount_Server))
                        {//서버 설정 사용
                            armResult.ShowCount = findBoard.ShowCount;
                        }
                        else
                        {//유저 설정 사용
                            armResult.ShowCount = nShowCount;
                        }

                        //페이지 번호 입력
                        armResult.PageNumber = nPageNumber;



                        //게시물 검색***************************************
                        List<BoardPostListModel> listReturn = new List<BoardPostListModel>();


                        //공지 검색 - 전체 *************
                        BoardPostListModel[] bplNotice_All = null;
                        bplNotice_All
                            = (from bp in db1.BoardPost
                               from ui in db1.UserInfo
                                              .Where(a => a.idUser == bp.idUser)
                               where bp.PostState == BoardPostStateType.Notice_All
                               select new BoardPostListModel(bp, ui, BoardItemType.NoticeAll))
                            .ToArray();
                        listReturn.AddRange(bplNotice_All);

                        //공지 검색 - 게시판 *************
                        BoardPostListModel[] bplNotice_Board = null;
                        bplNotice_Board
                            = (from bp in db1.BoardPost
                               from ui in db1.UserInfo
                                              .Where(a => a.idUser == bp.idUser)
                               where bp.idBoard == idBoard
                                    && bp.PostState == BoardPostStateType.Notice_Board
                               select new BoardPostListModel(bp, ui, BoardItemType.NoticeBoard))
                            .ToArray();
                        //전체 공지 추가
                        listReturn.AddRange(bplNotice_Board);



                        //게시판 포스트 검색
                        IQueryable<BoardPost> iqBP
                             = db1.BoardPost
                                 .Where(m => m.idBoard == idBoard
                                        && (m.PostState != BoardPostStateType.Delete
                                            && m.PostState != BoardPostStateType.Block))
                                 .OrderByDescending(m => m.idBoardPost);


                        //검색어 체크*********************************
                        if (false == string.IsNullOrEmpty(sSearchWord))
                        {//검색어가 있다.
                            //컨탠츠와 엮어서 표시한다.
                            iqBP
                                = from bp in iqBP
                                  join bpc in db1.BoardContent
                                      on bp.idBoard equals bpc.idBoard
                                  where bp.Title.Contains(sSearchWord)
                                      || bpc.Content.Contains(sSearchWord)
                                  select bp;
                        }


                        //게시물 숫자
                        armResult.TotalCount = iqBP.Count();

                        //게시물 정리 검색**************************************
                        listReturn.AddRange(
                            (from bp in iqBP
                             from ui in db1.UserInfo
                                            .Where(a => a.idUser == bp.idUser)
                             select new BoardPostListModel(bp, ui, BoardItemType.None))
                              .Skip(armResult.ShowCount * (armResult.PageNumber - 1))
                              .Take(armResult.ShowCount)
                              .ToArray()
                              );

                        //완성된 리스트를 넘겨 준다
                        armResult.List = listReturn;


                    }//end using db1
                }

            }
            else
            {//오류가 있다.
                rrResult.InfoCode = typeCheck.ToString();
            }


            return rrResult.ToResult(armResult);
        }



        /// <summary>
        /// 리스트 받기 - 로그인 필수
        /// </summary>
        /// <param name="sBoardIdList"></param>
        /// <param name="nShowCount"></param>
        /// <param name="nPageNumber"></param>
        /// <param name="bAdminMode"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult<BoardPostResultModel> ListA_Auth(
            string sBoardIdList
            , int nShowCount
            , int nPageNumber
            , bool bAdminMode)
        {
            return this.ListA(sBoardIdList, nShowCount, nPageNumber, bAdminMode);
        }

        /// <summary>
        /// 리스트 받기
        /// </summary>
        /// <param name="sBoardIdList"></param>
        /// <param name="nShowCount"></param>
        /// <param name="nPageNumber"></param>
        /// <param name="bAdminMode"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult<BoardPostResultModel> ListA(
            string sBoardIdList
            , int nShowCount
            , int nPageNumber
            , bool bAdminMode)
        {
            ApiResultReady rrResult = new ApiResultReady(this);
            BoardPostResultModel rmReturn = new BoardPostResultModel();

            //입력값 검사
            if (0 >= nShowCount)
            {
                //쇼카운트의 최소값은 1이다.
                nShowCount = 1;
            }

            //게시판 정보 자르기****************************
            string[] arrsBoardId
                = sBoardIdList
                    .Replace("[", "")
                    .Replace("]", "")
                    .Split(",");
            //게시판 변환 완료된 게시판 정보
            List<long> listBoardId = new List<long>();
            //게시판 정보를 변환 한다.
            foreach (string itemBoardId in arrsBoardId)
            {
                if ("" != itemBoardId)
                {
                    listBoardId.Add(Convert.ToInt64(itemBoardId));
                }
            }

            if (0 >= listBoardId.Count)
            {//게시판 정보가 없다.
                rmReturn.InfoCode = "1";
                rmReturn.Message = "게시판 정보가 없습니다.";
            }


            //유저 정보 추출
            ClaimModel cm = new ClaimModel(((ClaimsIdentity)User.Identity).Claims);
            //찾은 게시판 권한
            BoardAuthorityType TypeBoardAuth;
            //권한 체크 결과
            BoardAuthCheckType typeCheck;

            //권한이 있는 리스트만 추린다.
            List<long> listBoardId_Permit = new List<long>();

            if ("0" == rmReturn.InfoCode)
            {
                //게시판 권한 찾기
                foreach (long itmeBoardId in listBoardId)
                {
                    //이 게시판 권한찾기
                    this.AuthGet(itmeBoardId
                                    , cm.id_int
                                    , out TypeBoardAuth
                                    , out typeCheck);

                    if (BoardAuthCheckType.Success == typeCheck)
                    {//권한 찾음

                        if ((0 >= cm.id_int)
                        && (false == TypeBoardAuth.HasFlag(BoardAuthorityType.ReadListNonMember)))
                        {//비회원인데
                         //리스트 보기(비회원) 권한이 없다.
                            rmReturn.InfoCode = "-2";
                            rmReturn.Message = "로그인해야 볼수 있는 게시판입니다.";
                        }
                        else if ((0 < cm.id_int)
                            && (false == TypeBoardAuth.HasFlag(BoardAuthorityType.ReadList)))
                        {//회원인데
                         //리스트 보기 권한이 없다.
                            rmReturn.InfoCode = "-3";
                            rmReturn.Message = "게시판을 볼 수 없습니다.";
                        }
                        else if ((true == bAdminMode)
                            && (ManagementClassCheckType.Ok 
                                != GlobalStatic.MgtA.MgtClassCheck(cm.id_int, ManagementClassType.Admin)))
                        {//관리자 모드인데
                         //관리자 권한이 없다.
                            rmReturn.InfoCode = "-4";
                            rmReturn.Message = "관리자 권한이 없습니다.";
                        }
                        else
                        {
                            //권한이 있다.
                            //권한 있는 리스트에 넣는다.
                            listBoardId_Permit.Add(itmeBoardId);
                        }
                    }

                    if (0 >= listBoardId_Permit.Count())
                    {
                        rmReturn.InfoCode = "-11";
                        rmReturn.Message = "볼 수 있는 게시판이 없습니다.";
                    }
                }//end foreach(long itmeBoardId in listBoardId)

            }//end if ("0" == rmReturn.InfoCode)


            if ("0" == rmReturn.InfoCode)
            {
                using (SpaNetCoreFoundationContext db1 = new SpaNetCoreFoundationContext())
                {
                    //게시판 정보 받기************************************
                    IQueryable<Board> iqfindBoards
                        = db1.Board
                            .Where(m => listBoardId_Permit.Contains(m.idBoard));
                }
            }



            return rrResult.ToResult(rmReturn);
        }

        #endregion

        #region 포스트 보기

        /// <summary>
        /// 해당 포스팅의 정보를 리턴한다. - 권한 전달 필수
        /// </summary>
        /// <param name="idBoard">진입 기준 보드값. 포스트의 보드와 다를 수 있다.</param>
        /// <param name="idBoardPost"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult<BoardPostViewResultModel> PostView_Auth(
            int idBoard
            , int idBoardPost)
        {
            return this.PostView(idBoard, idBoardPost);
        }

        /// <summary>
        /// 해당 포스팅의 정보를 리턴한다.
        /// </summary>
        /// <param name="idBoard">진입 기준 보드값. 포스트의 보드와 다를 수 있다.</param>
        /// <param name="idBoardPost"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<BoardPostViewResultModel> PostView(
            int idBoard
            , int idBoardPost)
        {
            ApiResultReady rrResult = new ApiResultReady(this);
            BoardPostViewResultModel rmResult = new BoardPostViewResultModel();
            rrResult.ResultObject = rmResult;

            //유저 정보 추출
            ClaimModel cm = new ClaimModel(((ClaimsIdentity)User.Identity).Claims);
            //찾은 게시판 권한
            BoardAuthorityType typeBoardAuth;
            //권한 체크 결과
            BoardAuthCheckType typeCheck;
            //게시판 권한 찾기(입력기준)
            rrResult.Message
                = this.AuthGet(idBoard
                                , cm.id_int
                                , out typeBoardAuth
                                , out typeCheck);

            if (BoardAuthCheckType.Success == typeCheck)
            {//성공
             //권한 체크
                if ("0" == rmResult.InfoCode)
                {
                    if ((0 >= cm.id_int)
                        && (false == typeBoardAuth.HasFlag(BoardAuthorityType.ReadNonMember)))
                    {//비회원인데
                     //리스트 보기(비회원) 권한이 없다.
                        rmResult.InfoCode = "-2";
                        rmResult.Message = "로그인해야 볼수 있는 게시물입니다.";
                    }
                    else if ((0 < cm.id_int)
                        && (false == typeBoardAuth.HasFlag(BoardAuthorityType.Read)))
                    {//회원인데
                     //리스트 보기 권한이 없다.
                        rmResult.InfoCode = "-3";
                        rmResult.Message = "게시물을 볼 수 없습니다.";
                    }
                }


                if ("0" == rmResult.InfoCode)
                {
                    using (SpaNetCoreFoundationContext db1 = new SpaNetCoreFoundationContext())
                    {
                        //대상 검색
                        BoardPost findBP
                         = db1.BoardPost
                             .Where(m => m.idBoardPost == idBoardPost
                                && (m.PostState != BoardPostStateType.Delete
                                    && m.PostState != BoardPostStateType.Block))
                             .FirstOrDefault();

                        if (null == findBP)
                        {
                            rmResult.InfoCode = "-1";
                            rmResult.Message = "대상 게시물이 없습니다.";
                        }
                        else
                        {
                            //포스트 뷰 카운트 증가
                            if (0 < cm.id_int)
                            {//로그인 카운트
                                findBP.ViewCount += 1;
                            }
                            else
                            {
                                findBP.ViewCountNone += 1;
                            }
                            db1.SaveChanges();

                            //포스트에 연결된 게시판 정보
                            Board findBoard
                                = db1.Board
                                    .Where(m => m.idBoard == findBP.idBoard)
                                    .FirstOrDefault();

                            //리플 리스트 표시 여부
                            if (true == findBoard.BoardFaculty.HasFlag(BoardFacultyType.ReplyList))
                            {
                                rmResult.ReplyList = true;
                            }

                            //찾은 게시판 권한
                            BoardAuthorityType typeBoardAuth_Post;
                            //권한 체크 결과
                            BoardAuthCheckType typeCheck_Post;
                            //게시판 권한 찾기(입력기준)
                            rrResult.Message
                                = this.AuthGet(findBP.idBoard
                                                , cm.id_int
                                                , out typeBoardAuth_Post
                                                , out typeCheck_Post);

                            if (BoardAuthCheckType.Success == typeCheck_Post)
                            {
                                //리플 권한이 있는지 확인
                                if ((0 >= cm.id_int)
                                    && (true == typeBoardAuth_Post.HasFlag(BoardAuthorityType.WriteReplyNonMember)))
                                {//회원이 아닌데
                                    //비회원 작성권한이 있다.
                                    rmResult.ReplyWrite = true;
                                }
                                else if ((0 < cm.id_int)
                                    && (true == typeBoardAuth_Post.HasFlag(BoardAuthorityType.WriteReply)))
                                {//회원 인데
                                    //작성권한이 있다.
                                    rmResult.ReplyWrite = true;
                                }

                            }

                            //작성자 정보 검색
                            UserInfo findUI
                                = db1.UserInfo
                                    .Where(m => m.idUser == findBP.idUser)
                                    .FirstOrDefault();

                            //내가 작성자인지 확인
                            if (cm.id_int == findBP.idUser)
                            {//내가 작성자다.

                                if (true == typeBoardAuth.HasFlag(BoardAuthorityType.Edit))
                                {//수정 권한
                                    rmResult.EditAuth = true;
                                }
                                if (true == typeBoardAuth.HasFlag(BoardAuthorityType.Delete))
                                {//삭제 권한
                                    rmResult.DeleteAuth = true;
                                }
                            }
                            else
                            {//내가 작성자는 니다.
                                //다른 사람 글을 수정/삭제 할 수 있는 권한이 있는지 확인
                                if (true == typeBoardAuth.HasFlag(BoardAuthorityType.EditOther))
                                {//수정 권한
                                    rmResult.EditAuth = true;
                                }
                                if (true == typeBoardAuth.HasFlag(BoardAuthorityType.DeleteOther))
                                {//삭제 권한
                                    rmResult.DeleteAuth = true;
                                }
                            }


                            //컨탠츠 입력
                            BoardContent findBC
                                = db1.BoardContent
                                    .Where(m => m.idBoardPost == idBoardPost)
                                    .FirstOrDefault();

                            //데이터 입력
                            rmResult.Reset(
                                findBP
                                , findUI
                                , findBC);
                        }

                    }//end using db1 
                }
            }
            else
            {//오류가 있다.
                rrResult.InfoCode = typeCheck.ToString();
            }


            return rrResult.ToResult(rmResult);
        }

        #endregion

        #region 리플 리스트

        /// <summary>
        /// 리플 리스트 - 권한 전달 필수
        /// </summary>
        /// <param name="idBoard">진입 기준 보드값. 포스트의 보드와 다를 수 있다.</param>
        /// <param name="idBoardPost"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult<BoardPostViewReplyResultModel> PostReplyList_Auth(
            long idBoard
            , long idBoardPost)
        {
            return this.PostReplyList(idBoard, idBoardPost);
        }

        /// <summary>
        /// 리플 리스트
        /// </summary>
        /// <param name="idBoard">진입 기준 보드값. 포스트의 보드와 다를 수 있다.</param>
        /// <param name="idBoardPost"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<BoardPostViewReplyResultModel> PostReplyList(
            long idBoard
            , long idBoardPost)
        {
            ApiResultReady rrResult = new ApiResultReady(this);
            BoardPostViewReplyResultModel rmResult = new BoardPostViewReplyResultModel();
            rrResult.ResultObject = rmResult;

            //유저 정보 추출
            ClaimModel cm = new ClaimModel(((ClaimsIdentity)User.Identity).Claims);
            //찾은 게시판 권한
            BoardAuthorityType typeBoardAuth;
            //권한 체크 결과
            BoardAuthCheckType typeCheck;
            //게시판 권한 찾기(입력기준)
            rmResult.Message
                = this.AuthGet(idBoard
                                , cm.id_int
                                , out typeBoardAuth
                                , out typeCheck);

            if (BoardAuthCheckType.Success == typeCheck)
            {//성공
             //권한 체크
                if ("0" == rmResult.InfoCode)
                {
                    if ((0 >= cm.id_int)
                        && (true == typeBoardAuth.HasFlag(BoardAuthorityType.WriteReplyNonMember)))
                    {//비회원인데
                     //리플 작성 권한이 있다.
                        rmResult.WriteReply = true;
                    }
                    else if ((0 < cm.id_int)
                        && (true == typeBoardAuth.HasFlag(BoardAuthorityType.WriteReply)))
                    {//회원인데
                     //리플 작성권한이 있다.
                        rmResult.WriteReply = true;
                    }
                }


                if ("0" == rmResult.InfoCode)
                {
                    using (SpaNetCoreFoundationContext db1 = new SpaNetCoreFoundationContext())
                    {
                        //비회원 리플 작성 허용인지 확인
                        if (true == typeBoardAuth.HasFlag(BoardAuthorityType.WriteReplyNonMember))
                        {//비회원 댓글 작성 허용
                            rmResult.ReplyNonMember = true;
                        }
                        

                        //대상 검색
                        BoardPost findBP
                         = db1.BoardPost
                             .Where(m => m.idBoardPost == idBoardPost
                                && (m.PostState != BoardPostStateType.Delete
                                    && m.PostState != BoardPostStateType.Block))
                             .FirstOrDefault();


                        if (null == findBP)
                        {
                            rmResult.InfoCode = "-4";
                            rmResult.Message = "대상 게시물이 없습니다.";
                        }
                        else
                        {
                            //포스트에 연결된 게시판 정보
                            Board board
                                = db1.Board
                                    .Where(m => m.idBoard == findBP.idBoard)
                                    .FirstOrDefault();

                            //리플 리스트 표시 여부
                            if (true == board.BoardFaculty.HasFlag(BoardFacultyType.ReplyList))
                            {
                                //리플 리스트
                                IQueryable<BoardPostReply> iqReply
                                    = db1.BoardPostReply
                                        .Where(m => m.idBoardPost == findBP.idBoardPost
                                                && m.idBoardPostReply_Re == 0
                                                && m.ReplyState == BoardPostReplyStateType.Normal);

                                if (true == board.BoardFaculty.HasFlag(BoardFacultyType.ReReplyDiv))
                                {//대댓글 분리기능 사용
                                    //대댓글 분리 기능 사용
                                    rmResult.ReReplyDiv = true;
                                }
                                else
                                {//대댓글 분리 기능 사용안함
                                    //전체리스트
                                    rmResult.ReReplyDiv = false;

                                    //대댓글만 검색
                                    IQueryable<BoardPostReply> iqReReply
                                        = db1.BoardPostReply
                                            .Where(m => m.idBoardPost == findBP.idBoardPost
                                                    && m.idBoardPostReply_Re != 0
                                                    && m.ReplyState == BoardPostReplyStateType.Normal)
                                            //부모 아이디 기준으로 정렬한다.
                                            .OrderBy(ob => ob.idBoardPostReply_ReParent);

                                    //대댓글 입력
                                    rmResult.ReReplyList
                                        = (from rereply in iqReReply
                                           from ui in db1.UserInfo
                                                    .Where(qui => qui.idUser == rereply.idUser)
                                                    .DefaultIfEmpty()
                                           select new BoardPostViewReplyModel(
                                               ui, rereply))
                                        .ToList();
                                }

                                //정렬
                                iqReply = iqReply.OrderBy(ob => ob.WriteDate);


                                rmResult.List
                                    = (from reply in iqReply
                                       from ui in db1.UserInfo
                                                    .Where(qui => qui.idUser == reply.idUser)
                                                    .DefaultIfEmpty()
                                       select new BoardPostViewReplyModel(
                                           ui, reply))
                                        .ToList();
                            }
                            else
                            {//리플리스트 권한이 없다.
                                //리스트가 없다.
                            }
                        }
                    }//end using db1
                }
            }
            else
            {
                rmResult.InfoCode = "-1";
            }

            return rrResult.ToResult();
        }

        /// <summary>
        /// 대댓글 리스트 - 권한 전달 필수
        /// </summary>
        /// <param name="idBoard"></param>
        /// <param name="idBoardPost"></param>
        /// <param name="idBoardReply"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult<BoardPostViewReplyResultModel> PostReReplyList_Auth(
            int idBoard
            , int idBoardPost
            , long idBoardReply)
        {
            return this.PostReReplyList(idBoard, idBoardPost, idBoardReply);
        }

        /// <summary>
        /// 대댓글 리스트 - 권한 전달 필수
        /// </summary>
        /// <param name="idBoard"></param>
        /// <param name="idBoardPost"></param>
        /// <param name="idBoardReply">이 댓글을 부모로 하고 있는 댓글을 검색함</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<BoardPostViewReplyResultModel> PostReReplyList(
            long idBoard
            , long idBoardPost
            , long idBoardReply)
        {
            ApiResultReady armResult = new ApiResultReady(this);
            BoardPostViewReplyResultModel rmReturn = new BoardPostViewReplyResultModel();

            //유저 정보 추출
            ClaimModel cm = new ClaimModel(((ClaimsIdentity)User.Identity).Claims);
            //찾은 게시판 권한
            BoardAuthorityType typeBoardAuth;
            //권한 체크 결과
            BoardAuthCheckType typeCheck;
            //게시판 권한 찾기(입력기준)
            rmReturn.Message
                = this.AuthGet(idBoard
                                , cm.id_int
                                , out typeBoardAuth
                                , out typeCheck);

            if (BoardAuthCheckType.Success == typeCheck)
            {//성공
             //권한 체크
                if ("0" == rmReturn.InfoCode)
                {
                    if ((0 >= cm.id_int)
                        && (true == typeBoardAuth.HasFlag(BoardAuthorityType.WriteReplyNonMember)))
                    {//비회원인데
                     //리플 작성 권한이 있다.
                        rmReturn.WriteReply = true;
                    }
                    else if ((0 < cm.id_int)
                        && (true == typeBoardAuth.HasFlag(BoardAuthorityType.WriteReply)))
                    {//회원인데
                     //리플 작성권한이 있다.
                        rmReturn.WriteReply = true;
                    }
                }


                if ("0" == rmReturn.InfoCode)
                {
                    using (SpaNetCoreFoundationContext db1 = new SpaNetCoreFoundationContext())
                    {
                        //대상 검색
                        BoardPost findBP
                         = db1.BoardPost
                             .Where(m => m.idBoardPost == idBoardPost
                                && (m.PostState != BoardPostStateType.Delete
                                    && m.PostState != BoardPostStateType.Block))
                             .FirstOrDefault();


                        if (null == findBP)
                        {
                            rmReturn.InfoCode = "-4";
                            rmReturn.Message = "대상 게시물이 없습니다.";
                        }
                        else
                        {
                            //포스트에 연결된 게시판 정보
                            Board board
                                = db1.Board
                                    .Where(m => m.idBoard == findBP.idBoard)
                                    .FirstOrDefault();

                            //리플 리스트 표시 여부
                            if (true == board.BoardFaculty.HasFlag(BoardFacultyType.ReplyList))
                            {
                                //대댓글 리스트
                                IQueryable<BoardPostReply> iqReReply
                                    = db1.BoardPostReply
                                        .Where(m => m.idBoardPostReply_ReParent == idBoardReply)
                                        .OrderBy(ob => ob.idBoardPostReply);

                                rmReturn.List
                                    = (from rereply in iqReReply
                                       from ui in db1.UserInfo
                                                    .Where(qui => qui.idUser == rereply.idUser)
                                                    .DefaultIfEmpty()
                                       select new BoardPostViewReplyModel(
                                           ui, rereply))
                                        .ToList();
                            }
                            else
                            {//리플리스트 권한이 없다.
                                //리스트가 없다.
                            }
                        }
                    }//end using db1
                }
            }
            else
            {
                rmReturn.InfoCode = "-1";
            }

            return armResult.ToResult(rmReturn);
        }
        #endregion

        #region 리플 작성

        /// <summary>
        /// 리플 작성 - 권한 전달 필수
        /// </summary>
        /// <param name="idBoard"></param>
        /// <param name="idBoardPost"></param>
        /// <param name="idBoardReply_Target">대댓글인경우 대상 없으면 0</param>
        /// <param name="sTitle"></param>
        /// <param name="sContent"></param>
        /// <param name="sNonMember_ViewName">비회원 - 표시 이름</param>
        /// <param name="sNonMember_Password">비회원 - 비밀번호</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public ActionResult<BoardPostReplyCreateResultModel> PostReplyCreate_Auth(
            [FromForm] long idBoard
            , [FromForm] long idBoardPost
            , [FromForm] long idBoardReply_Target
            , [FromForm] string sTitle
            , [FromForm] string sContent
            , [FromForm] string sNonMember_ViewName
            , [FromForm] string sNonMember_Password)
        {
            return this.PostReplyCreate(
                idBoard
                , idBoardPost
                , idBoardReply_Target
                , sTitle
                , sContent
                , sNonMember_ViewName
                , sNonMember_Password);
        }

        /// <summary>
        /// 리플 작성
        /// </summary>
        /// <param name="idBoard"></param>
        /// <param name="idBoardPost"></param>
        /// <param name="idBoardReply_Target">대댓글인경우 대상 없으면 0</param>
        /// <param name="sTitle"></param>
        /// <param name="sContent"></param>
        /// <param name="sNonMember_ViewName">비회원 - 표시 이름</param>
        /// <param name="sNonMember_Password">비회원 - 비밀번호</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<BoardPostReplyCreateResultModel> PostReplyCreate(
            [FromForm] long idBoard
            , [FromForm] long idBoardPost
            , [FromForm] long idBoardReply_Target
            , [FromForm] string sTitle
            , [FromForm] string sContent
            , [FromForm] string sNonMember_ViewName
            , [FromForm] string sNonMember_Password)
        {
            ApiResultReady rrResult = new ApiResultReady(this);
            BoardPostReplyCreateResultModel rmResult = new BoardPostReplyCreateResultModel();
            rrResult.ResultObject = rmResult;

            DateTime dtNow = DateTime.Now;

            //유저 정보 추출
            ClaimModel cm = new ClaimModel(((ClaimsIdentity)User.Identity).Claims);
            //찾은 게시판 권한
            BoardAuthorityType typeBoardAuth;
            //권한 체크 결과
            BoardAuthCheckType typeCheck;
            //게시판 권한 찾기(입력기준)
            rrResult.Message
                = this.AuthGet(idBoard
                                , cm.id_int
                                , out typeBoardAuth
                                , out typeCheck);

            //이 유저가 해당 관리 등급이 있는지 확인한다.
            ManagementClassCheckType typePC
                = GlobalStatic.MgtA.MgtClassCheck(cm.id_int
                    , ManagementClassType.Admin);

            if (BoardAuthCheckType.Success == typeCheck)
            {//성공
                //권한 체크

                if ("0" == rmResult.InfoCode)
                {
                    if ((0 >= cm.id_int)
                        && (false == typeBoardAuth.HasFlag(BoardAuthorityType.WriteReplyNonMember)))
                    {//비회원인데
                     //리스트 보기(비회원) 권한이 없다.
                        rmResult.InfoCode = "-11";
                        rmResult.Message = "로그인해야 작성할 수 있습니다.";
                    }
                    else if ((0 < cm.id_int)
                        && (false == typeBoardAuth.HasFlag(BoardAuthorityType.WriteReply)))
                    {//회원인데
                     //리스트 보기 권한이 없다.
                        rmResult.InfoCode = "-12";
                        rmResult.Message = "리플을 작성할 수 없는 게시판입니다.";
                    }
                    else if ((0 >= cm.id_int)
                            && ((string.Empty == sNonMember_ViewName)
                                || (string.Empty == sNonMember_Password)))
                    {//비회원인데
                        //이름이나
                        //비밀번호가 없다.
                        rmResult.InfoCode = "-13";
                        rmResult.Message = "이름과 비밀번호를 입력해 주세요.";
                    }
                }

                if ("0" == rmResult.InfoCode)
                {
                    using (SpaNetCoreFoundationContext db1 = new SpaNetCoreFoundationContext())
                    {
                        //대상 검색
                        BoardPost findBP
                         = db1.BoardPost
                             .Where(m => m.idBoardPost == idBoardPost
                                && (m.PostState != BoardPostStateType.Delete
                                    && m.PostState != BoardPostStateType.Block))
                             .FirstOrDefault();

                        if (null == findBP)
                        {
                            rmResult.InfoCode = "-4";
                            rmResult.Message = "대상 게시물이 없습니다.";
                        }


                        UserInfo findUI = null;

                        //유저 정보 확인*****************************
                        if ("0" == rmResult.InfoCode)
                        {
                            //유저정보
                            findUI
                                = db1.UserInfo
                                    .Where(m => m.idUser == cm.id_int)
                                    .FirstOrDefault();
                        }


                        //댓글 작성 *****************************
                        if ("0" == rmResult.InfoCode)
                        {

                            //댓글 개수 늘려주기
                            ++findBP.ReplyCount;
                            
                            //새로 작성
                            BoardPostReply newBPR = new BoardPostReply();
                            newBPR.idBoard = findBP.idBoard;
                            newBPR.idBoardPost = findBP.idBoardPost;

                            //대댓글인경우 대상
                            newBPR.idBoardPostReply_Re = idBoardReply_Target;

                            //최상위 찾기
                            if (0 >= newBPR.idBoardPostReply_Re)
                            {//대댓글이 아니다.
                                //대상이 없다.
                                newBPR.idBoardPostReply_ReParent = 0;
                            }
                            else
                            {//대댓글이다.

                                //대상 검색
                                BoardPostReply findTarget
                                    = db1.BoardPostReply
                                        .Where(m => m.idBoardPostReply == newBPR.idBoardPostReply_Re)
                                        .FirstOrDefault();

                                //대상에 댓글을 하나 추가해준다.
                                findTarget.ReReplyCount += 1;


                                //댓글 관계 리스트 받기
                                List<BoardPostReplyRelationTreeModel> listReturn
                                    = db1.BoardPostReplyRelationTreeModels
                                        .FromSqlRaw("SELECT * FROM dbo.Reply_GetBottomUp({0})"
                                                , idBoardReply_Target)
                                        .OrderBy(m => m.Depth)
                                        .ToList();

                                if (0 >= listReturn.Count())
                                {//리스트가 없다.
                                    //타겟이 최상위다.
                                    newBPR.idBoardPostReply_ReParent
                                        = idBoardReply_Target;
                                }
                                else
                                {//리스트가 있다.

                                    //리스트의 가장끝이 최상위다.
                                    newBPR.idBoardPostReply_ReParent
                                        = listReturn[listReturn.Count() - 1]
                                            .idBoardPostReply;
                                }
                            }


                            newBPR.idUser = cm.id_int;
                            //상태 노멀로 해준다.
                            newBPR.ReplyState = BoardPostReplyStateType.Normal;


                            newBPR.Title = sTitle;
                            newBPR.Content = sContent;

                            newBPR.WriteDate = dtNow;

                            //비회원 확인 ******
                            if(0 >= cm.id_int)
                            {//비회원이다.
                                //비회원 정보 입력
                                newBPR.NonMember_ViewName = sNonMember_ViewName;
                                newBPR.NonMember_Password = sNonMember_Password;
                            }

                            //db에 추가한다.
                            db1.BoardPostReply.Add(newBPR);
                            db1.SaveChanges();


                            //리턴용 모델 작성
                            rmResult.NewItem = new BoardPostViewReplyModel(findUI, newBPR);
                        }
                    }//end using db1
                }
            }
            else
            {
                rmResult.InfoCode = "-1";
            }

            return rrResult.ToResult(rmResult);
        }
        #endregion

        #region 리플 삭제
        /// <summary>
        /// 리플 삭제 - 로그인 필수
        /// 이름을 삭제지만 실제 동작은 상태값 변경이다.
        /// </summary>
        /// <param name="idBoard"></param>
        /// <param name="idBoardPost"></param>
        /// <param name="idBoardReply_Target"></param>
        /// <param name="typeReplyState"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        public ActionResult<BoardPostReplyCreateResultModel> PostReplyDelete_Auth(
            [FromForm] long idBoard
           , [FromForm] long idBoardPost
           , [FromForm] long idBoardReply_Target
           , [FromForm] BoardPostReplyStateType typeReplyState)
        {
            return this.PostReplyDelete(
                idBoard
                , idBoardPost
                , idBoardReply_Target
                , typeReplyState);
        }

        /// <summary>
        /// 리플 삭제
        /// 이름을 삭제지만 실제 동작은 상태값 변경이다.
        /// </summary>
        /// <param name="idBoard"></param>
        /// <param name="idBoardPost"></param>
        /// <param name="idBoardReply">삭제할 대상</param>
        /// <param name="typeReplyState"></param>
        /// <returns></returns>
        [HttpDelete]
        public ActionResult<BoardPostReplyCreateResultModel> PostReplyDelete(
            [FromForm] long idBoard
            , [FromForm] long idBoardPost
            , [FromForm] long idBoardReply
            , [FromForm] BoardPostReplyStateType typeReplyState)
        {
            ApiResultReady armResult = new ApiResultReady(this);
            armResult.InfoCode = ApiResultType.None.GetHashCode().ToString();

            //유저 정보 추출
            ClaimModel cm = new ClaimModel(((ClaimsIdentity)User.Identity).Claims);
            //찾은 게시판 권한
            BoardAuthorityType typeBoardAuth;
            //권한 체크 결과
            BoardAuthCheckType typeCheck;
            //게시판 권한 찾기(입력기준)
            armResult.Message
                = this.AuthGet(idBoard
                                , cm.id_int
                                , out typeBoardAuth
                                , out typeCheck);

            if (BoardAuthCheckType.Success == typeCheck)
            {//성공
                if ("0" == armResult.InfoCode)
                {
                    if ((0 >= cm.id_int)
                        && (false == typeBoardAuth.HasFlag(BoardAuthorityType.WriteReplyNonMember)))
                    {//비회원인데
                     //리스트 보기(비회원) 권한이 없다.
                        armResult.InfoCode = "-2";
                        armResult.Message = "로그인해야 작성할 수 있습니다.";
                    }
                    else if ((0 < cm.id_int)
                        && (false == typeBoardAuth.HasFlag(BoardAuthorityType.WriteReply)))
                    {//회원인데
                     //리스트 보기 권한이 없다.
                        armResult.InfoCode = "-3";
                        armResult.Message = "리플을 작성할 수 없는 게시판입니다.";
                    }
                }


                if ("0" == armResult.InfoCode)
                {
                    using (SpaNetCoreFoundationContext db1 = new SpaNetCoreFoundationContext())
                    {
                        //대상 검색
                        BoardPost findBP
                         = db1.BoardPost
                             .Where(m => m.idBoardPost == idBoardPost
                                && (m.PostState != BoardPostStateType.Delete
                                    && m.PostState != BoardPostStateType.Block))
                             .FirstOrDefault();

                        if (null == findBP)
                        {
                            armResult.InfoCode = "-4";
                            armResult.Message = "대상 게시물이 없습니다.";
                        }


                        //검색한 댓글
                        BoardPostReply findBPR = null;

                        if ("0" == armResult.InfoCode)
                        {
                            //대상 찾기
                            findBPR
                                = db1.BoardPostReply
                                    .Where(m => m.idBoardPostReply == idBoardReply)
                                    .FirstOrDefault();

                            if (null == findBPR)
                            {
                                armResult.InfoCode = "-5";
                                armResult.Message = "대상 댓글이 없습니다.";
                            }
                        }

                        if ("0" == armResult.InfoCode)
                        {
                            //유저정보
                            UserInfo findUI
                                = db1.UserInfo
                                    .Where(m => m.idUser == cm.id_int)
                                    .FirstOrDefault();


                            //부모 댓글 찾기
                            BoardPostReply findBPR_Re = null;
                            if (0 < findBPR.idBoardPostReply_ReParent)
                            {//부모 댓글이 있다.
                                //부모 댓글 검색
                                findBPR_Re
                                    = db1.BoardPostReply
                                    .Where(m => m.idBoardPostReply
                                            == findBPR.idBoardPostReply_ReParent)
                                    .FirstOrDefault();
                            }


                            switch (typeReplyState)
                            {//변경 요청한 타입
                                case BoardPostReplyStateType.Normal:
                                    {
                                        switch (findBPR.ReplyState)
                                        {
                                            case BoardPostReplyStateType.Block:
                                            case BoardPostReplyStateType.Delete:
                                                //상태 변경
                                                findBPR.ReplyState = typeReplyState;
                                                break;
                                            case BoardPostReplyStateType.Normal:
                                            default:
                                                //변경 없음
                                                break;
                                        }
                                    }
                                    break;

                            }

                        }
                    }
                }


            }//end if (BoardAuthCheckType.Success == typeCheck)


            return armResult.ToResult(null);
        }

        #endregion


        #region 포스트 작성

        /// <summary>
        /// 포스트 작성 뷰 요청 - 권한 전달 필수
        /// </summary>
        /// <param name="idBoard"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult<BoardPostCreateViewResultModel> PostCreateView_Auth(
            long idBoard)
        {
            return this.PostCreateView(idBoard);
        }

        /// <summary>
        /// 포스트 작성 뷰 요청
        /// </summary>
        /// <param name="idBoard"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<BoardPostCreateViewResultModel> PostCreateView(
            long idBoard)
        {
            ApiResultReady armResult = new ApiResultReady(this);
            BoardPostCreateViewResultModel rmReturn = new BoardPostCreateViewResultModel();

            //유저 정보 추출
            ClaimModel cm = new ClaimModel(((ClaimsIdentity)User.Identity).Claims);
            //찾은 게시판 권한
            BoardAuthorityType TypeBoardAuth;
            //권한 체크 결과
            BoardAuthCheckType typeCheck;
            //게시판 권한 찾기
            armResult.Message
                = this.AuthGet(idBoard
                                , cm.id_int
                                , out TypeBoardAuth
                                , out typeCheck);

            if (BoardAuthCheckType.Success == typeCheck)
            {
                //권한 체크
                if ("0" == rmReturn.InfoCode)
                {
                    if ((0 >= cm.id_int)
                    && (false == TypeBoardAuth.HasFlag(BoardAuthorityType.WriteNonMember)))
                    {//비회원이다.
                     //비회원 작성권한이 없다.
                        armResult.InfoCode = "-1";
                        armResult.Message = "로그인이 필요합니다.";
                    }
                    else if ((0 < cm.id_int)
                        && false == TypeBoardAuth.HasFlag(BoardAuthorityType.Write))
                    {//회원정보가 있다.
                     //쓰기 권한이 없다.
                        armResult.InfoCode = "-2";
                        armResult.Message = "게시물을 쓸 수 없는 게시판입니다.";
                    }
                }

                if ("0" == rmReturn.InfoCode)
                {
                    using (SpaNetCoreFoundationContext db1 = new SpaNetCoreFoundationContext())
                    {
                        //카테고리 정보
                        rmReturn.BoardCategory
                            = db1.BoardCategory
                                .Where(m => m.idBoard == idBoard)
                                .Select(s => new BoardCategoryModel(s))
                                .ToList();


                        //공지 권한 - 전체
                        rmReturn.NoticeAll
                            = TypeBoardAuth.HasFlag(BoardAuthorityType.NoticeAll);
                        //공지 - 그룹
                        rmReturn.NoticeGroup
                            = TypeBoardAuth.HasFlag(BoardAuthorityType.NoticeGroup);
                        //공지 - 게시판
                        rmReturn.NoticeBoard
                            = TypeBoardAuth.HasFlag(BoardAuthorityType.NoticeBoard);
                    }
                }
            }
            else
            {//오류가 있다.
                armResult.InfoCode = typeCheck.ToString();
            }



            return armResult.ToResult(rmReturn);
        }



        /// <summary>
        /// 포스트 작성 - 권한 전달 필수
        /// </summary>
        /// <param name="idBoard"></param>
        /// <param name="sTitle"></param>
        /// <param name="typeBoardState"></param>
        /// <param name="nBoardCategory"></param>
        /// <param name="sContent"></param>
        /// <param name="listFileInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [DisableRequestSizeLimit]
        public ActionResult<BoardPostCreateResultModel> PostCreate_Auth(
            [FromForm] int idBoard
            , [FromForm] string sTitle
            , [FromForm] BoardPostStateType typeBoardState
            , [FromForm] int nBoardCategory
            , [FromForm] string sContent
            , [FromForm] FileInfoModel[] listFileInfo)
        {

            return this.PostCreate(
                    idBoard
                    , sTitle
                    , typeBoardState
                    , nBoardCategory
                    , sContent
                    , listFileInfo);
        }


        /// <summary>
        /// 포스트 작성
        /// </summary>
        /// <param name="idBoard"></param>
        /// <param name="sTitle"></param>
        /// <param name="typeBoardState"></param>
        /// <param name="nBoardCategory"></param>
        /// <param name="sContent"></param>
        /// <param name="listFileInfo"></param>
        /// <returns></returns>
        [HttpPost]
        [DisableRequestSizeLimit]
        //[RequestSizeLimit(50_000_000)]//30.000.000=28,6MB
        public ActionResult<BoardPostCreateResultModel> PostCreate(
            [FromForm] long idBoard
            , [FromForm] string sTitle
            , [FromForm] BoardPostStateType typeBoardState
            , [FromForm] int nBoardCategory
            , [FromForm] string sContent
            , [FromForm] FileInfoModel[] listFileInfo)
        {
            //유저 정보 추출
            ClaimModel cm = new ClaimModel(((ClaimsIdentity)User.Identity).Claims);

            return this.PostCreateProcess(
                cm.id_int
                , idBoard
                , sTitle
                , typeBoardState
                , nBoardCategory
                , sContent
                , listFileInfo);
        }

        /// <summary>
        /// 포스트 작성
        /// </summary>
        /// <param name="idUser"></param>
        /// <param name="idBoard"></param>
        /// <param name="sTitle"></param>
        /// <param name="typeBoardState"></param>
        /// <param name="nBoardCategory"></param>
        /// <param name="sContent"></param>
        /// <param name="listFileInfo"></param>
        /// <returns></returns>
        private ActionResult<BoardPostCreateResultModel> PostCreateProcess(
            long idUser
            , long idBoard
            , string sTitle
            , BoardPostStateType typeBoardState
            , int nBoardCategory
            , string sContent
            , FileInfoModel[] listFileInfo)
        {
            ApiResultReady armResult = new ApiResultReady(this);
            BoardPostCreateResultModel rmReturn = new BoardPostCreateResultModel();

            //요청 날짜
            DateTime dtNow = DateTime.Now;
            //ip 정보
            string sIP = Request.HttpContext.Connection.RemoteIpAddress.ToString();

            //찾은 게시판 권한
            BoardAuthorityType TypeBoardAuth;
            //권한 체크 결과
            BoardAuthCheckType typeCheck;
            //게시판 권한 찾기
            armResult.Message
                = this.AuthGet(idBoard
                                , idUser
                                , out TypeBoardAuth
                                , out typeCheck);


            if (BoardAuthCheckType.Success == typeCheck)
            {
                //권한 체크
                if ("0" == rmReturn.InfoCode)
                {
                    if ((0 >= idUser)
                    && (false == TypeBoardAuth.HasFlag(BoardAuthorityType.WriteNonMember)))
                    {//비회원이다.
                     //비회원 작성권한이 없다.
                        rmReturn.InfoCode = "-1";
                        rmReturn.Message = "로그인이 필요합니다.";
                    }
                    else if ((0 < idUser)
                        && false == TypeBoardAuth.HasFlag(BoardAuthorityType.Write))
                    {//회원정보가 있다.
                     //쓰기 권한이 없다.
                        rmReturn.InfoCode = "-2";
                        rmReturn.Message = "게시물을 쓸 수 없는 게시판입니다.";
                    }
                }

                //전달된 데이터 체크
                if ("0" == rmReturn.InfoCode)
                {
                    if (true == string.IsNullOrWhiteSpace(sTitle))
                    {
                        rmReturn.InfoCode = "-3";
                        rmReturn.Message = "제목이 없습니다.";
                    }
                    else if (true == string.IsNullOrWhiteSpace(sContent))
                    {
                        rmReturn.InfoCode = "-4";
                        rmReturn.Message = "내용이 없습니다.";
                    }
                }

                if ("0" == rmReturn.InfoCode)
                {
                    using (SpaNetCoreFoundationContext db1 = new SpaNetCoreFoundationContext())
                    {
                        //포스트*******************************
                        BoardPost newBP = new BoardPost();
                        newBP.idBoard = idBoard;
                        newBP.idBoardCategory = nBoardCategory;
                        newBP.idUser = idUser;
                        

                        newBP.Title = sTitle.WithMaxLength(GlobalStatic.BoardTitleMexLength);
                        newBP.WriteDate = dtNow;
                        newBP.ViewCount = 0;
                        newBP.PostState = typeBoardState;
                        newBP.WriteIP = sIP;

                        db1.BoardPost.Add(newBP);
                        db1.SaveChanges();

                        //파일 ****************************************
                        //컨탠츠 임시 저장
                        string sContentTemp = sContent;

                        //파일 처리******
                        //썸네일 파일 정보
                        FileInfoModel fiThumbnail = null;
                        //썸네일 바이트 정보
                        byte[] byteThumbnail = null;
                        //파일 리스트 문자열
                        StringBuilder sbFileList = new StringBuilder();
                        FileData[] arrFL
                            = GlobalStatic.FileProc.FileInDb(
                                GlobalStatic.Dir_LocalRoot
                                , listFileInfo
                                , out fiThumbnail
                                , out byteThumbnail);


                        //html 민첩성 팩으로 변환 **************************
                        HtmlAgilityPack.HtmlDocument domContent
                            = new HtmlAgilityPack.HtmlDocument();
                        domContent.LoadHtml(sContentTemp);

                        foreach (FileData itemFL in arrFL)
                        {
                            if ((FileStateType.NewFile == itemFL.FileState)
                                || (FileStateType.Edit == itemFL.FileState))
                            {//추가 이거나
                             //수정이다.
                             //'EditorDivision' 속성 찾기
                                HtmlNodeCollection nodeColl
                                    = domContent.DocumentNode
                                        .SelectNodes("//img[@ideditordivision='" + itemFL.EditorDivision + "']");


                                if (null != nodeColl)
                                {//검색이 있을때만 동작
                                 //찾은 대상 수정하기
                                    foreach (HtmlNode nodeItem in nodeColl)
                                    {
                                        nodeItem.SetAttributeValue("src", itemFL.FileUrl);
                                        nodeItem.SetAttributeValue("alt", itemFL.Description);
                                    }
                                }
                            }

                            if ((FileStateType.Normal == itemFL.FileState)
                                || (FileStateType.NewFile == itemFL.FileState)
                                || (FileStateType.Edit == itemFL.FileState))
                            {//기본상태
                                //추가 상태
                                //수정 상태일때는 파일리스트 정보를 준다.

                                sbFileList.Append("," + itemFL.idFileList);
                            }
                        }//end foreach (FileList itemFL in arrFL)

                        //완성된 돔을 html로 전달
                        sContentTemp = domContent.DocumentNode.OuterHtml;


                        //썸네일 저장 *****************************************
                        if (null != fiThumbnail)
                        {
                            Stream streamThumbnai = new MemoryStream(byteThumbnail);
                            Image imgThumbnai =
                                GlobalStatic.FileProc
                                    .GetReducedImage(256, 256, streamThumbnai);

                            if (null != imgThumbnai)
                            {
                                //디랙토리 생성
                                string sUrlThumbnail =
                                    GlobalStatic.Dir_LocalRoot
                                    + string.Format(@"\wwwroot\Upload\BoardThumbnail\{0}\{1}\"
                                                    , newBP.idBoard
                                                    , newBP.idBoardPost);
                                System.IO.Directory.CreateDirectory(sUrlThumbnail);
                                //썸네일 저장
                                imgThumbnai.Save(sUrlThumbnail + "Thumbnail_256x256.png", ImageFormat.Png);
                                //url 작성
                                string sThumbnailUrl
                                    = string.Format("/Upload/BoardThumbnail/{0}/{1}/"
                                                    , newBP.idBoard
                                                    , newBP.idBoardPost);
                                newBP.ThumbnailUrl = sThumbnailUrl;
                            }

                        }


                        //포스트 내용
                        BoardContent newBC = new BoardContent();
                        newBC.idBoard = idBoard;
                        newBC.idBoardPost = newBP.idBoardPost;
                        newBC.Content = sContentTemp;

                        if (0 < sbFileList.Length)
                        {//파일 리스트가 있다.
                            newBC.FileList = sbFileList.ToString().Substring(1);
                        }
                        else
                        {//첨부된 파일이 없다.
                            newBC.FileList = string.Empty;
                        }

                        db1.BoardContent.Add(newBC);
                        db1.SaveChanges();


                        //전달용
                        rmReturn.PostID = newBP.idBoardPost;
                    }//end using db1
                }
            }
            else
            {//오류가 있다.
                armResult.InfoCode = typeCheck.ToString();
            }

            return armResult.ToResult(rmReturn);
        }
        #endregion

        #region 포스트 작성 - 외부 지원API

        /// <summary>
        /// 포스트 작성 - 외부 API
        /// API를 사용하여 게시물을 작성한다.
        /// </summary>
        /// <param name="sApiKey"></param>
        /// <param name="idBoard"></param>
        /// <param name="sTitle"></param>
        /// <param name="sContent"></param>
        /// <param name="listFileInfo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<BoardPostCreateResultModel> PostCreate_Api(
            [FromForm] string sApiKey
            , [FromForm] long idBoard
            , [FromForm] string sTitle
            , [FromForm] string sContent
            , [FromForm] FileInfoModel[] listFileInfo)
        {
            ApiResultReady armResult = new ApiResultReady(this);

            //사용할 유저 정보
            User findUser = null;

            using (SpaNetCoreFoundationContext db1 = new SpaNetCoreFoundationContext())
            {
                //api 키를 찾는다.
                UserApi findUA
                    = db1.UserApi
                        .Where(m => m.ApiKey == sApiKey)
                        .FirstOrDefault();


                if (null == findUA)
                {
                    armResult.InfoCode = "-1";
                    armResult.Message = "api키를 찾지 못했습니다.";
                }

                if (true == armResult.IsSuccess())
                {
                    if (UserApiStateType.Normal == findUA.UserApiState)
                    {//성공
                    }
                    else if (UserApiStateType.Block == findUA.UserApiState)
                    {
                        armResult.InfoCode = "-21";
                        armResult.Message = "api가 블럭되었습니다.";
                    }
                    else if (UserApiStateType.Expiration == findUA.UserApiState)
                    {
                        armResult.InfoCode = "-22";
                        armResult.Message = "api가 만료되었습니다.";
                    }
                    else
                    {
                        armResult.InfoCode = "-20";
                        armResult.Message = "api키를 찾지 못했습니다.";
                    }
                }

                //유저 정보 확인
                if (true == armResult.IsSuccess())
                {
                    findUser
                        = db1.User
                            .Where(m => m.idUser == findUA.idUser)
                            .FirstOrDefault();

                    if (null == findUser)
                    {
                        armResult.InfoCode = "-30";
                        armResult.Message = "대상 유저를 찾지 못했습니다.";
                    }
                }

                //로그 기록 작성
                if (true == armResult.IsSuccess())
                {
                    ApiLog newApiLog = new ApiLog();
                    newApiLog.ApiKey = sApiKey;
                    newApiLog.idUser = findUA.idUser;
                    newApiLog.CallDate = DateTime.Now;
                    newApiLog.Step01 = "Board/PostCreate_Api";
                    newApiLog.Contents
                        = string.Format("title:{0} <br /> file count : {1}<br />{2} "
                            , sTitle
                            , listFileInfo.Length
                            , sContent);

                    db1.ApiLog.Add(newApiLog);
                    db1.SaveChanges();
                }
            }//end using db1

            if (true == armResult.IsSuccess())
            {//성공


                //작성 호출
                return this.PostCreateProcess(
                    findUser.idUser
                    , idBoard
                    , sTitle
                    , BoardPostStateType.Normal
                    , 0
                    , sContent
                    , listFileInfo);
            }
            else
            {
                return armResult.ToResult(null);
            }
        }
        #endregion

        #region 포스트 수정

        /// <summary>
        /// 포스트를 수정하기위한 데이터 요청 - 권한 전달 필수
        /// </summary>
        /// <param name="idBoard"></param>
        /// <param name="idBoardPost"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult<BoardPostEditResultModel> PostEditView_Auth(
            int idBoard
            , int idBoardPost)
        {
            //유저 정보 추출
            ClaimModel cm = new ClaimModel(((ClaimsIdentity)User.Identity).Claims);

            if (0 >= cm.id_int)
            {
                return StatusCode(StatusCodes.Status403Forbidden);
            }
            else
            {
                return this.PostEditView(idBoard, idBoardPost);
            }
        }

        /// <summary>
        /// 포스트를 수정하기위한 데이터 요청
        /// </summary>
        /// <param name="idBoard"></param>
        /// <param name="idBoardPost"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<BoardPostEditResultModel> PostEditView(
            int idBoard
            , int idBoardPost)
        {
            ApiResultReady armResult = new ApiResultReady(this);
            BoardPostEditResultModel rmReturn = new BoardPostEditResultModel();

            //유저 정보 추출
            ClaimModel cm = new ClaimModel(((ClaimsIdentity)User.Identity).Claims);
            //찾은 게시판 권한
            BoardAuthorityType TypeBoardAuth;
            //권한 체크 결과
            BoardAuthCheckType typeCheck;
            //게시판 권한 찾기
            armResult.Message
                = this.AuthGet(idBoard
                                , cm.id_int
                                , out TypeBoardAuth
                                , out typeCheck);

            if (BoardAuthCheckType.Success == typeCheck)
            {//성공

                using (SpaNetCoreFoundationContext db1 = new SpaNetCoreFoundationContext())
                {
                    BoardPost findBP
                        = db1.BoardPost
                            .Where(m => m.idBoardPost == idBoardPost
                                && (m.PostState != BoardPostStateType.Delete
                                    && m.PostState != BoardPostStateType.Block))
                            .FirstOrDefault();

                    if (null == findBP)
                    {//게시물을 찾지 못했다.
                        rmReturn.InfoCode = "-1";
                        rmReturn.Message = "게시물을 찾을 수 없습니다.";
                    }
                    else if ((cm.id_int == findBP.idUser)
                        && (false == TypeBoardAuth.HasFlag(BoardAuthorityType.Edit)))
                    {//내글인데
                        //글 수정권한이 없다.
                        rmReturn.InfoCode = "-2";
                        rmReturn.Message = "이 게시판은 수정 할 수 없습니다.";
                    }
                    else if ((cm.id_int != findBP.idUser)
                        && (false == TypeBoardAuth.HasFlag(BoardAuthorityType.EditOther)))
                    {//내글이 아닌데
                        //다른사람 글 수정권한이 없다.
                        rmReturn.InfoCode = "-3";
                        rmReturn.Message = "다른 사람글은 수정할 수 없습니다.";
                    }
                    else
                    {//권한이 있다.

                        //작성자 정보 검색
                        UserInfo findUI
                            = db1.UserInfo
                                .Where(m => m.idUser == findBP.idUser)
                                .FirstOrDefault();

                        //컨탠츠 검색
                        BoardContent findBC
                            = db1.BoardContent
                                .Where(m => m.idBoardPost == idBoardPost)
                                .FirstOrDefault();


                        //파일 리스트 검색*****
                        //비어있는지 예외처리
                        string sFileList = string.Empty;
                        if (null == findBC.FileList)
                        {
                            sFileList = "";
                        }
                        else
                        {
                            sFileList = findBC.FileList;
                        }

                        //파일 리스트를 리스트로 변환
                        string[] sFileListCut = sFileList.Split(",");
                        List<long> listFLCut = new List<long>();
                        foreach (string itemFLC in sFileListCut)
                        {
                            if ("" != itemFLC)
                            {
                                listFLCut.Add(Convert.ToInt64(itemFLC));
                            }

                        }

                        //파일 리스트 검색
                        List<FileData> findFileList
                            = db1.FileData
                                .Where(m => listFLCut.Contains(m.idFileList))
                                .ToList();



                        //데이터 입력
                        rmReturn.Reset(findBP, findUI, findBC);
                        //파일 정보 리스트 입력
                        rmReturn.FileInfoList = new List<FileInfoModel>();
                        foreach (FileData itemFL in findFileList)
                        {
                            FileInfoModel newFI = new FileInfoModel(itemFL);
                            //자신이 썸네일인지 여부
                            if (findBP.ThumbnailUrl == itemFL.FileUrl)
                            {//썸네일이다.
                                newFI.Thumbnail = true;
                            }

                            //리스트에 추가
                            rmReturn.FileInfoList.Add(newFI);
                        }


                        //카테고리 정보
                        rmReturn.BoardCategory
                            = db1.BoardCategory
                                .Where(m => m.idBoard == idBoard)
                                .Select(s => new BoardCategoryModel(s))
                                .ToList();


                        //공지 권한 - 전체
                        rmReturn.NoticeAll
                            = TypeBoardAuth.HasFlag(BoardAuthorityType.NoticeAll);
                        //공지 - 그룹
                        rmReturn.NoticeGroup
                            = TypeBoardAuth.HasFlag(BoardAuthorityType.NoticeGroup);
                        //공지 - 게시판
                        rmReturn.NoticeBoard
                            = TypeBoardAuth.HasFlag(BoardAuthorityType.NoticeBoard);
                    }

                }//end using db1

            }
            else
            {//오류가 있다.
                armResult.InfoCode = typeCheck.ToString();
            }

            return armResult.ToResult(rmReturn);
        }


        /// <summary>
        /// 포스트 수정 - 권한 전달 필수
        /// </summary>
        /// <param name="idBoard"></param>
        /// <param name="idBoardPost"></param>
        /// <param name="sTitle"></param>
        /// <param name="typeBoardState"></param>
        /// <param name="nBoardCategory"></param>
        /// <param name="sContent"></param>
        /// <param name="listFileInfo"></param>
        /// <returns></returns>
        [HttpPatch]
        [Authorize]
        [DisableRequestSizeLimit]
        public ActionResult<BoardPostCreateResultModel> PostEdit_Auth(
            [FromForm] long idBoard
            , [FromForm] long idBoardPost
            , [FromForm] string sTitle
            , [FromForm] BoardPostStateType typeBoardState
            , [FromForm] int nBoardCategory
            , [FromForm] string sContent
            , [FromForm] FileInfoModel[] listFileInfo)
        {
            return this.PostEdit(
                idBoard
                , idBoardPost
                , sTitle
                , typeBoardState
                , nBoardCategory
                , sContent
                , listFileInfo);
        }

        /// <summary>
        /// 포스트 수정
        /// </summary>
        /// <param name="idBoard"></param>
        /// <param name="idBoardPost"></param>
        /// <param name="sTitle"></param>
        /// <param name="typeBoardState"></param>
        /// <param name="nBoardCategory"></param>
        /// <param name="sContent"></param>
        /// <param name="listFileInfo"></param>
        /// <returns></returns>
        [HttpPatch]
        [DisableRequestSizeLimit]
        public ActionResult<BoardPostCreateResultModel> PostEdit(
            [FromForm] long idBoard
            , [FromForm] long idBoardPost
            , [FromForm] string sTitle
            , [FromForm] BoardPostStateType typeBoardState
            , [FromForm] int nBoardCategory
            , [FromForm] string sContent
            , [FromForm] FileInfoModel[] listFileInfo)
        {
            ApiResultReady armResult = new ApiResultReady(this);
            BoardPostCreateResultModel rmReturn = new BoardPostCreateResultModel();

            //요청 날짜
            DateTime dtNow = DateTime.Now;
            //ip 정보
            string sIP = Request.HttpContext.Connection.RemoteIpAddress.ToString();

            //유저 정보 추출
            ClaimModel cm = new ClaimModel(((ClaimsIdentity)User.Identity).Claims);
            //찾은 게시판 권한
            BoardAuthorityType TypeBoardAuth;
            //권한 체크 결과
            BoardAuthCheckType typeCheck;
            //게시판 권한 찾기
            armResult.Message
                = this.AuthGet(idBoard
                                , cm.id_int
                                , out TypeBoardAuth
                                , out typeCheck);

            if (BoardAuthCheckType.Success == typeCheck)
            {
                using (SpaNetCoreFoundationContext db1 = new SpaNetCoreFoundationContext())
                {
                    BoardPost findBP
                        = db1.BoardPost
                            .Where(m => m.idBoardPost == idBoardPost)
                            .FirstOrDefault();

                    if (null == findBP)
                    {//게시물을 찾지 못했다.
                        rmReturn.InfoCode = "-1";
                        rmReturn.Message = "게시물을 찾을 수 없습니다.";
                    }
                    else if ((cm.id_int == findBP.idUser)
                        && (false == TypeBoardAuth.HasFlag(BoardAuthorityType.Edit)))
                    {//내글인데
                        //글 수정권한이 없다.
                        rmReturn.InfoCode = "-2";
                        rmReturn.Message = "이 게시판은 수정 할 수 없습니다.";
                    }
                    else if ((cm.id_int != findBP.idUser)
                        && (false == TypeBoardAuth.HasFlag(BoardAuthorityType.EditOther)))
                    {//내글이 아닌데
                        //다른사람 글 수정권한이 없다.
                        rmReturn.InfoCode = "-3";
                        rmReturn.Message = "다른 사람글은 수정할 수 없습니다.";
                    }
                    else
                    {//권한이 있다.
                        //포스트*******************************
                        findBP.Title = sTitle;
                        findBP.EditDate = dtNow;
                        findBP.PostState = typeBoardState;
                        findBP.idBoardCategory = nBoardCategory;
                        findBP.EditIP = sIP;

                        //컨탠츠 임시 저장
                        string sContentTemp = sContent;

                        //파일 ****************************************

                        //썸네일 파일 정보
                        FileInfoModel fiThumbnail = null;
                        //썸네일 바이트 정보
                        byte[] byteThumbnail = null;
                        //파일 리스트 문자열
                        StringBuilder sbFileList = new StringBuilder();
                        FileData[] arrFL
                            = GlobalStatic.FileProc.FileInDb(
                                GlobalStatic.Dir_LocalRoot
                                , listFileInfo
                                , out fiThumbnail
                                , out byteThumbnail);


                        //html 민첩성 팩으로 변환 **************************
                        HtmlAgilityPack.HtmlDocument domContent
                            = new HtmlAgilityPack.HtmlDocument();
                        domContent.LoadHtml(sContentTemp);

                        foreach (FileData itemFL in arrFL)
                        {
                            if ((FileStateType.NewFile == itemFL.FileState)
                                || (FileStateType.Edit == itemFL.FileState))
                            {//추가 이거나
                             //수정이다.
                             //'EditorDivision' 속성 찾기
                                HtmlNodeCollection nodeColl
                                    = domContent.DocumentNode
                                        .SelectNodes("//img[@ideditordivision='" + itemFL.EditorDivision + "']");


                                if (null != nodeColl)
                                {//검색이 있을때만 동작
                                 //찾은 대상 수정하기
                                    foreach (HtmlNode nodeItem in nodeColl)
                                    {
                                        nodeItem.SetAttributeValue("src", itemFL.FileUrl);
                                        nodeItem.SetAttributeValue("alt", itemFL.Description);
                                    }
                                }
                            }

                            if ((FileStateType.Normal == itemFL.FileState)
                                || (FileStateType.NewFile == itemFL.FileState)
                                || (FileStateType.Edit == itemFL.FileState))
                            {//기본상태
                                //추가 상태
                                //수정 상태일때는 파일리스트 정보를 준다.

                                sbFileList.Append("," + itemFL.idFileList);
                            }
                        }//end foreach (FileList itemFL in arrFL)

                        //완성된 돔을 html로 전달
                        sContentTemp = domContent.DocumentNode.OuterHtml;


                        //썸네일 저장 ***************************************
                        if (null != fiThumbnail)
                        {
                            Stream streamThumbnai = new MemoryStream(byteThumbnail);
                            Image imgThumbnai =
                                GlobalStatic.FileProc
                                    .GetReducedImage(256, 256, streamThumbnai);

                            if (null != imgThumbnai)
                            {
                                //디랙토리 생성
                                string sUrlThumbnail =
                                    GlobalStatic.Dir_LocalRoot
                                    + string.Format(@"\wwwroot\Upload\BoardThumbnail\{0}\{1}\"
                                                    , findBP.idBoard
                                                    , findBP.idBoardPost);
                                System.IO.Directory.CreateDirectory(sUrlThumbnail);
                                //썸네일 저장
                                imgThumbnai.Save(sUrlThumbnail + "Thumbnail_256x256.png", ImageFormat.Png);
                                //url 작성
                                string sThumbnailUrl
                                    = string.Format("/Upload/BoardThumbnail/{0}/{1}/"
                                                    , findBP.idBoard
                                                    , findBP.idBoardPost);
                                findBP.ThumbnailUrl = sThumbnailUrl;
                            }
                        }


                        //포스트 내용 *****************
                        BoardContent findBC
                            = db1.BoardContent
                                .Where(m => m.idBoardPost == idBoardPost)
                                .FirstOrDefault();
                        findBC.Content = sContentTemp;

                        if (0 < sbFileList.Length)
                        {//파일 리스트가 있다.
                            findBC.FileList = sbFileList.ToString().Substring(1);
                        }
                        else
                        {//첨부된 파일이 없다.
                            findBC.FileList = string.Empty;
                        }

                        db1.SaveChanges();


                        //전달용
                        rmReturn.PostID = findBC.idBoardPost;

                    }//end if findBP
                }
            }
            else
            {//오류가 있다.
                armResult.InfoCode = typeCheck.ToString();
            }

            return armResult.ToResult(rmReturn);
        }

        #endregion


        #region 포스트 삭제

        /// <summary>
        /// 포스트 삭제하기위한 데이터 요청 - 권한 전달 필수
        /// </summary>
        /// <param name="idBoard"></param>
        /// <param name="idBoardPost"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public ActionResult<ApiResultBaseModel> PostDeleteView_Auth(
            long idBoard
            , long idBoardPost)
        {
            return this.PostDeleteView(idBoard, idBoardPost);
        }

        /// <summary>
        /// 포스트 삭제하기위한 데이터 요청
        /// </summary>
        /// <param name="idBoard"></param>
        /// <param name="idBoardPost"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<ApiResultBaseModel> PostDeleteView(
            long idBoard
            , long idBoardPost)
        {
            ApiResultReady armResult = new ApiResultReady(this);

            //유저 정보 추출
            ClaimModel cm = new ClaimModel(((ClaimsIdentity)User.Identity).Claims);
            //찾은 게시판 권한
            BoardAuthorityType TypeBoardAuth;
            //권한 체크 결과
            BoardAuthCheckType typeCheck;
            //게시판 권한 찾기
            armResult.Message
                = this.AuthGet(idBoard
                                , cm.id_int
                                , out TypeBoardAuth
                                , out typeCheck);

            if (BoardAuthCheckType.Success == typeCheck)
            {
                using (SpaNetCoreFoundationContext db1 = new SpaNetCoreFoundationContext())
                {
                    BoardPost findBP
                        = db1.BoardPost
                            .Where(m => m.idBoardPost == idBoardPost
                                && (m.PostState != BoardPostStateType.Delete
                                    && m.PostState != BoardPostStateType.Block))
                            .FirstOrDefault();

                    if (null == findBP)
                    {//게시물을 찾지 못했다.
                        armResult.InfoCode = "-1";
                        armResult.Message = "게시물을 찾을 수 없습니다.";
                    }
                    else if ((cm.id_int == findBP.idUser)
                        && (false == TypeBoardAuth.HasFlag(BoardAuthorityType.Delete)))
                    {//내글인데
                        //글 수정권한이 없다.
                        armResult.InfoCode = "-2";
                        armResult.Message = "이 게시판은 삭제 할 수 없습니다.";
                    }
                    else if ((cm.id_int != findBP.idUser)
                        && (false == TypeBoardAuth.HasFlag(BoardAuthorityType.DeleteOther)))
                    {//내글이 아닌데
                        //다른사람 글 수정권한이 없다.
                        armResult.InfoCode = "-3";
                        armResult.Message = "다른 사람글은 삭제할 수 없습니다.";
                    }
                    else
                    {
                        //삭제 가능 리턴.
                    }

                }//end using db1
            }
            else
            {//오류가 있다.
                armResult.InfoCode = typeCheck.ToString();
            }



            return armResult.ToResult(null);
        }


        /// <summary>
        /// 포스트 삭제 - 권한 전달 필수
        /// </summary>
        /// <param name="idBoard"></param>
        /// <param name="idBoardPost"></param>
        /// <returns></returns>
        [HttpDelete]
        public ActionResult<ApiResultBaseModel> PostDelete_Auth(
            [FromForm] long idBoard
            , [FromForm] long idBoardPost)
        {
            return this.PostDelete(idBoard, idBoardPost);
        }

        /// <summary>
        /// 포스트 삭제
        /// </summary>
        /// <param name="idBoard"></param>
        /// <param name="idBoardPost"></param>
        /// <returns></returns>
        [HttpDelete]
        public ActionResult<ApiResultBaseModel> PostDelete(
            [FromForm] long idBoard
            , [FromForm] long idBoardPost)
        {
            ApiResultReady armResult = new ApiResultReady(this);

            //요청 날짜
            DateTime dtNow = DateTime.Now;

            //유저 정보 추출
            ClaimModel cm = new ClaimModel(((ClaimsIdentity)User.Identity).Claims);
            //찾은 게시판 권한
            BoardAuthorityType TypeBoardAuth;
            //권한 체크 결과
            BoardAuthCheckType typeCheck;
            //게시판 권한 찾기
            armResult.Message
                = this.AuthGet(idBoard
                                , cm.id_int
                                , out TypeBoardAuth
                                , out typeCheck);


            if (BoardAuthCheckType.Success == typeCheck)
            {
                using (SpaNetCoreFoundationContext db1 = new SpaNetCoreFoundationContext())
                {
                    BoardPost findBP
                        = db1.BoardPost
                            .Where(m => m.idBoardPost == idBoardPost)
                            .FirstOrDefault();

                    if (null == findBP)
                    {//게시물을 찾지 못했다.
                        armResult.InfoCode = "-1";
                        armResult.Message = "게시물을 찾을 수 없습니다.";
                    }
                    else if ((cm.id_int == findBP.idUser)
                        && (false == TypeBoardAuth.HasFlag(BoardAuthorityType.Delete)))
                    {//내글인데
                        //글 수정권한이 없다.
                        armResult.InfoCode = "-2";
                        armResult.Message = "이 게시판은 삭제 할 수 없습니다.";
                    }
                    else if ((cm.id_int != findBP.idUser)
                        && (false == TypeBoardAuth.HasFlag(BoardAuthorityType.DeleteOther)))
                    {//내글이 아닌데
                        //다른사람 글 수정권한이 없다.
                        armResult.InfoCode = "-3";
                        armResult.Message = "다른 사람글은 삭제할 수 없습니다.";
                    }
                    else
                    {
                        //포스트*******************************
                        findBP.DeleteDate = dtNow;
                        findBP.PostState = BoardPostStateType.Delete;

                        db1.SaveChanges();
                    }

                }//end using db1
            }
            else
            {//오류가 있다.
                armResult.InfoCode = typeCheck.ToString();
            }

            return armResult.ToResult(null);
        }
        #endregion


        #region 요약

        /// <summary>
        /// 게시판의 조건에 맞는 갯수
        /// </summary>
        /// <param name="sBoardIdList">게시판 아이디 리스트 문자열</param>
        /// <param name="nShowCount">표시할 개수. 최대 50개</param>
        /// <param name="sSearchWord">검색어</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<BoardPostSummaryResultModel> SummaryList(
            string sBoardIdList
            , int nShowCount
            , string sSearchWord)
        {
            ApiResultReady armResult = new ApiResultReady(this);
            BoardPostSummaryResultModel rmReturn = new BoardPostSummaryResultModel();

            //표시 개수 계산
            int nShowCountTemp = nShowCount;
            if (50 < nShowCount)
            {
                nShowCountTemp = 50;
            }

            //게시판 정보 자르기*********************
            string[] arrsBoardId
                = sBoardIdList
                    .Replace("[", "")
                    .Replace("]", "")
                    .Split(",");
            //게시판 변환 완료된 게시판 정보
            List<long> listBoardId = new List<long>();
            //게시판 정보를 변환 한다.
            foreach (string itemBoardId in arrsBoardId)
            {
                if ("" != itemBoardId)
                {
                    listBoardId.Add(Convert.ToInt64(itemBoardId));
                }
            }


            if (0 >= listBoardId.Count)
            {//게시판 정보가 없다.
                rmReturn.InfoCode = "1";
                rmReturn.Message = "게시판 정보가 없습니다.";
            }


            if ("0" == rmReturn.InfoCode)
            {
                using (SpaNetCoreFoundationContext db1 = new SpaNetCoreFoundationContext())
                {
                    //게시판 정보
                    Board findBoard
                        = db1.Board
                            .Where(m => listBoardId.Contains(m.idBoard))
                            .FirstOrDefault();

                    if (null != findBoard)
                    {
                        rmReturn.BoardTitle = findBoard.Title;


                        //포스트 검색
                        IQueryable<BoardPost> iqBP
                             = db1.BoardPost
                                 .Where(m => listBoardId.Contains(m.idBoard)
                                        && (m.PostState != BoardPostStateType.Delete
                                            && m.PostState != BoardPostStateType.Block))
                                 .OrderByDescending(m => m.idBoardPost);

                        //검색어 체크*********************************
                        if (false == string.IsNullOrEmpty(sSearchWord))
                        {//검색어가 있다.
                            //컨탠츠와 엮어서 표시한다.
                            iqBP
                                = from bp in iqBP
                                  join bpc in db1.BoardContent
                                      on bp.idBoard equals bpc.idBoard
                                  where bp.Title.Contains(sSearchWord)
                                      || bpc.Content.Contains(sSearchWord)
                                  select bp;
                        }

                        //검색된 게시물 정리**************************************
                        rmReturn.List
                            = (from bp in iqBP
                               from ui in db1.UserInfo.Where(a => a.idUser == bp.idUser)
                               select new BoardPostListModel(bp, ui, BoardItemType.None))
                               .Distinct()
                              .Take(nShowCountTemp)
                              .ToList();
                    }
                    else
                    {
                        rmReturn.InfoCode = "2";
                        rmReturn.Message = "해당 게시판이 없습니다.";

                    }

                }//end db1
            }


            return armResult.ToResult(rmReturn);
        }
        #endregion

        /// <summary>
        /// 게시판에 해당하는 권한을 가지고 온다.
        /// </summary>
        /// <param name="idBoard"></param>
        /// <param name="idUser"></param>
        /// <param name="typeBoardAuth"></param>
        /// <param name="typeCheck"></param>
        /// <returns></returns>
        private string AuthGet(
            long idBoard
            , long idUser
            , out BoardAuthorityType typeBoardAuth
            , out BoardAuthCheckType typeCheck)
        {

            //권한 체크 결과.
            typeCheck = BoardAuthCheckType.Success;
            string sMessage = string.Empty;
            //게시판 권한 초기화
            typeBoardAuth = BoardAuthorityType.None;


            if (0 >= idBoard)
            {
                typeCheck = BoardAuthCheckType.NoBoardId;
                sMessage = "게시판을 찾을 수 없습니다.";
            }
            //else if(0 >= idUser)
            //{
            //    typeReturn = BoardAuthCheckType.NoUserId;
            //}

            using (SpaNetCoreFoundationContext db1 = new SpaNetCoreFoundationContext())
            {
                //게시판이 사용할 수 있는 상태인지 확인한다.********************
                Board board = null;
                if (BoardAuthCheckType.Success == typeCheck)
                {
                    //게시판 찾기
                    board
                        = db1.Board
                            .Where(m => m.idBoard == idBoard)
                            .FirstOrDefault();


                    if (null == board)
                    {//게시판이 없다.
                        typeCheck = BoardAuthCheckType.NotFindBoard;
                        sMessage = "게시판을 찾을 수 없습니다.";
                    }
                    else if (BoardStateType.NotUse == board.BoardState)
                    {//사용할 수 없는 게시판이다.
                        typeCheck = BoardAuthCheckType.NotUseBoard;
                        sMessage = "사용할 수 없는 게시판입니다.";
                    }
                }



                //유저가 해당 게시판에 별도 권한을 가지고 있는지 확인*****************
                if (BoardAuthCheckType.Success == typeCheck)
                {
                    BoardAuthority bauth
                        = db1.BoardAuthority
                            .Where(m => m.idBoard == idBoard
                                    && m.idUser == idUser)
                            .FirstOrDefault();

                    if (null != bauth)
                    {//이 유저가 이 게시판에 별도 권한이 있다.
                        typeBoardAuth = bauth.Authority;
                    }
                    else
                    {//별도 권한이 없다.
                        //기본권한을 사용한다.
                        typeBoardAuth = board.AuthorityDefault;
                    }
                }

            }//end using db1


            return sMessage;
        }

    }//end class
}
