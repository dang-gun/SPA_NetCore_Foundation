using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using BoardModel;
using ApiModel;
using IdentityServer4.UserServices;
using System.Security.Claims;
using ModelDB;
using SPA_NetCore_Foundation.Global;
using SPA_NetCore_Foundation.Faculty;

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




                        //받은 정보 입력******************************************
                        //자기 리스트만 보일지 여부
                        bool bMyList = findBoard.BoardFaculty.HasFlag(BoardFacultyType.MyList);
                        if (true == bAdminMode)
                        {//어드민 모드이다.
                            //어드민 모드일때 자기리스트만 보이는 기능은
                            //전체리스트 보기로 바꿔준다.
                            bMyList = false;
                        }

                        //한화면 컨탠츠 수
                        if (true == findBoard.BoardFaculty.HasFlag(BoardFacultyType.ShowCount_Server))
                        {//서버 설정 사용
                            armResult.ShowCount = findBoard.ShowCount;
                        }
                        else
                        {//유저 설정 사용
                            armResult.ShowCount = nShowCount;
                        }

                        //페이지 번호
                        if (0 >= nPageNumber)
                        {//번호가 잘못됐다.
                            armResult.PageNumber = 1;
                        }
                        else
                        {//재대로됨
                            armResult.PageNumber = nPageNumber;
                        }



                        //게시물 검색***************************************
                        List<BoardPostListModel> listReturn = new List<BoardPostListModel>();


                        //공지 검색 - 전체 *************
                        BoardPostListModel[] bplNotice_All = null;
                        bplNotice_All
                            = (from bp in db1.BoardPost
                               from ui in db1.UserInfo
                                              .Where(a => a.idUser == bp.idUser)
                               from ui_f in db1.UserInfo
                                              .Where(a => a.idUser == bp.idUser_Forwarding)
                                              .DefaultIfEmpty()
                               where bp.PostState == BoardPostStateType.Notice_All
                               select new BoardPostListModel(bp, ui, ui_f, BoardItemType.NoticeAll))
                            .ToArray();
                        listReturn.AddRange(bplNotice_All);

                        //공지 검색 - 게시판 *************
                        BoardPostListModel[] bplNotice_Board = null;
                        bplNotice_Board
                            = (from bp in db1.BoardPost
                               from ui in db1.UserInfo
                                              .Where(a => a.idUser == bp.idUser)
                               from ui_f in db1.UserInfo
                                              .Where(a => a.idUser == bp.idUser_Forwarding)
                                              .DefaultIfEmpty()
                               where bp.idBoard == idBoard
                                    && bp.PostState == BoardPostStateType.Notice_Board
                               select new BoardPostListModel(bp, ui, ui_f, BoardItemType.NoticeBoard))
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

                        if (true == bMyList)
                        {//내 리스트만 표시기능 활성화
                            //내가 작성한 리스트 표시
                            iqBP = iqBP.Where(m => m.idUser == cm.id_int);
                        }

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
                             from ui_f in db1.UserInfo
                                            .Where(a => a.idUser == bp.idUser_Forwarding)
                                            .DefaultIfEmpty()
                             select new BoardPostListModel(bp, ui, ui_f, BoardItemType.None))
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
