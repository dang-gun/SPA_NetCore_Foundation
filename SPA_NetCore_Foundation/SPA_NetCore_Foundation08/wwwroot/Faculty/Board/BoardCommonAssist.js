
/*
 * 게시판 기능 지원용
*/


var BoardCA = {};

/** 게시판 정보 */
BoardCA.BoardInfoList = [{}];

/* ◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇
 * 게시판 html 기본경로
 * ◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇*/
BoardCA.FileUrl = {};

BoardCA.FileUrl.BoardInfoJson = "/Faculty/Board/BoardInfo.json";

BoardCA.FileUrl.Body = "/Faculty/Board/BoardCommon_Body.html"
BoardCA.FileUrl.Body_Div = "/Faculty/Board/BoardCommon_Body_Div.html"
BoardCA.FileUrl.Body_DivAd = "/Faculty/Board/BoardCommon_Body_DivAd.html"
BoardCA.FileUrl.ListItem = "/Faculty/Board/BoardCommon_ListItem.html"
BoardCA.FileUrl.ListItem_BigThumbnail = "/Faculty/Board/BoardCommon_ListItem_BigThumbnail.html"
BoardCA.FileUrl.PostCreate = "/Faculty/Board/BoardCommon_PostCreate.html"
BoardCA.FileUrl.PostEdit = "/Faculty/Board/BoardCommon_PostEdit.html"
BoardCA.FileUrl.PostReply_ListItem = "/Faculty/Board/BoardCommon_PostReply_ListItem.html"
BoardCA.FileUrl.PostView = "/Faculty/Board/BoardCommon_PostView.html"
BoardCA.FileUrl.PostView_SummaryList = "/Faculty/Board/BoardCommon_PostView_SummaryList.html"
BoardCA.FileUrl.Title = "/Faculty/Board/BoardCommon_Title.html"
BoardCA.FileUrl.Title_Empty = "/Faculty/Board/BoardCommon_Title_Empty.html"
BoardCA.FileUrl.VideoInsert = "/Faculty/Board/BoardCommon_VideoInsert.html"

BoardCA.FileUrl.Summary_ListItem = "/Faculty/Board/BoardSummary_ListItem.html"
BoardCA.FileUrl.Summary_ListItem_BigThumbnail = "/Faculty/Board/BoardSummary_ListItem_BigThumbnail.html"
BoardCA.FileUrl.Summary_ListItem_BigThumbnail_Favorites = "/Faculty/Board/BoardSummary_ListItem_BigThumbnail_Favorites.html"
BoardCA.FileUrl.Summary_NoThumbnailListItem = "/Faculty/Board/BoardSummary_NoThumbnailListItem.html"

/* ◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇
 * 게시판에 사용될 데이터
 * ◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇*/

/** 게시판 모드 타입 */
BoardCA.ModeType = {
    /** 
     *  기본.
     *  일반적인 게시판 형태
     * */
    Defult: 0,

    /** 
     *  무한 스크롤 방식.
     *  스크롤이 하단에 오면 새로 바인딩 한다.
     *  스크롤 판단은 외부에서 한후 'BoardCA.PageMoveNext()'를 호출해준다.
     */
    InfiniteScroll: 1,

};

/** 게시판 즐겨찾기 타입 */
BoardCA.BoardFavoritesType = {
    /** 
     * 표시안함
     * */
    None: 0,

    /** 
     * 추가되있음
     */
    Add: 1,

    /** 없다 */
    Nothing: 2,
};



/* ◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇
 * 게시판 유틸 기능
 * ◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇*/

/** 게시판에 사용하는 정규식 */
BoardCA.RegExr = {};
/** 마크다운에 들어있는 이미지의 내용을 제거하는 코드 */
//BoardCA.RegExr.ImageCut = /(?<=\(data:image).*?(?=\))/g; // 크롬에서만 동작
BoardCA.RegExr.ImageCut = /(?=\(data:image).*?(?=\))/g;

/**
 * 마크다운에서 base64로 들어있는 이미지를 제외하고 글자수를 검사한다.
 * @param {string} sMarkdown 검사할 마크다운 문자열
 * @returns {int} 이미지를 제외한 글자수
 */
BoardCA.Markdown_ImageRemove_Lentgh = function (sMarkdown)
{
    return sMarkdown.replace(BoardCA.RegExr.ImageCut, "").length;
};


/** 
 *  유튜브 주소 추출 - url 쿼리
 *  예>https://www.youtube.com/watch?v=0K-kYJmrBz0 , https://youtu.be/0K-kYJmrBz0
 * */
BoardCA.RegExr.Youtube_ID = /^.*((youtu.be\/)|(v\/)|(\/u\/\w\/)|(embed\/)|(watch\?))\??v?=?([^#\&\?]*).*/;

/** 비디오 삽입시 딜레이를 주기위한 타이머 */
BoardCA.VideoInsertTimer = 0;


/** 선택된 비디오 방식 */
BoardCA.VideoType =
{
/** 없음 */
    None: 0,

/** 유튜브 */
    YouTube: 1,
/** 엑스비디오 */
    XVideos: 2
};

/** 선택된 비디오 방식 */
BoardCA.VideoSelectValue = BoardCA.VideoType.YouTube;

/**
 * 비디오 선택
 * @param {any} typeVideo
 */
BoardCA.VideoSelect = function (typeVideo)
{
    BoardCA.VideoSelectValue = typeVideo;

    var sMsg = "";

    switch (typeVideo)
    {
        case BoardCA.VideoType.YouTube:
            sMsg = "입력할 유튜브 주소를 넣으세요.";
            break;
        case BoardCA.VideoType.XVideos:
            sMsg = "영상 주소를 넣으세요.";
            break;
    }

    $("#txtVideoUrl").attr("placeholder", sMsg);
};

/**
 * 비디오 삽입 인터페이스의 온체인지 이벤트를 받아 처리
 * @param {any} event
 */
BoardCA.VideoInsertHtml_OnChange = function (event)
{
    var sUrl = $("#txtVideoUrl").val();
    var sHtml = "";

    switch (BoardCA.VideoSelectValue)
    {
        case BoardCA.VideoType.YouTube:
            {
                var matchs = sUrl.match(BoardCA.RegExr.Youtube_ID);

                if ((null !== matchs)
                    && (7 <= matchs.length))
                {
                    sHtml = '<iframe width="420" height="315" src="https://www.youtube.com/embed/' + matchs[7] + '" frameborder="0" allow="accelerometer; autoplay; encrypted-media; gyroscope; picture-in-picture" allowfullscreen></iframe>';

                    if (0 < BoardCA.VideoInsertTimer)
                    {//기존 타이머가 있다.
                        //기존 타이머 제거
                        clearTimeout(BoardCA.VideoInsertTimer);
                    }
                }
            }
            break;
        case BoardCA.VideoType.XVideos:
            {
                sHtml = `<video class="video-js" controls autoplay preload="none" poster="http://vjs.zencdn.net/v/oceans.png" data-setup="{}">`
                    //+ `<source src="` + sUrl + `">`
                    + `<source src="https://vid4-l3.xvideos-cdn.com/` + sUrl + `">`
                    + `<p class="vjs-no-js">To view this video please enable JavaScript, and consider upgrading to a web browser that <a href="https://videojs.com/html5-video-support/" target="_blank">supports HTML5 video</a></p>`
                    + `</video>`;
            }
            break;
    }


    if ("" !== sHtml)
    {
        //바인딩 타이머 예약
        BoardCA.VideoInsertTimer
            = setTimeout(function () { $("#divViewVideo").html(sHtml); }, 1000);
    }
    
};

/**
 * 유튜브 삽입
 * @param {any} event
 */
BoardCA.VideoInsertHtml_OnClick = function (event)
{
    event.stopPropagation();

    var sUrl = $("#txtVideoUrl").val();

    switch (BoardCA.VideoSelectValue)
    {
        case BoardCA.VideoType.YouTube:
            {
                var matchs = sUrl.match(BoardCA.RegExr.Youtube_ID);

                if ((null !== matchs)
                    && (7 <= matchs.length))
                {
                    //삽입 시도
                    //TUI2.YoutubeInsert(GlobalStatic.Page_Now.BoardComm.Editor, matchs[7]);
                    GlobalStatic.Page_Now.BoardComm
                        .YoutubeInsert(GlobalStatic.Page_Now.BoardComm.Editor, matchs[7]);
                    DG_Popup.Close();
                }
            }
            break;
        case BoardCA.VideoType.XVideos:
            {
                //삽입 시도
                //TUI2.VideoJsInsert(GlobalStatic.Page_Now.BoardComm.Editor, sUrl);
                GlobalStatic.Page_Now.BoardComm
                    .VideoJsInsert(GlobalStatic.Page_Now.BoardComm.Editor, sUrl);
                DG_Popup.Close();
            }
            break;

        default:
            DG_Popup.Close();
            break;
    }


};


/* ◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇
 * 게시판 주요 기능
 * ◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇*/

/**
 * 게시물을 보기위해 쿼리를 만들어 호출한다.
 */
BoardCA.ListLink = function ()
{
    //쿼리가 있으면 제거
    var sCutHash = location.hash.split("?")[0];

    location.href = sCutHash + "?bid=" + GlobalStatic.Page_Now.BoardID;
};




/**
 * 이미지를 본문에 추가한다.
 * 이것은 BoardCommon기능이 아니라 DG_JsFileSelector기능이다.
 * @param {any} event
 */
BoardCA.Tools_InsertImageClick = function (event)
{
    GlobalStatic.Page_Now.BoardComm.dgFS.OpenFileSelection();
};

/** 비디오 입력창 표시 */
BoardCA.Tools_InsertVideoClick = function ()
{
    GlobalStatic.Page_Now.BoardComm.InsertVideoShow();
};



/**
 * 리스트 전체 다시 바인딩
 * @param {any} nPageNum
 * @param {bool} bItem 아이템도 다시 그릴지 여부
 */
BoardCA.Reset_List = function (nPageNum, bItem)
{
    GlobalStatic.Page_Now.BoardComm.BindTitle(
        GlobalStatic.Page_Now.BoardID
        , bItem
        , nPageNum
        , GlobalStatic.Page_Now.BoardComm.FirstBind_Callback);

    GlobalStatic.Page_Now.BoardComm.FirstBind_Callback = null;
};

/**
 * 리스트 바인딩.
 * 현제 페이지를 다시 그린다.
 */
BoardCA.ListShowBind = function ()
{
    //쿼리가 있는지 확인
    var jsonQuery = getParamsSPA();
    //페이지 번호 받기
    var nPN = dgIsObject.IsIntValue(jsonQuery["pn"]);
    if (0 >= nPN)
    {//0이하이면
        //1로 초기화
        nPN = 1;
    }

    GlobalStatic.Page_Now.BoardComm.BindTitle(
        GlobalStatic.Page_Now.BoardID
        , true
        , nPN
        , null);
};

/**
 * 게시물을 보기위해 쿼리를 만들어 호출한다.
 * @param {int} nPostView 볼 게시물 번호
 */
BoardCA.PostViewLink = function (nPostView)
{
    var nPV = dgIsObject.IsIntValue(nPostView);

    //쿼리가 있으면 제거
    var sCutHash = location.hash.split("?")[0];

    location.href = sCutHash + "?bid=" + GlobalStatic.Page_Now.BoardID
        + "&pvid=" + nPV;
};

/**
 * 게시물을 보기위해 쿼리를 만들어 호출한다.
 * @param {int} nBoardId 볼 게시판 아이디
 * @param {int} nPostView 볼 게시물 번호
 */
BoardCA.PostViewLink_Board = function (nBoardId, nPostView)
{
    var nPV = dgIsObject.IsIntValue(nPostView);

    //쿼리가 있으면 제거
    var sCutHash = location.hash.split("?")[0];

    location.href = sCutHash + "?bid=" + nBoardId
        + "&pvid=" + nPV;
};

/**
 * 게시물을 보기위해 쿼리를 만들어 호출한다.
 * @param {int} sUrl 볼 게시판의 주소
 * @param {int} nBoardId 볼 게시판 아이디
 * @param {int} nPostView 볼 게시물 번호
 */
BoardCA.PostViewLink_MoveBoard = function (sUrl,nBoardId, nPostView)
{
    var nPV = dgIsObject.IsIntValue(nPostView);

    location.href = sUrl + "/?bid=" + nBoardId
        + "&pvid=" + nPV;
};

BoardCA.PostViewLinkGet_MoveBoard = function (sUrl, nBoardId, nPostView)
{
    
    return sUrl + "/?bid=" + nBoardId
        + "&pvid=" + nPostView;
};

 /**
  * 이 클래스를 사용하는 액션이 호출할 함수
  * @param {bool} bItem 아이템도 다시 그릴지 여부
  */
BoardCA.Action = function (bItem)
{
    var bItemTemp = bItem ? bItem : true;

    //쿼리가 있는지 확인
    var jsonQuery = getParamsSPA();

    if ((undefined !== jsonQuery["bid"])
        && (undefined !== jsonQuery["pvid"]))
    {//보드 아이디가 있고
        //포스트 뷰 아이디가 있다.

        //게시물 보기다.
        BoardCA.PostView(dgIsObject.IsIntValue(jsonQuery["pvid"]));
    }
    else
    {
        //리스트를 다시 만들어준다.
        BoardCA.Reset_List(jsonQuery["pn"], bItemTemp);
    }

};


//◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇
// 즐겨찾기 관련
//◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇

/** 즐겨찾기 제목 */
BoardCA.FavoritesTitle = "즐겨찾기";

/**
 * 즐겨찾기 추가
 * @param {any} domBtn
 * @param {any} idBoardPost
 */
BoardCA.FavoritesAdd = function (domBtn, idBoardPost)
{
    var domBtnTemp = $(domBtn);

    AA.post(AA.TokenRelayType.HeadAdd,
        {
            url: FS_Api.BoardFavorites_Add
            , data: { idBoardPost: idBoardPost }
            , success: function (jsonData)
            {
                if ("0" === jsonData.InfoCode)
                {//에러 없음
                    GlobalStatic.MessageBox_Info(BoardCA.FavoritesTitle
                        , "즐겨찾기에 추가되었습니다.");

                    //css 수정
                    domBtnTemp.addClass("BoardFavorites_Add");
                    domBtnTemp.removeClass("BoardFavorites_Nothing");
                }
                else
                {//에러 있음
                    GlobalStatic.MessageBox_Error(
                        BoardCA.FavoritesTitle
                        , jsonData.Message);
                }
            }
            , error: function (error)
            {
                console.log(error);
            }
        });
};

/**
 * 즐겨찾기 제거
 * @param {any} domBtn
 * @param {any} idBoardPost
 */
BoardCA.FavoritesDelete = function (domBtn, idBoardPost)
{
    var domBtnTemp = $(domBtn);

    AA.delete(AA.TokenRelayType.HeadAdd,
        {
            url: FS_Api.BoardFavorites_Delete
            , data: { idBoardPost: idBoardPost }
            , success: function (jsonData)
            {
                if ("0" === jsonData.InfoCode)
                {//에러 없음
                    GlobalStatic.MessageBox_Info(
                        BoardCA.FavoritesTitle
                        , "즐겨찾기에서 제외됐습니다.");

                    //css 수정
                    domBtnTemp.removeClass("BoardFavorites_Add");
                    domBtnTemp.addClass("BoardFavorites_Nothing");

                }
                else
                {//에러 있음
                    GlobalStatic.MessageBox_Error(
                        BoardCA.FavoritesTitle
                        , jsonData.Message);
                }
            }
            , error: function (error)
            {
                console.log(error);
            }
        });
};

BoardCA.FavoritesAuto = function (domBtnThis, event, idBoardPost)
{
    var domBtn = $(domBtnThis);
    var nBP = dgIsObject.IsIntValue(idBoardPost);


    if (true === domBtn.hasClass("BoardFavorites_Add"))
    {//추가되있는 항목이다.
        //제외시킨다.
        BoardCA.FavoritesDelete(domBtn, nBP);
    }
    else
    {//제외된 항목이다.
        //추가 시킨다.
        BoardCA.FavoritesAdd(domBtn, nBP);
    }
    
};


//◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇
// 포스팅 보기
//◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇

/** 포스트 보기 요청 */
BoardCA.PostView = function (nPostView)
{
    var nPV = dgIsObject.IsIntValue(nPostView);
    GlobalStatic.Page_Now.BoardComm.PostView(GlobalStatic.Page_Now.BoardID, nPV);
};

/** 댓글 작성 요청 */
BoardCA.PostReplyCreate = function ()
{
    //쿼리가 있는지 확인
    var jsonQuery = getParamsSPA();

    GlobalStatic.Page_Now.BoardComm.PostReplyCreate(
        GlobalStatic.Page_Now.BoardID
        , dgIsObject.IsIntValue(jsonQuery["pvid"]));
};



/** 포스트 수정 표시 */
BoardCA.PostEditShow = function (nPostView)
{
    var nPV = dgIsObject.IsIntValue(nPostView);
    GlobalStatic.Page_Now.BoardComm.PostEditShow(GlobalStatic.Page_Now.BoardID, nPV);
};
/** 포스트 수정 요청 */
BoardCA.PostEdit = function (nPostView)
{
    var nPV = dgIsObject.IsIntValue(nPostView);
    GlobalStatic.Page_Now.BoardComm.PostEdit(GlobalStatic.Page_Now.BoardID, nPV);
};
/** 포스트 삭제 표시 */
BoardCA.PostDeleteShow = function (nPostView)
{
    var nPV = dgIsObject.IsIntValue(nPostView);
    GlobalStatic.Page_Now.BoardComm.PostDeleteView(GlobalStatic.Page_Now.BoardID, nPV);
};



/** 글쓰기 화면 표시 */
BoardCA.PostCreateShow = function ()
{
    GlobalStatic.Page_Now.BoardComm.PostCreateShow(GlobalStatic.Page_Now.BoardID);
};
/** 글쓰기 시도 */
BoardCA.PostCreate = function ()
{
    GlobalStatic.Page_Now.BoardComm.PostCreate();
};


/** 목록으로 돌아가기 */
BoardCA.ListView = function ()
{
    //리스트를 다시 만들어준다.
    BoardCA.Reset_List(1);
};

/** 가지고 있는 페이지에서 다음 페이지로 이동한다. */
BoardCA.PageMoveNext = function ()
{
    if (typeof GlobalStatic.Page_Now.BoardComm === "object")
    {
        GlobalStatic.Page_Now.BoardComm.PageMoveNext(GlobalStatic.Page_Now.BoardID);
    }
    
};

/**
 * 주소줄을 갱신하면서 다음 지정한 페이지로 이동한다.
 * @param {int} nPageNum 이동할 페이지 번호. -2100:맨앞으로, -1100:맨뒤로
 */
BoardCA.PageMoveLink = function (nPageNum)
{
    var nTagertPageNum = 0;

    if (-2100 === nPageNum)
    {
        //맨앞으로
        nTagertPageNum = 1;
    }
    else if (-1100 === nPageNum)
    {
        //맨 뒤로
        nTagertPageNum = GlobalStatic.Page_Now.BoardComm.PageTotal;
    }
    else
    {
        nTagertPageNum = nPageNum;
    }

    //완성된 페이지 번호
    var nPN = dgIsObject.IsIntValue(nTagertPageNum);

    //쿼리가 있으면 제거
    var sCutHash = location.hash.split("?")[0];

    //주소 완성하기
    location.href = sCutHash + "?bid=" + GlobalStatic.Page_Now.BoardID
        + "&pn=" + nPN;
};

/**
 * 댓글 보기/가리기를
 * 자동으로 처리한다.
 */
BoardCA.PostReplayShowHide_Auto = function ()
{
    //대상 찾기
    var domBoardComm_Reply_List = $(".BoardComm_Reply_List");

    //대상에 가리기 클래스가 있는지 확인
    var bShow = domBoardComm_Reply_List.hasClass("v-hidden");

    if (true === bShow)
    {//가리기 상태다
        //보기 상태로 변경한다.
        GlobalStatic.Page_Now.BoardComm.PostReplayShowHide(true);
    }
    else
    {//보기 상태다
        //가리가 상태로 변경
        GlobalStatic.Page_Now.BoardComm.PostReplayShowHide(false);
    }

};

/**
 * 대댓글 작성
 * @param {any} idBoardPost
 * @param {any} idBoardReply_Target
 */
BoardCA.PostReReplayCreateShow = function (
    idBoardPost
    , idBoardReply_Target)
{
    GlobalStatic.Page_Now.BoardComm.PostReReplyCreateShow(
        GlobalStatic.Page_Now.BoardID
        , idBoardPost
        , idBoardReply_Target);
};

BoardCA.PostReReplyCreate = function (btnThis)
{
    //쿼리가 있는지 확인
    var jsonQuery = getParamsSPA();

    var btn = $(btnThis);

    //대상 받기
    //var idBoardPost = $(btnThis).attr("idBoardPost");
    var idBoardReply_Target = dgIsObject.IsIntValue(btn.attr("idBoardReply"));

    GlobalStatic.Page_Now.BoardComm.PostReplyCreate(
        GlobalStatic.Page_Now.BoardID
        , dgIsObject.IsIntValue(jsonQuery["pvid"])
        , idBoardReply_Target);
};


/**
 * 대댓글 취소
 */
BoardCA.PostReReplyCreateCancel = function ()
{
    GlobalStatic.Page_Now.BoardComm.PostReReplyCreateCancel();
};



/**
 * 대댓글 리스트 바인딩
 * @param {any} idBoardReply_Target 리스트를 불러올 부모 댓글 아이디
 */
BoardCA.ReRePlayList = function (idBoardPost, idBoardReply_Target)
{
    GlobalStatic.Page_Now.BoardComm.PostReReplyList(
        GlobalStatic.Page_Now.BoardID
        , idBoardPost
        , idBoardReply_Target);
};




/* ◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇
 * 게시판 요약 기능
 * ◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇*/

BoardCA.Summary = {};

BoardCA.Summary.BoardSummary_ListItemHtml = "";
BoardCA.Summary.BoardSummary_ListItem_BigThumbnailHtml = "";
BoardCA.Summary.BoardSummary_ListItem_BigThumbnail_FavoritesHtml = "";
BoardCA.Summary.BoardSummary_NoThumbnailListItemHtml = "";

BoardCA.Summary.Load = function ()
{
    AA.HtmlFileLoad(BoardCA.FileUrl.Summary_ListItem
        , function (html)
        {
            BoardCA.Summary.BoardSummary_ListItemHtml = html;
        }
        , {}
    );

    AA.HtmlFileLoad(BoardCA.FileUrl.Summary_ListItem_BigThumbnail
        , function (html)
        {
            BoardCA.Summary.BoardSummary_ListItem_BigThumbnailHtml = html;
        }
        , {}
    );
    AA.HtmlFileLoad(BoardCA.FileUrl.Summary_ListItem_BigThumbnail_Favorites
        , function (html)
        {
            BoardCA.Summary.BoardSummary_ListItem_BigThumbnail_FavoritesHtml = html;
        }
        , {}
    );

    AA.HtmlFileLoad(BoardCA.FileUrl.Summary_NoThumbnailListItem
        , function (html)
        {
            BoardCA.Summary.BoardSummary_NoThumbnailListItemHtml = html;
        }
        , {}
    );
};

BoardCA.Summary.BindItem_Count = function (jsonOption)
{
    var objThis = this;

    var jsonOptDefult = {
        /** 사용할 돔 */
        domDiv: null,
        /** 표시할 게시판들 */
        arrBoardId: [0],
        /** 리스트 요청에 사용 */
        sApiUrl_List: FS_Api.Board_SummaryList,
        /** 그릴때 사용할 html */
        ItemHtml: objThis.BoardSummary_NoThumbnailListItemHtml,
        /** 리스트로 표시할 개수 */
        nShowCount: 5,

        /** 검색어 */
        SearchWord: ""
    };

    var jsonOpt = Object.assign(jsonOptDefult, jsonOption);

    
    
    var domDivTemp = $(jsonOpt.domDiv);
    var arrBoardIdTemp = jsonOpt.arrBoardId;
    var nShowCountTemp = jsonOpt.nShowCount;


    AA.get(AA.TokenRelayType.CaseByCase
        , {
            url: jsonOpt.sApiUrl_List
            , data: {
                sBoardIdList: JSON.stringify(arrBoardIdTemp)
                , nShowCount: nShowCountTemp
                , sSearchWord: jsonOpt.SearchWord
            }
            , success: function (jsonData)
            {
                if ("0" === jsonData.InfoCode)
                {//에러 없음
                    //아이템 리스트
                    BoardCA.Summary.BindItem_Items(
                        domDivTemp
                        , jsonData.List
                        , jsonOpt.ItemHtml);
                }
                else
                {//에러 있음
                    //요약은 표시를 하지 않을뿐 피드백이 없다.
                }
            }
            , error: function (error)
            {
                console.log(error);
            }
        });
};

/**
 * 
 * @param {any} domDiv
 * @param {any} arrJsonData
 * @param {any} sItemHtml
 */
BoardCA.Summary.BindItem_Items = function (
    domDiv
    , arrJsonData
    , sItemHtml )
{
    var objThis = this;

    //기존 아이템 지우기
    domDiv.empty();


    //리스트 html을 임시 저장
    var sHtmlTemp = "";

    //컨탠츠 추가***************
    for (var i = 0; i < arrJsonData.length; ++i)
    {
        var jsonItemData = arrJsonData[i];

        //Url 만들기
        //게시판 정보 리스트에서 주소 받기
        var sBoardUrl
            = BoardCA.BoardInfoList
                .find(el => el.idBoard === jsonItemData.idBoard)
                .UrlStandard;
        jsonItemData.Url = sBoardUrl
            + "?bid=" + jsonItemData.idBoard
            + "&pvid=" + jsonItemData.idBoardPost;

        //썸네일 주소
        if ("" !== jsonItemData.ThumbnailUrl
            && null !== jsonItemData.ThumbnailUrl)
        {
            jsonItemData.ThumbnailFullUrl = jsonItemData.ThumbnailUrl + "Thumbnail_256x256.png";
            jsonItemData.ThumbnailStyle = "";
        }
        else
        {//썸네일이 없다.
            jsonItemData.ThumbnailUrl = "";
            jsonItemData.ThumbnailStyle = "visibility:hidden";
        }

        //즐겨찾기 여부
        switch (jsonItemData.FavoritesType)
        {
            case BoardCA.BoardFavoritesType.None:
                jsonItemData.FavoritesCss = "d-none";
                break;
            case BoardCA.BoardFavoritesType.Add:
                jsonItemData.FavoritesCss = " BoardFavorites_Add";
                break;
            case BoardCA.BoardFavoritesType.Nothing:
                jsonItemData.FavoritesCss = " BoardFavorites_Nothing";
                break;

            default:
                jsonItemData.FavoritesCss = "";
                break;
        }

        sHtmlTemp
            += GlobalStatic.DataBind.DataBind_All(
                sItemHtml
                , jsonItemData)
                .ResultString;

    }

    //대상에 추가
    //domDiv.html(sHtmlTemp);
    domDiv.append($(sHtmlTemp));
};