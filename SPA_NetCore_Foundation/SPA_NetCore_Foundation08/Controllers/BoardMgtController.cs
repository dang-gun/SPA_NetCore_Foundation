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
using BoardMgtModel;

namespace SPA_NetCore_Foundation.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BoardMgtController : ControllerBase
    {
        #region 보드 어드민

        /// <summary>
        /// 테스트용으로 데이터를 300개 추가합니다.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<ApiResultBaseModel> TestPostAdd(
            [FromForm] int nBoardId)
        {
            ApiResultReady rrResult = new ApiResultReady(this);

            DateTime dtNow = DateTime.Now;
            int nCount = 0;

            //유저 정보 추출
            ClaimModel cm = new ClaimModel(((ClaimsIdentity)User.Identity).Claims);
            //이 유저가 해당 관리 등급이 있는지 확인한다.
            ManagementClassCheckType typePC
                = GlobalStatic.MgtA.MgtClassCheck(cm.id_int
                    , ManagementClassType.Admin);


            if (typePC == ManagementClassCheckType.Ok)
            {
                using (SpaNetCoreFoundationContext db1 = new SpaNetCoreFoundationContext())
                {
                    for (int i = 0; i < 300; ++i)
                    {
                        ++nCount;

                        //포스트
                        BoardPost newBP = new BoardPost();
                        newBP.idBoard = nBoardId;
                        newBP.idUser = cm.id_int;
                        newBP.Title = string.Format("테스트용 데이터 {0} - {1} ", nCount, dtNow);
                        newBP.WriteDate = dtNow;

                        db1.BoardPost.Add(newBP);
                        db1.SaveChanges();


                        //컨탠츠
                        BoardContent newBC = new BoardContent();
                        newBC.idBoard = newBP.idBoard;
                        newBC.idBoardPost = newBP.idBoardPost;
                        newBC.Content = string.Format("테스트용 내용 {0} - {1} ", nCount, dtNow);

                        db1.BoardContent.Add(newBC);
                        db1.SaveChanges();
                    }


                }//end using db1
            }
            else
            {
                //에러
                rrResult.InfoCode = ApiResultType.PermissionCheckError.ToString();
            }

            return rrResult.ToResult();
        }

        /// <summary>
        /// 게시판 관리자 - 생성된 게시판 리스트
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<BoardListResultModel> List()
        {
            ApiResultReady rrResult = new ApiResultReady(this);
            BoardListResultModel armResult = new BoardListResultModel();
            rrResult.ResultObject = armResult;

            //유저 정보 추출
            ClaimModel cm = new ClaimModel(((ClaimsIdentity)User.Identity).Claims);
            //이 유저가 해당 관리 등급이 있는지 확인한다.
            ManagementClassCheckType typePC
                = GlobalStatic.MgtA.MgtClassCheck(cm.id_int
                    , ManagementClassType.Admin);

            if (typePC == ManagementClassCheckType.Ok)
            {
                using (SpaNetCoreFoundationContext db1 = new SpaNetCoreFoundationContext())
                {
                    armResult.List = db1.Board.ToList();
                }//end using db1
            }
            else
            {
                //에러
                rrResult.InfoCode = typePC.ToString();
            }

            return rrResult.ToResult();
        }

        /// <summary>
        /// 선택한 게시판의 정보를 받는다.
        /// </summary>
        /// <param name="nBoardId"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<BoardListResultModel> Item(int nBoardId)
        {
            ApiResultReady rrResult = new ApiResultReady(this);
            BoardListResultModel armResult = new BoardListResultModel();
            rrResult.ResultObject = armResult;

            //유저 정보 추출
            ClaimModel cm = new ClaimModel(((ClaimsIdentity)User.Identity).Claims);
            //이 유저가 해당 관리 등급이 있는지 확인한다.
            ManagementClassCheckType typePC
                = GlobalStatic.MgtA.MgtClassCheck(cm.id_int
                    , ManagementClassType.Admin);

            if (typePC == ManagementClassCheckType.Ok)
            {
                using (SpaNetCoreFoundationContext db1 = new SpaNetCoreFoundationContext())
                {
                    armResult.List
                        = db1.Board
                            .Where(m => m.idBoard == nBoardId)
                            .ToList();
                }//end using db1
            }
            else
            {
                //에러
                rrResult.InfoCode = typePC.ToString();
                rrResult.Message = "권한이 없습니다.";
            }

            return rrResult.ToResult();
        }

        /// <summary>
        /// 계시판 정보 수정
        /// </summary>
        /// <param name="nBoardId"></param>
        /// <param name="sTitle"></param>
        /// <param name="nShowCount"></param>
        /// <param name="typeBoardState"></param>
        /// <param name="typeBoardFaculty"></param>
        /// <param name="nAuthorityDefault"></param>
        /// <param name="sMemo"></param>
        /// <returns></returns>
        [HttpPut]
        public ActionResult<ApiResultBaseModel> Edit(
            [FromForm] long nBoardId
            , [FromForm] string sTitle
            , [FromForm] short nShowCount
            , [FromForm] BoardStateType typeBoardState
            , [FromForm] BoardFacultyType typeBoardFaculty
            , [FromForm] BoardAuthorityType nAuthorityDefault
            , [FromForm] string sMemo)
        {
            ApiResultReady armResult = new ApiResultReady(this);

            //유저 정보 추출
            ClaimModel cm = new ClaimModel(((ClaimsIdentity)User.Identity).Claims);
            //이 유저가 해당 관리 등급이 있는지 확인한다.
            ManagementClassCheckType typePC
                = GlobalStatic.MgtA.MgtClassCheck(cm.id_int
                    , ManagementClassType.Admin);

            if (typePC == ManagementClassCheckType.Ok)
            {
                using (SpaNetCoreFoundationContext db1 = new SpaNetCoreFoundationContext())
                {
                    Board findBoard
                        = db1.Board
                            .Where(m => m.idBoard == nBoardId)
                            .FirstOrDefault();

                    if (null != findBoard)
                    {
                        findBoard.Title = sTitle;
                        findBoard.ShowCount = nShowCount;
                        findBoard.BoardState = typeBoardState;
                        findBoard.BoardFaculty = (BoardFacultyType)typeBoardFaculty;
                        findBoard.AuthorityDefault = (BoardAuthorityType)nAuthorityDefault;
                        findBoard.Memo = sMemo;

                        db1.SaveChanges();

                        //게시판 정보 json으로 저장
                        GlobalStatic.FileProc.WWW_Json_BoardInfo();
                    }
                    else
                    {
                        armResult.InfoCode = typePC.ToString();
                        armResult.Message = "대상이 없습니다.";
                    }

                }//end using db1
            }
            else
            {
                //에러
                armResult.InfoCode = ApiResultType.PermissionCheckError.ToString();
                armResult.Message = "권한이 없습니다.";
            }

            return armResult.ToResult();
        }

        /// <summary>
        /// 게시판 관리 - 게시판 생성
        /// </summary>
        /// <param name="sTitle"></param>
        /// <param name="typeBoardState"></param>
        /// <param name="nAuthorityDefault"></param>
        /// <param name="sMemo"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<ApiResultBaseModel> Create(
            [FromForm] string sTitle
            , [FromForm] BoardStateType typeBoardState
            , [FromForm] BoardAuthorityType nAuthorityDefault
            , [FromForm] string sMemo)
        {
            ApiResultReady rrResult = new ApiResultReady(this);
            

            DateTime dtNow = DateTime.Now;

            //유저 정보 추출
            ClaimModel cm = new ClaimModel(((ClaimsIdentity)User.Identity).Claims);
            //이 유저가 해당 관리 등급이 있는지 확인한다.
            ManagementClassCheckType typePC
                = GlobalStatic.MgtA.MgtClassCheck(cm.id_int
                    , ManagementClassType.Admin);

            if (typePC == ManagementClassCheckType.Ok)
            {
                using (SpaNetCoreFoundationContext db1 = new SpaNetCoreFoundationContext())
                {
                    Board newBoard = new Board();
                    newBoard.Title = sTitle;
                    newBoard.BoardState = typeBoardState;
                    newBoard.AuthorityDefault = (BoardAuthorityType)nAuthorityDefault;

                    //기능 설정
                    newBoard.BoardFaculty |= BoardFacultyType.ShowCount_Server;

                    newBoard.ShowCount = 10;
                    newBoard.Memo = sMemo;
                    newBoard.CreateDate = dtNow;

                    db1.Board.Add(newBoard);
                    db1.SaveChanges();


                    //게시판 정보 json으로 저장
                    GlobalStatic.FileProc.WWW_Json_BoardInfo();

                }//end using db1
            }
            else
            {
                //에러
                rrResult.InfoCode = ApiResultType.PermissionCheckError.ToString();
                rrResult.Message = "권한이 없습니다.";
            }

            return rrResult.ToResult();
        }

        #endregion

        #region 보드 어드민 - 게시판 개별 권한

        /// <summary>
        /// 권한 유저 추가.
        /// 권한을 별도로 지정하지 않고 수정을 통해 권한을 지정한다.
        /// </summary>
        /// <param name="nBoardId"></param>
        /// <param name="nUserId"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<ApiResultBaseModel> AuthAdd(
            [FromForm] long nBoardId
            , [FromForm] long nUserId)
        {
            ApiResultReady rrResult = new ApiResultReady(this);

            DateTime dtNow = DateTime.Now;


            //유저 정보 추출
            ClaimModel cm = new ClaimModel(((ClaimsIdentity)User.Identity).Claims);
            //이 유저가 해당 관리 등급이 있는지 확인한다.
            ManagementClassCheckType typePC
                = GlobalStatic.MgtA.MgtClassCheck(cm.id_int
                    , ManagementClassType.Admin);


            if (typePC == ManagementClassCheckType.Ok)
            {
                using (SpaNetCoreFoundationContext db1 = new SpaNetCoreFoundationContext())
                {
                    //게시판 찾기
                    Board findBoard
                        = db1.Board
                            .Where(m => m.idBoard == nBoardId)
                            .FirstOrDefault();

                    //유저 찾기
                    User findUser
                        = db1.User
                            .Where(m => m.idUser == nUserId)
                            .FirstOrDefault();

                    //이미 권한을 가지고 있는지 확인
                    BoardAuthority findBA
                        = db1.BoardAuthority
                            .Where(m => m.idBoard == nBoardId
                                    && m.idUser == nUserId)
                            .FirstOrDefault();


                    //게시판 있는지 확인
                    if ("0" == rrResult.InfoCode)
                    {
                        if (null == findBoard)
                        {
                            //게시판을 찾지 못했다.
                            rrResult.InfoCode = "-1";
                            rrResult.Message = "게시판을 찾을 수 없습니다.";
                        }
                    }


                    if ("0" == rrResult.InfoCode)
                    {
                        if (null == findUser)
                        {
                            //유저를 찾지 못했다.
                            rrResult.InfoCode = "-2";
                            rrResult.Message = "유저를 찾을 수 없습다.";
                        }
                    }

                    if ("0" == rrResult.InfoCode)
                    {
                        if (null != findBA)
                        {
                            //이미 권한을 가지고 있다.
                            rrResult.InfoCode = "-3";
                            rrResult.Message = "이미 권한을 가지고 있습니다.";
                        }
                    }


                    if ("0" == rrResult.InfoCode)
                    {
                        //유저 정보
                        UserInfo findUI
                            = db1.UserInfo
                                .Where(m => m.idUser == nUserId)
                                .FirstOrDefault();

                        //추가할 권한 정보
                        BoardAuthority newBA = new BoardAuthority();
                        newBA.idBoard = findBoard.idBoard;
                        newBA.idUser = findUI.idUser;
                        //게시판의 기본권을 먼저 준다.
                        newBA.Authority = findBoard.AuthorityDefault;
                        newBA.AuthState = BoardAuthorityStateType.Use;
                        newBA.Memo = string.Empty;
                        newBA.EditDate = dtNow;

                        db1.BoardAuthority.Add(newBA);
                        db1.SaveChanges();
                    }


                }//end using db1
            }//end typePC


            return rrResult.ToResult();
        }

        /// <summary>
        /// 보드 권한 리스트
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<BoardAuthListResultModel> AuthList(long nBoardId)
        {
            ApiResultReady rrResult = new ApiResultReady(this);
            BoardAuthListResultModel armResult = new BoardAuthListResultModel();
            rrResult.ResultObject = armResult;

            //유저 정보 추출
            ClaimModel cm = new ClaimModel(((ClaimsIdentity)User.Identity).Claims);
            //이 유저가 해당 관리 등급이 있는지 확인한다.
            ManagementClassCheckType typePC
                = GlobalStatic.MgtA.MgtClassCheck(cm.id_int
                    , ManagementClassType.Admin);

            if (typePC == ManagementClassCheckType.Ok)
            {
                using (SpaNetCoreFoundationContext db1 = new SpaNetCoreFoundationContext())
                {
                    IQueryable<BoardAuthority> iqBAuth
                        = db1.BoardAuthority
                            .Where(m => m.idBoard == nBoardId);

                    armResult.List
                        = (from bauth in iqBAuth
                           join user in db1.UserInfo
                             on bauth.idUser equals user.idUser
                           select new BoardAuthItemModel
                           {
                               idBoardAuthority = bauth.idBoardAuthority,
                               idBoard = bauth.idBoard,
                               idUser = bauth.idUser,
                               Authority = bauth.Authority,
                               AuthState = bauth.AuthState,
                               UserName = user.ViewName,
                               SignEmail = "",
                               Memo = bauth.Memo,
                               EditDate = bauth.EditDate
                           })
                           .ToList();


                }//end using db1
            }

            return rrResult.ToResult();
        }

        /// <summary>
        /// 게시판 권한 수정
        /// </summary>
        /// <param name="nBoardAuthority"></param>
        /// <param name="nAuthority"></param>
        /// <param name="nAuthState"></param>
        /// <param name="sMemo"></param>
        /// <returns></returns>
        [HttpPatch]
        public ActionResult<ApiResultBaseModel> AuthEdit(
            [FromForm] long nBoardAuthority
            , [FromForm] int nAuthority
            , [FromForm] int nAuthState
            , [FromForm] string sMemo)
        {
            ApiResultReady rrResult = new ApiResultReady(this);

            DateTime dtNow = DateTime.Now;

            //유저 정보 추출
            ClaimModel cm = new ClaimModel(((ClaimsIdentity)User.Identity).Claims);
            //이 유저가 해당 관리 등급이 있는지 확인한다.
            ManagementClassCheckType typePC
                = GlobalStatic.MgtA.MgtClassCheck(cm.id_int
                    , ManagementClassType.Admin);

            if (typePC == ManagementClassCheckType.Ok)
            {
                using (SpaNetCoreFoundationContext db1 = new SpaNetCoreFoundationContext())
                {
                    //수정할 권한 찾기
                    BoardAuthority findBA
                        = db1.BoardAuthority
                            .Where(m => m.idBoardAuthority == nBoardAuthority)
                            .FirstOrDefault();

                    if (null == findBA)
                    {
                        rrResult.InfoCode = "-1";
                        rrResult.Message = "수정할 대상을 찾지 못했습니다.";
                    }
                    else
                    {
                        findBA.Authority = (BoardAuthorityType)nAuthority;
                        findBA.AuthState = (BoardAuthorityStateType)nAuthState;
                        findBA.Memo = sMemo;
                        findBA.EditDate = dtNow;

                        db1.SaveChanges();
                    }

                }//end using db1
            }


            return rrResult.ToResult();
        }

        #endregion

    }//end class
}
