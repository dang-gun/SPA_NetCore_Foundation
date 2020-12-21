
/**
 * 게시판 공통 기능
 * @param {int} nBoardID 사용할 보드 번호
 * @param {bool} bItem 아이템도 다시 그릴지 여부
 * @param {json} jsonOption 생성 옵션
 * @param {function} callback 바인딩이 끝나면 동작시킬 콜백
 */
function BoardCommon(
    nBoardID
    , bItem
    , jsonOption
    , callback)
{
    var objThis = this;

    //게시판 번호 백업
    objThis.BoardID = nBoardID;
    //해쉬 이후 url
    objThis.UrlHash = BoardCA.UrlHash();

    //옵션 합치기
    objThis.BoardOption = Object.assign({},objThis.jsonOption_Defult, jsonOption);

    //영역 백업
    objThis.TableArea = objThis.BoardOption.TableArea;
    //제목 백업
    objThis.MessageTitle = objThis.BoardOption.BoardTitle;

    var nbItemTemp = bItem;
    objThis.FirstBind_Callback = callback;

    //필수 로드 사항********************************************
    AA.HtmlFileLoad(objThis.BoardOption.FileUrl_Body
        , function (html)
        {
            objThis.BoardCommon_BodyHtml = html;
            //게시판 초기화
            objThis.Initialize(nbItemTemp);
        }
        , {}
    );
    AA.HtmlFileLoad(objThis.BoardOption.FileUrl_Title
        , function (html)
        {
            objThis.BoardCommon_TitleHtml = html;
            //게시판 초기화
            objThis.Initialize(nbItemTemp);
        }
        , {}
    );
    AA.HtmlFileLoad(objThis.BoardOption.FileUrl_ListItem
        , function (html)
        {
            objThis.BoardCommon_ListItemHtml = html;
            //게시판 초기화
            objThis.Initialize(nbItemTemp);
        }
        , {}
    );
    AA.HtmlFileLoad(objThis.BoardOption.FileUrl_PostView
        , function (html)
        {
            objThis.BoardCommon_PostViewHtml = html;
            //게시판 초기화
            objThis.Initialize(nbItemTemp);
        }
        , {}
    );
    AA.HtmlFileLoad(objThis.BoardOption.FileUrl_PostReply_ListItem
        , function (html)
        {
            objThis.BoardCommon_PostReply_ListItemHtml = html;
            //게시판 초기화
            objThis.Initialize(nbItemTemp);
        }
        , {}
    );


    //로드를 기다릴필요가 없는 html
    AA.HtmlFileLoad(objThis.BoardOption.FileUrl_PostCreate
        , function (html)
        {
            objThis.BoardCommon_PostCreateHtml = html;
        }
        , {}
    );
    AA.HtmlFileLoad(objThis.BoardOption.FileUrl_PostEdit
        , function (html)
        {
            objThis.BoardCommon_PostEditHtml = html;
        }
        , {}
    );
    AA.HtmlFileLoad(objThis.BoardOption.FileUrl_VideoInsert
        , function (html)
        {
            objThis.BoardCommon_VideoInsertHtml = html;
        }
        , {}
    );
}

/** 게시판 생성 기본 옵션 */
BoardCommon.prototype.jsonOption_Defult = {
    /** 게시판 영역 */
    TableArea: null,
    /** 
     *  부모 영역
     *  게시판을 스크롤하고 있는 부모의 영역.
     *  무한 스크롤모드를 사용하는 경우 필수로 전달해야 한다.
     * */
    ParentArea: null,
    /**
     *  부모 영역
     *  게시판을 스크롤하고 있는 부모의 영역.
     *  조건에 따라 스크롤 영역이 변하는 경우 사용하는 영역
     *  
     *  'ParentArea'가 동작할때는 'ParentArea2'가 동작하면 안되고
     *  반대의 경우도 마찬가지다.
     * */
    ParentArea2: null,
    /** 게시판 타이틀 */
    BoardTitle: "게시판",

    /** 게시판 영역에 바인딩될 몸통 html 주소 */
    FileUrl_Body: BoardCA.FileUrl.Body,
    /** 게시판 제목에 사용될 html 주소 */
    FileUrl_Title: BoardCA.FileUrl.Title,
    /** 게시판 항목에 사용될 html 주소 */
    FileUrl_ListItem: BoardCA.FileUrl.ListItem,
    /** 게시물 보기에 사용될 html 주소 */
    FileUrl_PostView: BoardCA.FileUrl.PostView,
    /** 게시물 보기에 있는 댓글에 사용될 html 주소 */
    FileUrl_PostReply_ListItem: BoardCA.FileUrl.PostReply_ListItem,

    /** 게시물 작성에 사용될 html 주소 */
    FileUrl_PostCreate: BoardCA.FileUrl.PostCreate,
    /** 게시물 수정에 사용될 html 주소 */
    FileUrl_PostEdit: BoardCA.FileUrl.PostEdit,
    /** 게시물 작성시 비디오 넣기 기능에 html 주소 */
    FileUrl_VideoInsert: BoardCA.FileUrl.VideoInsert,

    /** 포스트보기에 게시물 리스트 보일지 여부 */
    PostView_List: false,
    /** 포스트보기에 댓글 리스트를 자동으로 열지 여부 */
    PostView_ReplyOepn: true,
    /** 포스트보기에 대댓글 리스트를 자동으로 열지 여부 */
    PostView_ReReplyOepn: true,

    /** 
     * 게시판 모드
     * 'BoardCA.ModeType' 사용
     * */
    Mode: BoardCA.ModeType.Defult,

    /**
     * 관리자 모드 사용할지 여부.
     * 관리자 권한이 있어야 동작한다.
     * */
    AdminMode: false,
    /** 디버그 모드 여부 */
    Debug: false,

    /**
     * 첨부 파일 최대 갯수
     * -1 = 무한
     * */
    FileMax: -1,

};

/** 게시판 생성 옵션 저장 */
BoardCommon.prototype.BoardOption = null;

/** 사용할 게시판 번호 */
BoardCommon.prototype.BoardID = 0;
/** 이 게시판이 사용되는 페이지의 해쉬 이후 URL  */
BoardCommon.prototype.UrlHash = "";
/** 타이틀로 사용할 html */
BoardCommon.prototype.MessageTitle = "게시판";
/** 게시판에서만 사용할 데이터 바인드 */
BoardCommon.prototype.DataBind = new DG_JsDataBind();

/** html로드가 끝났는지 여부 */
BoardCommon.prototype.HtmlLoadComplete = true;
/** 바디로 사용할 html */
BoardCommon.prototype.BoardCommon_BodyHtml = "";
/** 타이틀로 사용할 html */
BoardCommon.prototype.BoardCommon_TitleHtml = "";
/** 리스트 아이템 html */
BoardCommon.prototype.BoardCommon_ListItemHtml = "";
/** 포스트 보기에 사용할 html */
BoardCommon.prototype.BoardCommon_PostViewHtml = "";
/** 포스트 리플에 사용할 html */
BoardCommon.prototype.BoardCommon_PostReply_ListItemHtml = "";

/** 포스트 작성에 사용할 html */
BoardCommon.prototype.BoardCommon_PostCreateHtml = "";
/** 포스트 수정 사용할 html */
BoardCommon.prototype.BoardCommon_PostEditHtml = "";
/** 비디오 입력에 사용할 html */
BoardCommon.prototype.BoardCommon_VideoInsertHtml = "";


/** 테이블 타겟 오브젝트 */
BoardCommon.prototype.TableArea = null;

/** 로딩 표시 영역 */
BoardCommon.prototype.BoardComm_Loading = null;
/** 포스트 표시 영역 */
BoardCommon.prototype.BoardComm_PostView = null;
/** 댓글 영역 */
BoardCommon.prototype.BoardComm_Reply = null;
BoardCommon.prototype.BoardComm_Reply_List = null;
BoardCommon.prototype.BoardComm_Reply_Create = null;
/** 대댓글 영역 */
BoardCommon.prototype.BoardComm_ReReplyCreateArea = null;
/** 대댓글 작성 UI를 감싼 영역 */
BoardCommon.prototype.BoardComm_ReReplyCreateWrap = null;

BoardCommon.prototype.BoardComm_List = null;
BoardCommon.prototype.BoardComm_Head = null;
BoardCommon.prototype.BoardComm_Title = null;
BoardCommon.prototype.BoardComm_ListItem = null;
BoardCommon.prototype.BoardComm_Foot = null;

/** 도구 영역 */
BoardCommon.prototype.BoardComm_Tools = null;
/** 글수 영역 */
BoardCommon.prototype.spanBoardComm_Tools_PostCount = null;





/** 에디터 개체 */
BoardCommon.prototype.Editor = null;
/** 파일 첨부 라이브러리 */
BoardCommon.prototype.dgFS = null;
/** 허용 확장자 - 이미지 확장자 */
BoardCommon.prototype.ImageType = [".bmp", ".dib", ".jpg", ".jpeg", ".jpe", ".gif", ".png", ".tif", ".tiff", ".raw"];
/** 에디터 글자수 개체 */
BoardCommon.prototype.labLength = null;
/** 뷰어 개체 */
BoardCommon.prototype.Viewer = null;

/** 이미지에서 데이터 부분을 제거하기 위한 정규식 */
BoardCommon.prototype.RegExr_ImageDataCut = /(?=\(data:image).*?(?=\))/g;


/** 첫 바인딩을 시도하였는지 여부 */
BoardCommon.prototype.bFirstBind = false;

/** 첫 바인딩이 끝나고 콜백 함수 */
BoardCommon.prototype.FirstBind_Callback = null;


/** 마지막으로 받은 페이지 개수 */
BoardCommon.prototype.PageTotal = 0;

/** 지금 보고 있는 페이지 번호 */
BoardCommon.prototype.PageNumber = 0;

/** 
 *  지금 보고 있는 게시물의 번호
 *  백업용으로 리스트에서도 값이 있을 수 있다.
 * */
BoardCommon.prototype.PostViewID = -1;


/** 포스트 작성이나 수정중에 본문에 포함된 이미지의 크기 */
BoardCommon.prototype.PostImageSize = 0;

/** 아이템 바인딩이 진행중인지 여부 */
BoardCommon.prototype.BindItemLoading = false;

/** 스크롤 이벤트를 연속 판단을 막으려고 사용하는 타이머의 아이디 */
BoardCommon.prototype.ScrollTimerId = -1;

/** 보기시 댓글 리스트 표시여부를 서버에서 받은 정보 */
BoardCommon.prototype.ReplyListShow = false;


/** 
 *  리플 리스트를 처음 한번 열렸는지 여부 
 *  두번째는 리플리스트를 강제 갱신외에는 갱신하지 않는다.
 */
BoardCommon.prototype.ReplyListFirstOpen = false;

/**
 * 지정한 대상에 초기화 해줍니다.
 * @param {boolean} bItem 아이템 바인징 여부
 */
BoardCommon.prototype.Initialize = function (
    bItem)
{
    var objThis = this;

    //필요한 데이터가 모두 로드 되었는지 확인
    if ((true === objThis.bFirstBind)
        || ("" === objThis.BoardCommon_BodyHtml)
        || ("" === objThis.BoardCommon_TitleHtml)
        || ("" === objThis.BoardCommon_ListItemHtml)
        || ("" === objThis.BoardCommon_PostViewHtml))
    {
        return;
    }

    //첫 바인딩이 완료되었음을 알림
    objThis.bFirstBind = true;

    //바인딩 규칙을 지정한다.
    objThis.DataBind.MatchPatternListAdd(
        "Board"
        , {//유저 얼굴이미지
            ":DateOrTime": function (sOriData, sMatchString, sValue, jsonValue)
            {
                //같은 날이면 시간만
                //다른 날이면 날짜만 표시
                var sReturn = "";
                if (true === moment().isBefore(sValue, "day"))
                {//같은날
                    sReturn = moment(sValue).format("hh:mm");
                }
                else
                {//다른날
                    sReturn = moment(sValue).format("YYYY-MM-DD");
                }
                return GlobalStatic.DataBind.ReplaceAll(sOriData, sMatchString, sReturn);
            }
            , ":ReplyCount": function (sOriData, sMatchString, sValue, jsonValue)
            {
                var sReturn = "";
                if (0 < dgIsObject.IsIntValue(sValue))
                {
                    sReturn = "[" + sValue + "]";
                }

                return GlobalStatic.DataBind.ReplaceAll(sOriData
                    , sMatchString
                    , sReturn);
            }
            , ":DateTime": function (sOriData, sMatchString, sValue, jsonValue)
            {
                return GlobalStatic.DataBind.ReplaceAll(sOriData, sMatchString, moment(sValue).format("YYYY-MM-DD hh:mm"));
            }
            , ":UserNameView": function (sOriData, sMatchString, sValue, jsonValue)
            {
                var sReturn = "";

                if (0 < jsonValue.idUser)
                {//회원이 썼다.
                    sReturn = jsonValue.UserName;
                }
                else
                {//비회원이 작성했다.
                    //비회원 정보를 쓴다.
                    sReturn = jsonValue.NonMember_ViewName;
                }
                return GlobalStatic.DataBind.ReplaceAll(sOriData, sMatchString, sReturn);
            }
        });


    //설정 리셋
    objThis.Reset();

    //보드 동작 요청
    BoardCA.Action(bItem);
    //objThis.BindTitle(nBoardID, bItem);
};

/**
 * 영역을 다시 세팅한다.
 */
BoardCommon.prototype.Reset = function ()
{
    var objThis = this;
    //기존 내용지우기
    objThis.TableArea.empty();

    //무한 스크롤 모드 체크 ************************************
    //기존에 연결된 스크롤 이벤트 제거
    if (objThis.BoardOption.ParentArea)
    {
        objThis.BoardOption.ParentArea.unbind("scroll.BoardInfiniteScroll");
    }

    if (objThis.BoardOption.ParentArea2)
    {
        objThis.BoardOption.ParentArea2.unbind("scroll.BoardInfiniteScroll");
    }
    
    
    if (BoardCA.ModeType.InfiniteScroll === objThis.BoardOption.Mode)
    {//무한 스크롤 모드다.
        //부모 영역에 스크롤 이벤트를 연결한다.

        //스크롤 이벤트 연결
        objThis.BoardOption.ParentArea.bind("scroll.BoardInfiniteScroll"
            , function ()
            {
                objThis.Parent_OnScroll(
                    objThis
                    , objThis.BoardOption.ParentArea
                    , objThis.TableArea);
            });

        var sType = typeof objThis.BoardOption.ParentArea2;
        if ("null" !== sType
            && "undefined" !== sType)
        {
            objThis.BoardOption.ParentArea2 = $("#divPage");
            objThis.BoardOption.ParentArea2.unbind("scroll.BoardInfiniteScroll");
            //스크롤 이벤트 연결
            objThis.BoardOption.ParentArea2.bind("scroll.BoardInfiniteScroll"
                , function ()
                {
                    objThis.Parent_OnScroll(
                        objThis
                        , objThis.BoardOption.ParentArea2
                        , objThis.TableArea);
                });
        }
    }

    //게시판 css 추가
    objThis.TableArea.addClass("BoardComm");

    //게시판 바디  만들기
    objThis.TableArea.html(
        objThis.DataBind.DataBind_All(
            objThis.BoardCommon_BodyHtml
            , { UrlListView: objThis.UrlHash })
            .ResultString);

    //자주쓰는 영역 찾기
    objThis.BoardComm_Loading = objThis.TableArea.find(".BoardComm_Loading");

    objThis.BoardComm_PostView = objThis.TableArea.find(".BoardComm_PostView");

    objThis.BoardComm_List = objThis.TableArea.find(".BoardComm_List");
    objThis.BoardComm_Head = objThis.TableArea.find(".BoardComm_Head");
    objThis.BoardComm_Title = objThis.TableArea.find(".BoardComm_Title");
    objThis.BoardComm_ListItem = objThis.TableArea.find(".BoardComm_ListItem");
    objThis.BoardComm_Foot = objThis.TableArea.find(".BoardComm_Foot");

    objThis.BoardComm_Tools = objThis.TableArea.find(".BoardComm_Tools");
    objThis.spanBoardComm_Tools_PostCount = objThis.TableArea.find(".spanBoardComm_Tools_PostCount");

};

/**
 * 스크롤 영역에 넣을 이벤트
 * @param {any} objThis BoardCommon 개체
 * @param {any} domParentArea 스크롤 영역으로 사용할 부모 영역
 * @param {any} domList 아이템 리스트가 바인딩되는 영역
 */
BoardCommon.prototype.Parent_OnScroll = function (objThis, domParentArea, domList)
{
    var objThisTemp = objThis;

    //스크롤 탑 구하기
    var nScrolltop = parseInt(domParentArea.scrollTop());
    //리스트 크기 구하기
    var nListHeight = domList.height() - domParentArea.height();

    //스크롤 계산
    if (nScrolltop >= nListHeight)
    {//스크롤이 맨아래다.
        if (0 > objThisTemp.ScrollTimerId)
        {//타이머 아이디가 없다.

            //다음 페이지로 이동 요청
            BoardCA.PageMoveNext();

            //다음 1초까지 요청을 보내지 않도록 타이머 아이디를 유지한다.
            objThisTemp.ScrollTimerId
                = setTimeout(function () { objThisTemp.ScrollTimerId = -1; }
                    , 1000);
        }
    }
};

/**
 * 게시판 로딩 표시
 * @param {any} bShow
 */
BoardCommon.prototype.Loading = function (bShow)
{
    var objThis = this;

    if (true === bShow)
    {//로딩 표시
        objThis.BoardComm_Loading.removeClass("d-none");
    }
    else
    {//로딩 제거
        objThis.BoardComm_Loading.addClass("d-none");
    }
};


/**
 * 포스트 뷰 표시/지우기
 * @param {bool} bShow 포스트 뷰 표시 여부
 */
BoardCommon.prototype.PostViewDisplay = function (
    bShow
    , bReply)
{
    var objThis = this;

    if (true === bShow)
    {
        //화면에 표시
        objThis.BoardComm_PostView.removeClass("d-none");
    }
    else
    {
        //내용물 지우기
        objThis.BoardComm_PostView.empty();

        //화면에 표시 안함
        objThis.BoardComm_PostView.addClass("d-none");
    }

    if ((undefined !== objThis.BoardComm_Reply)
        && (null !== objThis.BoardComm_Reply))
    {//대상이 있다.
        if (true === bReply)
        {
            objThis.BoardComm_Reply.removeClass("d-none");
        }
        else
        {
            objThis.BoardComm_Reply_List.empty();

            objThis.BoardComm_Reply.addClass("d-none");
        }
    }
    
};

/**
 * 리스트 표시/지우기
 * @param {any} bShow
 */
BoardCommon.prototype.ListDisplay = function (bShow)
{
    var objThis = this;

    if (true === bShow)
    {
        //화면에 표시
        objThis.BoardComm_List.removeClass("d-none");
    }
    else
    {
        //내용물 지우기
        objThis.BoardComm_Title.empty();
        objThis.BoardComm_Head.empty();
        objThis.BoardComm_ListItem.empty();
        objThis.BoardComm_Foot.empty();

        //화면에 표시 안함
        objThis.BoardComm_List.addClass("d-none");
    }
};


/** 가지고 있는 페이지에서 다음 페이지로 이동한다. */
BoardCommon.prototype.PageMoveNext = function ()
{
    var objThis = this;

    if (false === objThis.BindItemLoading)
    {//로딩이 진행중이 아니다.

        //다음 페이지 번호 받기
        var nPageNum = objThis.PageNumber + 1;

        objThis.BindItem(nPageNum, null);
    }
};



//◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇
// 리스트 표시
//◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇

/**
 * 리스트를 다시 그려준다.
 * @param {bool} bItem 아이템도 다시 그릴지 여부
 * @param {int} nPageNum 선택된 페이지 번호
 * @param {function} callback 바인딩이 끝나면 동작시킬 콜백
 */
BoardCommon.prototype.BindTitle = function (
    bItem
    , nPageNum
    , callback)
{
    var objThis = this;

    if (false === objThis.bFirstBind)
    {//첫 바인딩이 되지 않았다.
        //바인딩을 할수 없는 상태라는 것
        return;
    }

    //쿼리가 있는지 확인
    var jsonQuery = getParamsSPA();
    //포스트뷰 대상이 있는지 확인
    var pvid = dgIsObject.IsIntValue(jsonQuery["pvid"]);
    //아이템 바인딩 여부
    var bItemTemp = bItem;

    var jsonTitle = {
        idBoardPost: "#"
        , Title: "제목"
        , UserName: "작성자"
        , ViewCount: "조회"
        , WriteDate: "작성일"
    };

    //게시판 로딩 표시
    objThis.Loading(true);

    //포스트 지우기 여부
    if (0 < pvid)
    {//포스트 보기다.
        objThis.PostViewDisplay(true, objThis.ReplyListShow);
    }
    else
    {//포스트 뷰가 아니다.
        //포스트 뷰 가리기
        objThis.PostViewDisplay(false, false);
    }
    
    //리스트 표시
    objThis.ListDisplay(true);

    //기존 타이틀 내용 지우기
    objThis.BoardComm_Head.empty();


    //리스트 html을 임시 저장
    var sHtmlTemp = "";
    
    //타이틀 그리기**********************
    sHtmlTemp
        = objThis.DataBind.DataBind_All(
            objThis.BoardCommon_TitleHtml
            , jsonTitle)
            .ResultString;
    //대상에 추가
    objThis.BoardComm_Head.html(sHtmlTemp);

    if (true === bItemTemp)
    {
        objThis.BindItem(nPageNum, callback);
    }
    else
    {//더이상 바인딩이 없다.
        if (typeof callback === "function")
        {
            callback();
        }

        //게시판 로딩 표시
        objThis.Loading(false);
    }
};



/**
 * 아이템 리스트를 받고 리스트 갱신을 요청한다.
 * @param {int} nPageNum 선택된 페이지 번호
 * @param {function} callback 바인딩이 끝나면 동작시킬 콜백
 */
BoardCommon.prototype.BindItem = function (
    nPageNum
    , callback)
{
    var objThis = this;
    var nPageNumTemp = 0 <= nPageNum ? nPageNum : 1;
    var callbackTemp = callback;

    //아이템이 바인딩 중임을 알린다.
    objThis.BindItemLoading = true;

    //게시판 로딩 표시
    objThis.Loading(true);

    AA.get(AA.TokenRelayType.CaseByCase
        , {
            url: FS_Api.Board_List
            , url_Auth: FS_Api.Board_List_Auth
            , data: {
                idBoard: objThis.BoardID,
                nPageNumber: nPageNumTemp,
                bAdminMode: objThis.BoardOption.AdminMode
            }
            , success: function (jsonResult)
            {
                if ("0" === jsonResult.InfoCode)
                {//에러 없음

                    //지금 페이지 번호 백업
                    objThis.PageNumber = jsonResult.PageNumber;

                    //아이템 리스트
                    objThis.BindItem_Items(jsonResult.List);
                    //페이징 그리기
                    objThis.PagingBind(jsonResult);

                    //콜백이 있으면 호출
                    if (typeof callbackTemp === "function")
                    {
                        callbackTemp();
                    }
                }
                else
                {//에러 있음
                    GlobalStatic.MessageBox_Error(
                        objThis.MessageTitle
                        , "아래 이유로 볼 수 없습니다. <br />" + jsonResult.Message);

                    //게시판 로딩 표시
                    objThis.Loading(false);
                }
            }
            , error: function (error)
            {
                console.log(error);
                //로딩이 끝났음을 알린다.
                objThis.BindItemLoading = false;
                //게시판 로딩 표시
                objThis.Loading(false);
            }
        });
};

/**
 * 받은 아이텔 리스트를 화면에 그린다.
 * @param {josn} arrJsonData
 */
BoardCommon.prototype.BindItem_Items = function (arrJsonData)
{
    var objThis = this;

    //url에서 해쉬부분만 자른다.
    //url 쿼리는 제거
    var sPageHash = location.hash.split("?")[0];

    //쿼리가 있는지 확인
    var jsonQuery = getParamsSPA();
    //포스트뷰 대상이 있는지 확인
    var pvid = dgIsObject.IsIntValue(jsonQuery[BoardCA.UrlQ.PostViewId]);
    //페이지 번호 확인
    var pn = dgIsObject.IsIntValue(jsonQuery[BoardCA.UrlQ.PageNumber]);

    if (BoardCA.ModeType.InfiniteScroll === objThis.BoardOption.Mode)
    {//리스트 끝에 추가
        //기존 아이템을 그냥 둔다.
    }
    else
    {
        //기존 아이템 지우기
        objThis.BoardComm_ListItem.empty();
    }
    


    //리스트 html을 임시 저장
    var sHtmlTemp = "";

    //컨탠츠 추가***************
    for (var i = 0; i < arrJsonData.length; ++i)
    {
        var jsonItemData = arrJsonData[i];

        //포스트 뷰 url 만들기
        jsonItemData.PostViewUrl
            = sPageHash + "?"
            + BoardCA.UrlQ.PostViewId + "=" + jsonItemData.idBoardPost + "&";

        if (0 < pn)
        {//페이지 번호가 있다.
            //있을때만 페이지 번호를 넣는다.
            jsonItemData.PostViewUrl
                += BoardCA.UrlQ.PageNumber + "=" + pn + "&";
        }
                

        //지금 보고 있는 글인지 확인
        if (pvid === dgIsObject.IsIntValue(jsonItemData.idBoardPost))
        {//보고있는 아이템이다.
            jsonItemData.ItemNow = "ItemNow table-primary";
        }
        else
        {
            jsonItemData.ItemNow = "";
        }

        //공지 여부 확인
        switch (jsonItemData.ItemType)
        {
            case 1:
                jsonItemData.ItemTypeCss = "ItemNoticeAll";
                break;
            case 2:
                jsonItemData.ItemTypeCss = "ItemNoticeGroup";
                break;
            case 3:
                jsonItemData.ItemTypeCss = "ItemNoticeBoard";
                break;

            case 0:
            default:
                jsonItemData.ItemTypeCss = "";
                break;
        }

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

        //조회수 계산
        jsonItemData.ViewCount = jsonItemData.ViewCount + jsonItemData.ViewCountNone;

        //포워딩 받은 글은 글쓴이에 포워딩 유저를 표시한다.
        jsonItemData.ViewUserName = jsonItemData.UserName;
        jsonItemData.ViewUserId = jsonItemData.idUser;
        
        sHtmlTemp
            += objThis.DataBind.DataBind_All(
                objThis.BoardCommon_ListItemHtml
                , jsonItemData)
                .ResultString;


        //게시판 로딩 제거
        objThis.Loading(false);
    }

    //대상에 추가
    //objThis.BoardComm_ListItem.html(sHtmlTemp);
    objThis.BoardComm_ListItem.append($(sHtmlTemp));
};

/**
 * 페이징 처리
 * @param {any} jsonData
 */
BoardCommon.prototype.PagingBind = function (jsonData)
{
    var objThis = this;
    var domBtn = null;
    var nTemp = 0;

    if (BoardCA.ModeType.InfiniteScroll === objThis.BoardOption.Mode)
    {//무한 스크롤 모드다.
        //무한 스크롤에서 페이징은 표시하지 않는다.
        return;
    }

    //해쉬이후 주소 받기 *****
    //페이지네이션에 사용할 url만들기
    var sUrlPart = objThis.UrlHash + "?";

    //쿼리가 있는지 확인 *****
    var jsonQuery = getParamsSPA();
    //카테고리 정보 받기 ***
    var nCat = dgIsObject.IsIntValue(jsonQuery[BoardCA.UrlQ.Category]);

    //페이지 번호 받기 ***
    var nPN = dgIsObject.IsIntValue(jsonQuery[BoardCA.UrlQ.PageNumber]);
    if (0 >= nPN)
    {
        nPN = 1;
    }


    //카테고리 정보 전달하기
    if (0 < nCat)
    {//카테고리 정보가 있다.
        sUrlPart += BoardCA.UrlQ.Category + "=" + nCat + "&";
    }



    //글수 표시
    objThis.spanBoardComm_Tools_PostCount.html(jsonData.TotalCount);

    //페이지 개수 계산
    objThis.PageTotal = parseInt(jsonData.TotalCount / jsonData.ShowCount) + 1;

    //기본 dom만들기
    var domPage = $('<nav></nav>');
    var domUl = $('<ul class="pagination justify-content-center"></ul>');
    domPage.append(domUl);


    //맨 앞으로 ***********
    domBtn = objThis.PaginationButton("&lt;&lt;", 1, sUrlPart);
    domBtn.addClass("BoardComm_PageFirst");
    
    if (1 >= objThis.PageNumber)
    {//맨 앞페이지다.
        //더이상 갈때가 없다.
        domBtn.addClass("disabled");
    }
    domUl.append(domBtn);


    //앞으로 *************
    nTemp = nPN - 1;
    if (0 >= nTemp)
    {//0보다 작다.
        //1로 처리
        nTemp = 1;
    }

    domBtn = objThis.PaginationButton("&lt;", nTemp, sUrlPart);
    domBtn.addClass("BoardComm_PagePrevious");

    if (1 >= objThis.PageNumber)
    {//맨 앞페이지다.
        //더이상 갈때가 없다.
        domBtn.addClass("disabled");
    }
    domUl.append(domBtn);



    //지금 페이지 앞번호********************
    for (var i = nPN - 4; i < nPN; ++i)
    {
        domBtn = objThis.PaginationButton(i.toString(), i, sUrlPart);
        domBtn.addClass("BoardComm_PageBefore");

        if (0 >= i)
        {//0보다 이하
            domBtn.css("visibility", "hidden");
        }
        domUl.append(domBtn);
    }

    //지금 페이지************************
    domBtn = objThis.PaginationButton(nPN.toString(), nPN, sUrlPart);
    domBtn.addClass("disabled");
    domBtn.addClass("active");
    domBtn.addClass("BoardComm_PageNow");
    domUl.append(domBtn);


    //지금 페이지 뒷번호 ************************
    for (var j = nPN + 1; j < nPN + 4 + 1; ++j)
    {
        domBtn = objThis.PaginationButton(j.toString(), j, sUrlPart);
        domBtn.addClass("BoardComm_PageAfter");

        if ( objThis.PageTotal < j)
        {//0보다 이하
            domBtn.css("visibility", "hidden");
        }
        domUl.append(domBtn);
    }
    


    //뒤로 ***************
    nTemp = nPN + 1;
    if (objThis.PageTotal <= nTemp)
    {//최대 페이지가 넘었다.
        //최대 페이지로 설정
        nTemp = objThis.PageTotal;
    }

    domBtn = objThis.PaginationButton("&gt;", nTemp, sUrlPart);
    domBtn.addClass("BoardComm_PageNext");

    if (objThis.PageTotal <= nPN)
    {//맨 뒷페이지다.
        //더이상 갈때가 없다.
        domBtn.addClass("disabled");
    }
    domUl.append(domBtn);


    //맨 뒤로 *******************
    domBtn = objThis.PaginationButton("&gt;&gt;", objThis.PageTotal, sUrlPart);
    domBtn.addClass("BoardComm_PageLast");

    if (objThis.PageTotal <= nPN)
    {//맨 뒷페이지다.
        //더이상 갈때가 없다.
        domBtn.addClass("disabled");
    }

    domUl.append(domBtn);


    objThis.BoardComm_Foot.html(domPage);
};

/**
 * 페이지네이션에 사용할 버튼을 완성하여 리턴한다.
 * @param {string} sPageNumber 사용할 페이지 텍스트
 * @param {int} nPageNumber 사용할 페이지 번호
 * @param {string} sUrlPart url 앞쪽
 * @returns {dom} 완성된 제이쿼리 li 완성
 */
BoardCommon.prototype.PaginationButton = function (
    sPageNumber
    , nPageNumber
    , sUrlPart)
{
    //감싸는 li
    var domLi = null;
    domLi = $('<li class="page-item"></li>');
    domLi.addClass("BoardComm_Paginate");

    //링크 a
    var domA = $('<a class="page-link">' + sPageNumber + '</a>');
    domA.attr("href", sUrlPart + BoardCA.UrlQ.PageNumber + "=" + nPageNumber + "&")
    domLi.append(domA);

    return domLi;
};


//◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇
// 포스팅 보기
//◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇

/**
 * 대상 게시물 보기
 * @param {any} nPostView 포스트 고유 번호
 */
BoardCommon.prototype.PostView = function (nPostView)
{
    var objThis = this;

    if (false === objThis.bFirstBind)
    {//첫 바인딩이 되지 않았다.
        //바인딩을 할수 없는 상태라는 것
        return;
    }

    var idBoardTemp = objThis.BoardID;
    var nPostViewTemp = nPostView;

    var bReturn = true;
    var sMsg = "";

    //유효성 검사
    if (0 >= idBoardTemp)
    {
        bReturn = false;
        sMsg = "게시판 정보가 없습니다."
    }
    else if (0 >= nPostViewTemp)
    {
        bReturn = false;
        sMsg = "게시물 정보가 없습니다."
    }


    if (true === bReturn)
    {//성공

        AA.get(AA.TokenRelayType.CaseByCase
            , {
                url: FS_Api.Board_PostView
                , url_Auth: FS_Api.Board_PostView_Auth
                , data: {
                    idBoard: idBoardTemp,
                    idBoardPost: nPostViewTemp
                }
                , success: function (jsonData)
                {
                    if ("0" === jsonData.InfoCode)
                    {//에러 없음
                        objThis.PostViewBind(jsonData);
                    }
                    else
                    {//에러 있음
                        GlobalStatic.MessageBox_Error(objThis.MessageTitle, jsonData.Message);
                    }
                }
                , error: function (error)
                {
                    console.log(error);
                }
            });

    }
    else
    {
        GlobalStatic.MessageBox_Error(objThis.MessageTitle, sMsg);
    }
    
};

/**
 * 대상 컨탠츠 보기
 * @param {any} jsonData
 */
BoardCommon.prototype.PostViewBind = function (jsonData)
{
    var objThis = this;

    //보고있는 게시물 번호 백업
    objThis.PostViewID = jsonData.idBoardPost;

    //조회수 계산
    jsonData.ViewCount = jsonData.ViewCount + jsonData.ViewCountNone;

    //카테고리 텍스트
    jsonData.PostCategoryText = "";


    //수정 권한 확인
    if (true === jsonData.EditAuth)
    {
        jsonData.EditAuthClass = "";
    }
    else
    {
        jsonData.EditAuthClass = "v-hidden";
    }
    //삭제 권한 확인
    if (true === jsonData.DeleteAuth)
    {
        jsonData.DeleteAuthClass = "";
    }
    else
    {
        jsonData.DeleteAuthClass = "v-hidden";
    }

    //목록 주소 만들기
    jsonData.UrlListView = objThis.UrlHash;


    //컨탠츠 그리기
    var sHtml
        = objThis.DataBind.DataBind_All(
            objThis.BoardCommon_PostViewHtml
            , jsonData)
            .ResultString;

    //뷰영역 표시
    objThis.BoardComm_PostView.html(sHtml);

    //댓글 영역 찾아놓기 *************************
    objThis.BoardComm_Reply = objThis.TableArea.find(".BoardComm_Reply");
    objThis.BoardComm_Reply_List = objThis.TableArea.find(".BoardComm_Reply_List");
    objThis.BoardComm_Reply_Create = objThis.TableArea.find(".BoardComm_Reply_Create");
    //대댓글 영역
    objThis.BoardComm_ReReplyCreateArea
        = objThis.TableArea.find(".BoardComm_ReReplyCreateArea");
    //대댓글 UI를 감싼 영역
    objThis.BoardComm_ReReplyCreateWrap
        = objThis.TableArea.find(".BoardComm_ReReplyCreateWrap.ReReplyCreateWrap_" + objThis.PostViewID);

    //창표시 ****************
    //서버에서 받은 리플 리스트 표시여부 백업
    objThis.ReplyListShow = jsonData.ReplyList;
    //포스트 표시
    objThis.PostViewDisplay(true, objThis.ReplyListShow);
    //리스트 표시 여부
    objThis.ListDisplay(objThis.BoardOption.PostView_List);
    if (true === objThis.BoardOption.PostView_List)
    {//리스트 표시
        BoardCA.ListShowBind();
    }

    
    //뷰어 생성후 컨탠츠 바인딩 *******************
    $("#divViewer").html(jsonData.Content);


    //게시판 로딩 제거
    objThis.Loading(false);


    //리플 표시.
    if ((true === jsonData.ReplyList)
        || (true === objThis.BoardOption.PostView_ReplyOepn))
    {
        objThis.PostReplyListView(true);
    }
    else
    {
        objThis.PostReplyListView(false);
    }

};


//◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇
// 댓글 리스트
//◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇

/** 댓글 리스트가 보기 상태인지 값 */
BoardCommon.prototype.PostReplyListShow = false;

/**
 * 댓글 보기/가리기를 자동으로 처리한다.
 */
BoardCommon.prototype.PostReplyListShowHide_Auto = function ()
{
    var objThis = this;

    objThis.PostReplyListView(!objThis.PostReplyListShow);
};

/**
 * 리플 리스트 표시/가리기.
 * 리스트 표시인 경우 바인딩된 내용이 없으면 바인딩을 요청한다.
 * @param {any} bView
 */
BoardCommon.prototype.PostReplyListView = function (bView)
{
    var objThis = this;
    var domOpenListButtonOpenText
        = objThis.BoardComm_Reply.find(".OpenListButtonOpenText");

    if (true === bView)
    {//표시
        objThis.PostReplyListShow = true;
        objThis.BoardComm_Reply_List.removeClass("d-none");
        objThis.BoardComm_Reply_Create.removeClass("d-none");
        domOpenListButtonOpenText.html("닫기");

        if (false === objThis.ReplyListFirstOpen)
        {//댓글 리스트가 첫바인딩 되지 않았다.
            //리플 리스트 새로 그린다.
            objThis.PostReplyList(objThis.PostViewID);
        }
    }
    else
    {//숨기기
        objThis.PostReplyListShow = false;
        objThis.BoardComm_Reply_List.addClass("d-none");
        objThis.BoardComm_Reply_Create.addClass("d-none");
        domOpenListButtonOpenText.html("열기");
    }
};

/**
 * 리플 리스트를 바인딩 한다.
 * @param {any} idBoardPost 볼 게시물 번호
 */
BoardCommon.prototype.PostReplyList = function (idBoardPost)
{
    var objThis = this;
    //로딩 표시
    objThis.BoardComm_Reply_List.html("댓글 리스트를 불러오고 있습니다.");

    //리스트 호출
    AA.get(AA.TokenRelayType.CaseByCase
        , {
            url: FS_Api.Board_PostReplyList
            , url_Auth: FS_Api.Board_PostReplyList_Auth
            , data: {
                idBoard: objThis.BoardID,
                idBoardPost: idBoardPost
            }
            , success: function (jsonData)
            {
                if ("0" === jsonData.InfoCode)
                {//에러 없음
                    //첫 바인딩 성공
                    objThis.ReplyListFirstOpen = true;

                    //리플 작성 권한
                    if (true === jsonData.WriteReply)
                    {
                        objThis.BoardComm_Reply_Create.removeClass("d-none");
                    }
                    else
                    {
                        objThis.BoardComm_Reply_Create.addClass("d-none")
                    }

                    //비회원 댓글 작성기능 활성화 여부 처리
                    objThis.PostReplyCreate_NonMember(jsonData.ReplyNonMember);

                    //리플 리스트
                    objThis.PostReplyListBind(
                        jsonData.ReReplyDiv
                        , jsonData.List
                        , jsonData.ReReplyList);
                }
                else
                {//에러 있음
                    GlobalStatic.MessageBox_Error(
                        objThis.MessageTitle
                        , "아래 이유로 볼 수 없습니다. <br />" + jsonData.Message);
                }
            }
            , error: function (error)
            {
                console.log(error);
            }
        });
};

/**
 * 리플 리스트 바인딩
 * @param {boolean} bReReplyDiv 대댓글 분리기능 사용여부
 * @param {any} jsonList "BoardPostViewReplyModel"참고. 댓글 리스트
 * @param {any} jsonReReplyList "BoardPostViewReplyModel"참고. 대댓글 리스트
 */
BoardCommon.prototype.PostReplyListBind = function (
    bReReplyDiv
    , jsonList
    , jsonReReplyList)
{
    if (null !== jsonList)
    {
        var objThis = this;
        var sOutHtml = "";



        //댓글 개수 표시
        $("#BoardComm_Reply_Count").html(jsonList.length);

        //기존 리스트 제거
        objThis.BoardComm_Reply_List.empty();

        //댓글 바인딩
        for (var i = 0; i < jsonList.length; ++i)
        {
            var item = jsonList[i];
            //아이템을 html로 변환한다.
            //변환한걸 html로 작성
            sOutHtml += objThis.PostReplyListItemHtml(bReReplyDiv, item);
        }

        //리플을 출력한다.
        objThis.BoardComm_Reply_List.html(sOutHtml);

        //대댓글 바인딩
        if ((null !== jsonReReplyList)
            && (0 < jsonReReplyList.length))
        {
            objThis.PostReReplyListFullBind(jsonReReplyList);
        }
    }//end if if (null !== jsonList)  
};


/**
 * 댓글 아이템 Html을 만든다.
 * @param {any} bReReplyDiv 대댓글 분리 기능 사용여부
 * @param {any} jsonItme
 */
BoardCommon.prototype.PostReplyListItemHtml = function (
    bReReplyDiv
    , jsonItme)
{
    var objThis = this;
    var sReturn = "";

    //댓글 유저 이미지 *****
    if ((undefined === jsonItme.UserFaceUrl)
        || ("" === jsonItme.UserFaceUrl))
    {//이미지 정보가 없다.
        //비어있는 값으로 세팅
        jsonItme.UserFaceUrl = "";
        //가려준다.
        jsonItme.UserFaceVisibility = "v-hidden";
    }

    //대댓글 분리 여부 ****
    if (true === bReReplyDiv)
    {//분리기능 사용
        jsonItme.ReReplyCss = "";
        jsonItme.ReReplyCreateOpenCss = "d-none";
    }
    else
    {//분리기능 사용하지 않음
        //분리기능을 사용하지 않을때는 대댓글의 숫자와 목록 펼치기 기능을 사용하지 않는다.
        jsonItme.ReReplyCss = "d-none";
        jsonItme.ReReplyCreateOpenCss = "";
    }

    sReturn
        = objThis.DataBind.DataBind_All(
            objThis.BoardCommon_PostReply_ListItemHtml
            , jsonItme)
            .ResultString;

    return sReturn;
};


//◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇
// 댓글 기능
//◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇

/**
 * 비회원 댓글 작성 UI 표시/숨기기
 * @param {any} bNonMember 비회원 작성 가능여부
 */
BoardCommon.prototype.PostReplyCreate_NonMember = function (bNonMember)
{
    var txtNonMember_ReName = $("#txtNonMember_ReName");
    var txtNonMember_RePw = $("#txtNonMember_RePw");

    var txtNonMember_ReReName = $("#txtNonMember_ReReName");
    var txtNonMember_ReRePw = $("#txtNonMember_ReRePw");



    if ((true === bNonMember)
        && (false === GlobalSign.SignIn))
    {//비회원 작성 허용 인데
        //로그인이 되있지 않다.
        //UI표시
        txtNonMember_ReName.removeClass("d-none");
        txtNonMember_RePw.removeClass("d-none");
        txtNonMember_ReReName.removeClass("d-none");
        txtNonMember_ReRePw.removeClass("d-none");
    }
    else
    {
        //UI제거
        txtNonMember_ReName.addClass("d-none");
        txtNonMember_RePw.addClass("d-none");
        txtNonMember_ReReName.addClass("d-none");
        txtNonMember_ReRePw.addClass("d-none");
    }
};

/** 댓글/대댓글 비우기 */
BoardCommon.prototype.PostReplyCreate_Empty = function ()
{
    var txtBoardComm_Reply_Create = $("#txtBoardComm_Reply_Create");
    var txtBoardComm_ReReply_Create = $("#txtBoardComm_ReReply_Create");

    txtBoardComm_Reply_Create.val("");
    txtBoardComm_ReReply_Create.val("");
};


/**
 * 리플 작성.
 * 대댓글 포함
 * @param {any} bReReply 대댓글인지 여부
 */
BoardCommon.prototype.PostReplyCreate = function (bReReply)
{
    var objThis = this;
    var bReturn = true;
    var sMessage = "";

    var nPostView = objThis.PostViewID;
    var idBoardReply_Target = objThis.idBoardPostReply_LastTarget;

    //대상값 확인
    idBoardReply_Target = dgIsObject.IsIntValue(idBoardReply_Target);


    var jsonData = {
        idBoard: objThis.BoardID,
        idBoardPost: nPostView,
        idBoardReply_Target: dgIsObject.IsIntValue(idBoardReply_Target),
        typeReplyState: 0,
        sTitle: "",
        sContent: "",

        sNonMember_ViewName: "",
        sNonMember_Password: "",
    };

    //내용 확인
    if (true === bReReply)
    {//대댓글이다.
        jsonData.sContent
            = dgIsObject.IsStringValue($("#txtBoardComm_ReReply_Create").val());

        jsonData.sNonMember_ViewName 
            = dgIsObject.IsStringValue($("#txtNonMember_ReReName").val());
        jsonData.sNonMember_Password
            = dgIsObject.IsStringValue($("#txtNonMember_ReRePw").val());
    }
    else
    {//대댓글이 아니다. == 일반 댓글
        jsonData.sContent = $("#txtBoardComm_Reply_Create").val();

        jsonData.sNonMember_ViewName
            = dgIsObject.IsStringValue($("#txtNonMember_ReName").val());
        jsonData.sNonMember_Password
            = dgIsObject.IsStringValue($("#txtNonMember_RePw").val());
    }

    if (0 >= nPostView)
    {
        bReturn = false;
        sMessage = "게시물을 선택하지 않았습니다."
    }
    else if ("" === jsonData.sContent)
    {
        bReturn = false;
        sMessage = "내용을 넣어주세요"
    }
    else if (false === GlobalSign.SignIn)
    {//비회원
        if (("" === jsonData.sNonMember_ViewName)
            || ("" === jsonData.sNonMember_Password))
        {
            bReturn = false;
            sMessage = "이름, 비밀번호를 넣어야 합니다."
        }
    }


    if (true === bReturn)
    {
        AA.post(AA.TokenRelayType.CaseByCase
            , {
                url: FS_Api.Board_PostReplyCreate
                , url_Auth: FS_Api.Board_PostReplyCreate_Auth
                , data: jsonData
                , success: function (jsonResult)
                {
                    if ("0" === jsonResult.InfoCode)
                    {//에러 없음
                        //입력창 초기화
                        objThis.PostReplyCreate_Empty();

                        if (0 < jsonResult.NewItem.idBoardPostReply_ReParent)
                        {//대댓글 갱신
                            objThis.PostReReplyList(
                                jsonResult.NewItem.idBoardPost
                                , jsonResult.NewItem.idBoardPostReply_ReParent
                                , true);
                        }
                        else
                        {//일반 댓글
                            //리스트 갱신
                            objThis.PostReplyList(jsonResult.NewItem.idBoardPost);
                        }

                    }
                    else
                    {//에러 있음
                        GlobalStatic.MessageBox_Error(objThis.MessageTitle, jsonResult.Message);
                    }
                }
                , error: function (error)
                {
                    console.log(error);
                }
            });
    }
    else
    {
        GlobalStatic.MessageBox_Error(objThis.MessageTitle, sMessage);
    }
};




//◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇
// 대댓글 기능
//◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇

/** 마지막으로 선택한 댓글 아이디 */
BoardCommon.prototype.idBoardPostReply_LastTarget = 0;

/**
 * 대댓글 작성창 표시/가리기 자동
 * @param {any} idBoardPost
 * @param {any} idBoardPostReply_Target
 */
BoardCommon.prototype.PostReReplyCreateShowHide_Toggle = function (
    idBoardPost
    , idBoardPostReply_Target)
{
    var objThis = this;

    //선택된 댓글 아이디 백업
    //objThis.idBoardPostReply_LastTarget = idBoardPostReply_Target;

    //대댓글 작성창 표시 대상 찾기
    var domTarget = $("#divReReplyCreateAreaTarget_" + idBoardPostReply_Target);

    //대상이 열려있는지 확인
    var bOpen = false;
    bOpen = dgIsObject.IsBoolValue(domTarget.attr("openCreateArea"));

    //요청(대상의 상태와 반대로 요청함)
    objThis.PostReReplyCreateShowHide(idBoardPost, idBoardPostReply_Target, !bOpen);
};

BoardCommon.prototype.PostReReplyCreateShowHide = function (
    idBoardPost
    , idBoardPostReply_Target
    , bShow)
{
    var objThis = this;

    //선택된 댓글 아이디 백업
    objThis.idBoardPostReply_LastTarget = idBoardPostReply_Target;

    //대댓글 작성창 표시 대상 찾기
    var domTarget = $("#divReReplyCreateAreaTarget_" + idBoardPostReply_Target);

    if (false === bShow)
    {//대댓글 창 가리기
        //대댓글 창은 가리는 것이 아니라 원래 있던 영역으로 옮기는 작업을 한다.
        domTarget.attr("openCreateArea", "false");

        //안보이는 원래 영역으로 이동
        objThis.BoardComm_ReReplyCreateArea.append(
            objThis.BoardComm_ReReplyCreateWrap);
    }
    else
    {//대상에 대댓글 창 표시

        //기존에 열려있는 대댓글작성창 찾기다고 표시
        var domOpenCreateArea = $(".BoardComm_Reply_List div[openCreateArea=true]");
        domOpenCreateArea.attr("openCreateArea", "false");

        //대댓글 작성창 보이기
        domTarget.removeClass("d-none");
        //대댓글 작성창 옮기기
        domTarget.append(objThis.BoardComm_ReReplyCreateWrap);
        //열렸다고 표시
        domTarget.attr("openCreateArea", "true");
    }

};



//◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇
// 대댓글 리스트
//◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇

/**
 * 대댓글 전체 한번에 바인딩
 * @param {any} arrReReply
 */
BoardCommon.prototype.PostReReplyListFullBind = function (
    arrReReply)
{
    var objThis = this;

    //댓글
    for (var i = 0; i < arrReReply.length; ++i)
    {
        var item = arrReReply[i];

        //대상(부모)을 찾는다
        var domReParent = $( "#divPostReplyItem_" + item.idBoardPostReply_ReParent + "_ReReply");
        //화면에 표시
        domReParent.removeClass("d-none");

        //아이템을 html로 변환한다.
        //변환한걸 html로 작성
        //대댓글은 대댓글 분리기능을 제외해놔야한다.
        domReParent.append(objThis.PostReplyListItemHtml(false, item));
    }
}

/**
 * 대댓글 리스트 바인딩
 * @param {any} idBoardPost
 * @param {any} idBoardReply_Target 리스트를 불러올 부모 댓글 아이디
 * @param {any} bCompulsion 강제로 갱신할지 여부
 */
BoardCommon.prototype.PostReReplyList = function (
    idBoardPost
    , idBoardReply_Target
    , bCompulsion)
{
    var objThis = this;

    var idBoardReply_Target_Temp = idBoardReply_Target;

    //댓글 열림 버튼 찾기
    var divOpenReReplyList
        = $("#divPostReplyItem_" + idBoardReply_Target_Temp + "_ReReply");

    //속성의 값 받기
    var bOpen = dgIsObject.IsBoolValue(divOpenReReplyList.attr("openReReplyList"));

    if (false === bOpen || true === dgIsObject.IsBoolValue(bCompulsion))
    {//리스트가 닫쳐 있다.
        //강제 열기 동작이다.

        //열림 동작
        divOpenReReplyList.attr("openReReplyList", "true");
        

        //리스트 호출
        AA.get(AA.TokenRelayType.CaseByCase
            , {
                url: FS_Api.Board_PostReReplyList
                , url_Auth: FS_Api.Board_PostReReplyList_Auth
                , data: {
                    idBoard: objThis.BoardID,
                    idBoardPost: idBoardPost,
                    idBoardReply: idBoardReply_Target_Temp
                }
                , success: function (jsonData)
                {
                    if ("0" === jsonData.InfoCode)
                    {//에러 없음

                        //리플 리스트
                        objThis.PostReReplyListBind(
                            idBoardReply_Target_Temp
                            , jsonData.List);
                    }
                    else
                    {//에러 있음
                        GlobalStatic.MessageBox_Error(
                            objThis.MessageTitle
                            , "아래 이유로 볼 수 없습니다. <br />" + jsonData.Message);
                    }
                }
                , error: function (error)
                {
                    console.log(error);
                }
            });
    }
    else
    {
        //닫침 동작
        btnOpen.attr("openReReplyList", "false");

        //대댓글 리스트
        objThis.PostReReplyListBind(idBoardReply_Target_Temp, []);
    }
};

/**
 * 지정한 대댓글의 리스트 바인드
 * @param {any} idBoardReply_Target
 * @param {any} arrReReply
 */
BoardCommon.prototype.PostReReplyListBind = function (
    idBoardReply_Target
    , arrReReply)
{

    var objThis = this;
    var sOutHtml = "";

    //대상 찾기
    var domTarget = $("#divPostReplyItem_" + idBoardReply_Target + "_ReReply");

    //대상을 비우고
    domTarget.empty();


    for (var i = 0; i < arrReReply.length; ++i)
    {
        var item = arrReReply[i];

        item.ReReplyCss = "d-none";

        //sOutHtml
        //    += objThis.DataBind.DataBind_All(
        //        objThis.BoardCommon_PostReply_ListItemHtml
        //        , item)
        //        .ResultString;

        //아이템을 html로 변환한다.
        //변환한걸 html로 작성
        //대댓글은 대댓글 분리기능을 제외해놔야한다.
        sOutHtml += objThis.PostReplyListItemHtml(false, item);

    }


    domTarget.html(sOutHtml);
};

//◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇
// 리플 작성/수정/삭제
//◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇

/**
 * 대댓글 작성창 취소
 */
BoardCommon.prototype.PostReReplyCreateCancel = function ()
{
    var objThis = this;

    //글쓰기 창 비우기
    $("#txtBoardComm_Reply_Create").val("");

    //대댓글 창 찾기
    var divReReplyCreateWrap = $("#divReReplyCreateWrap");
    //대댓글 창 옮기기
    divReReplyCreateWrap.append(objThis.divReReplyCreate);
};


//◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇
// 작성
//◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇

/**
 * 포스트 작성창 보기
 */
BoardCommon.prototype.PostCreateShow = function ()
{
    var objThis = this;
    var idBoardTemp = objThis.BoardID;

    var bReturn = true;
    var sMsg = "";

    if (0 >= idBoardTemp)
    {//선택된 에이전트가 없다.
        bReturn = false;
        sMsg = "선택된 게시판이 없습니다.";
    }
    else if (false === GlobalSign.SignIn)
    {//로그인 필요
        bReturn = false;
        sMsg = "로그인을 해야 사용할 수 있는 기능입니다.";
    }
    else
    {
        AA.get(AA.TokenRelayType.CaseByCase
            , {
                url: FS_Api.Board_PostCreateView
                , url_Auth: FS_Api.Board_PostCreateView_Auth
                , data: {
                    idBoard: idBoardTemp
                }
                , success: function (jsonData)
                {
                    if ("0" === jsonData.InfoCode)
                    {//에러 없음

                        
                        var bps = new BoardPostState();
                        //카테고리 처리 클래스
                        var bpc = new BoardPostCategory();
                        

                        //작성창 표시
                        var sHtml
                            = objThis.DataBind.DataBind_All(
                                objThis.BoardCommon_PostCreateHtml
                                , {
                                    idBoard: idBoardTemp,
                                    TitlePost: "",
                                    PostCategoryHtml: bpc.InputHtml(idBoardTemp, jsonData),
                                    BoardPostStateHtml: bps.InputHtml(idBoardTemp, jsonData),
                                    EditorLengthTitle: "글자수 : ",
                                    UrlListView: objThis.UrlHash
                                })
                                .ResultString;


                        //창표시****************
                        //포스트 표시
                        objThis.PostViewDisplay(true, false);
                        //리스트 지우기
                        objThis.ListDisplay(false);

                        //창표시
                        objThis.BoardComm_PostView.html(sHtml);

                        //파일 업로드 
                        objThis.FileSelectorNew([]);
                        

                        //글자수
                        objThis.labLength = $("#labLength");
                        $("#labLengthMax").html(GlobalStatic.BoardMaxLength);
                        //에디터 생성
                        objThis.CKEditorNew(objThis);
                    }
                    else
                    {//에러 있음
                        GlobalStatic.MessageBox_Error(objThis.MessageTitle, jsonData.Message);
                    }
                }
                , error: function (error)
                {
                    console.log(error);
                }
            });


        
        
    }//end if


    if (false === bReturn)
    {
        GlobalStatic.MessageBox_Error(objThis.MessageTitle, sMsg);
    }


};

/**
 * 파일 로드 완료
 * @param {any} objThis
 * @param {any} objThis_Toss
 */
BoardCommon.prototype.FS_LoadComplete = function (objThis, objThis_Toss)
{
    objThis.ImageInsert(objThis, objThis_Toss.LoadCompleteFile);
};

/**
 * 파일 삭제 완료
 * @param {any} objThis
 * @param {any} jsonFIle
 * @param {any} domDivItme
 */
BoardCommon.prototype.FS_DeleteComplete = function (objThis, jsonFIle, domDivItme)
{
    objThis.ImageDelete(objThis, jsonFIle, domDivItme);
};

/**
 * 에디터에 이미지를 추가해준다.
 * @param {any} objThis
 * @param {any} arrFIle
 */
BoardCommon.prototype.ImageInsert = function (objThis, arrFIle)
{
    for (var i = 0; i < arrFIle.length; ++i)
    {
        var itemFile = arrFIle[i];
        //확장자 검사
        var sExt = this.ImageType.find(element => element === itemFile.Extension);
        if (undefined !== sExt)
        {
            //에디터에 html 태그를 추가해준다.
            objThis.Editor.insertHtml(
                '<img idEditorDivision="' + itemFile.EditorDivision
                + '" alt="' + itemFile.EditorDivision
                + '" src="' + itemFile.Binary + '" />');
        }
    }
};

/**
 * 에디터에 이미지 제거
 * @param {any} objThis
 * @param {any} jsonFIle
 * @param {any} domDivItme
 */
BoardCommon.prototype.ImageDelete = function (objThis, jsonFIle, domDivItme)
{

    jQuery(objThis.Editor.editable().$)
        .find("img[ideditordivision='" + jsonFIle.EditorDivision + "']")
        .remove();
};


/**
 * 비디오 입력 창 표시
 * @param {any} objThis
 */
BoardCommon.prototype.InsertVideoShow = function (/*objThis*/)
{
    var objThis = this;

    //생성창 만들기
    var sHtml
        = objThis.DataBind.DataBind_All(
            objThis.BoardCommon_VideoInsertHtml
            , {
                Title: "비디오 넣기"
            })
            .ResultString;

    //창표시
    popTemp = DG_Popup.Show({
        Content: sHtml,
        ContentCss: "",

        top: 100,
        left: 100,
        StartViewWeight: true
    });
}

BoardCommon.prototype.YoutubeInsert = function (objEditor, sUrl)
{
    var objThis = this;

    var elYoutube
        = CKEDITOR.dom.element
            .createFromHtml('<iframe name="youtubeIframe" src="https://www.youtube.com/embed/'
                + sUrl + '" frameborder="0" allowfullscreen></iframe>');

    //에디터에 추가
    GlobalStatic.Page_Now.BoardComm.Editor.insertElement(elYoutube);
};

BoardCommon.prototype.VideoJsInsert = function (objEditor, sUrl)
{
    var objThis = this;

    var sVideoJsHtml = '<video class="video-js" controls autoplay preload="none" poster="http://vjs.zencdn.net/v/oceans.png" data-setup="{}">'
        + '<source src="' + sUrl + '">'
        //+ '<p class="vjs-no-js">To view this video please enable JavaScript, and consider upgrading to a web browser that <a href="https://videojs.com/html5-video-support/" target="_blank">supports HTML5 video</a></p>'
        + '</video>';

    //비디오js 추가
    //objEditor.insertHtml(sVideoJsHtml);
    var elYoutube
        = CKEDITOR.dom.element
            .createFromHtml(sVideoJsHtml);
    //에디터에 추가
    GlobalStatic.Page_Now.BoardComm.Editor.insertElement(elYoutube);

    //GlobalStatic.Page_Now.BoardComm.Editor.insertHtml('<video class="video-js" controls autoplay preload="none" poster="http://vjs.zencdn.net/v/oceans.png" data-setup="{}"><source src="><p class="vjs-no-js">To view this video please enable JavaScript, and consider upgrading to a web browser that <a href="https://videojs.com/html5-video-support/" target="_blank">supports HTML5 video</a></p></video>');
};



/** 포스트 작성 시도 */
BoardCommon.prototype.PostCreate = function ()
{
    var objThis = this;
    var bReturn = true;
    var sMessage = "";

    //게시판 아이디
    var idBoard = objThis.BoardID;

    //카테고리 처리 클래스
    var bps = new BoardPostState();
    var bpc = new BoardPostCategory();

    // 허용 호스트 리스트
    var iframeSrcArray = new Array("https://www.youtube.com/embed/", "http://twitch.tv/");

    var jsonData = {
        idBoard: idBoard,
        sTitle: dgIsObject.IsStringValue($("#txtBoard_PostCreate_Title").val()),
        typeBoardState: bps.GetHtmlValue(idBoard),
        nBoardCategory: bpc.GetHtmlValue(idBoard),
        sContent: objThis.Editor.document.getBody().getHtml(),
        listFileInfo: objThis.dgFS.ItemList_Get()
    };
    //이미지에서 데이터 정보 제거
    jsonData.sContent = jsonData.sContent.replace(objThis.RegExr_ImageDataCut, "(data:image");

    //썸네일 만들기
    if (0 < jsonData.listFileInfo.length)
    {//첨부파일이 있다.

        //첨부파일 확인
        var arrFIle = jsonData.listFileInfo;
        for (var i = 0; i < arrFIle.length; ++i)
        {
            var itemFile = arrFIle[i];
            //확장자 검사
            var sExt = this.ImageType.find(element => element === itemFile.Extension);
            if (undefined !== sExt)
            {//이미지 리스트에 확장자가 있다.
                itemFile.Thumbnail = true;
                break;
            }
        }
    }


    if (0 >= jsonData.idBoard)
    {
        bReturn = false;
        sMessage = "게시판을 선택하지 않았습니다."
    }
    else if ("" === jsonData.sTitle)
    {
        bReturn = false;
        sMessage = "제목을 넣어 주세요."
    }
    else if ("" === jsonData.sContent)
    {
        bReturn = false;
        sMessage = "내용을 넣어주세요"
    }
    else if (jsonData.sContent.length > GlobalStatic.BoardMaxLength)
    {
        bReturn = false;
        sMessage = "내용이 너무 깁니다.<br />"
            + jsonData.sContent.length + "/" + GlobalStatic.BoardMaxLength;
    }

    // iframe 있는지 확인
    if (jsonData.sContent.includes('youtubeIframe') === true) 
    {
        
        // iframe안의 src 추출
        var urlSplit = jsonData.sContent.split('"');
        var responseUrls="";
        for (var i2 = 0; i2 < urlSplit.length; i2++) {
            if (urlSplit[i2].includes('https:') === true || urlSplit[i2].includes('http:') === true) {
                responseUrls += urlSplit[i2]+",";
            }
        }

        // 동영상 첨부 여러건 등록 PATH 배열에 저장
        responseUrls = responseUrls.substr(0, responseUrls.length - 1);
        var responseUrl = responseUrls.split(",");


        // 비교(등록된 PathList != 허용리스트)
        var checkAllowCnt = 0;
        for (var i3 = 0; i3 < iframeSrcArray.length; i3++)
        {
            for (var j = 0; j < responseUrl.length; j++) {
                if (responseUrl[j].includes(iframeSrcArray[i]) === true) {
                    checkAllowCnt++;
                }
            }
        }

        if (checkAllowCnt !== responseUrl.length) {
            sMessage = "허용리스트에 없는 주소입니다.<br />";
            bReturn = false;
        }

    }

    if (true === bReturn)
    {
        AA.post(AA.TokenRelayType.CaseByCase
            , {
                url: FS_Api.Board_PostCreate
                , url_Auth: FS_Api.Board_PostCreate_Auth
                , data: jsonData
                , success: function (jsonResult)
                {
                    if ("0" === jsonResult.InfoCode)
                    {//에러 없음
                        BoardCA.PostViewLink(jsonResult.PostID);
                    }
                    else
                    {//에러 있음
                        GlobalStatic.MessageBox_Error(objThis.MessageTitle, jsonData.Message);
                    }
                }
                , error: function (error)
                {
                    console.log(error);
                }
            });
    }
    else
    {
        GlobalStatic.MessageBox_Error(objThis.MessageTitle, sMessage);
    }
};



//◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇
// 수정
//◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇

/**
 * 
 * @param {any} idBoardPost
 */
BoardCommon.prototype.PostEditShow = function (idBoardPost)
{
    var objThis = this;

    var idBoardTemp = objThis.BoardID;
    var idBoardPostTemp = idBoardPost;

    var bReturn = true;
    var sMsg = "";

    //유효성 검사
    if (0 >= idBoardTemp)
    {
        bReturn = false;
        sMsg = "게시판 정보가 없습니다."
    }
    else if (0 >= idBoardPostTemp)
    {
        bReturn = false;
        sMsg = "게시물 정보가 없습니다."
    }



    if (true === bReturn)
    {//성공

        AA.get(AA.TokenRelayType.CaseByCase
            , {
                url: FS_Api.Board_PostEditView
                , url_Auth: FS_Api.Board_PostEditView_Auth
                , data: {
                    idBoard: idBoardTemp,
                    idBoardPost: idBoardPostTemp
                }
                , success: function (jsonData)
                {
                    if ("0" === jsonData.InfoCode)
                    {//에러 없음
                        objThis.PostEditBind(jsonData);
                    }
                    else
                    {//에러 있음
                        GlobalStatic.MessageBox_Error(objThis.MessageTitle, jsonData.Message);
                    }
                }
                , error: function (error)
                {
                    console.log(error);
                }
            });

    }
    else
    {
        GlobalStatic.MessageBox_Error(objThis.MessageTitle, sMsg);
    }

};


/**
 * 대상 컨탠츠 수정창
 * @param {any} jsonData
 */
BoardCommon.prototype.PostEditBind = function (jsonData)
{
    var objThis = this;


    var idBoard = jsonData.idBoard;

    var bps = new BoardPostState();
    //카테고리 처리 클래스
    var bpc = new BoardPostCategory();


    jsonData.TitlePost = jsonData.Title;
    jsonData.EditorLengthTitle = "글자수";

    jsonData.PostCategoryHtml = bpc.InputHtml(idBoard, jsonData);
    jsonData.BoardPostStateHtml = bps.InputHtml(idBoard, jsonData);

    //목록에 이동할 url 지정
    jsonData.UrlListView = objThis.UrlHash;

    //컨탠츠 그리기
    var sHtml
        = objThis.DataBind.DataBind_All(
            objThis.BoardCommon_PostEditHtml
            , jsonData)
            .ResultString;

    //창표시****************
    //포스트 표시
    objThis.PostViewDisplay(true, false);
    //리스트 지우기
    objThis.ListDisplay(false);
    //컨탠츠 표시
    objThis.BoardComm_PostView.html(sHtml);

    //글자수
    objThis.labLength = $("#labLength");
    $("#labLengthMax").html(GlobalStatic.BoardMaxLength);

    //에디터 생성
    objThis.CKEditorNew(objThis, jsonData);

    //파일 업로드 
    objThis.FileSelectorNew(jsonData.FileInfoList);
};


/**
 * 포스트 수정 시도
 * @param {any} idBoardPost
 */
BoardCommon.prototype.PostEdit = function (idBoardPost)
{
    var objThis = this;
    var bReturn = true;
    var sMessage = "";

    //게시판 아이디
    var idBoardTemp = objThis.BoardID;

    // 허용 호스트 리스트
    var iframeSrcArray = new Array("https://www.youtube.com/embed/","http://twitch.tv/");

    //카테고리 처리 클래스
    var bps = new BoardPostState();
    var bpc = new BoardPostCategory();

    var jsonData = {
        idBoard: idBoardTemp,
        idBoardPost: idBoardPost,
        sTitle: dgIsObject.IsStringValue($("#txtBoard_PostCreate_Title").val()),
        typeBoardState: bps.GetHtmlValue(idBoardTemp),
        nBoardCategory: bpc.GetHtmlValue(idBoardTemp),
        sContent: objThis.Editor.document.getBody().getHtml(),
        listFileInfo: objThis.dgFS.ItemList_Get()
    };

    //이미지에서 데이터 정보 제거
    jsonData.sContent = jsonData.sContent.replace(objThis.RegExr_ImageDataCut, "(data:image");

    //썸네일 만들기
    if (0 < jsonData.listFileInfo.length)
    {//첨부파일이 있다.

        //첨부파일 확인
        var arrFIle = jsonData.listFileInfo;
        for (var i = 0; i < arrFIle.length; ++i)
        {
            var itemFile = arrFIle[i];
            //확장자 검사
            var sExt = this.ImageType.find(element => element === itemFile.Extension);
            if (undefined !== sExt)
            {//이미지 리스트에 확장자가 있다.
                itemFile.Thumbnail = true;
                break;
            }
        }
    }//end if jsonData.listFileInfo.length


    if (0 >= jsonData.idBoard) {
        bReturn = false;
        sMessage = "게시판을 선택하지 않았습니다."
    }
    else if ("" === jsonData.sTitle) {
        bReturn = false;
        sMessage = "제목을 넣어 주세요."
    }
    else if ("" === jsonData.sContent) {
        bReturn = false;
        sMessage = "내용을 넣어주세요"
    }
    else if (jsonData.sContent.length > GlobalStatic.BoardMaxLength) {
        bReturn = false;
        sMessage = "내용이 너무 깁니다.<br />"
            + jsonData.sContent.length + "/" + GlobalStatic.BoardMaxLength;
    } 

    // iframe 있는지 확인
    if (jsonData.sContent.includes('youtubeIframe') === true) {

        // iframe안의 src 추출
        var urlSplit = jsonData.sContent.split('"');
        var responseUrls = "";
        for (var i3 = 0; i3 < urlSplit.length; i3++) {
            if (urlSplit[i3].includes('https:') === true || urlSplit[i3].includes('http:') === true) {
                responseUrls += urlSplit[i3] + ",";
            }
        }

        // 동영상 첨부 여러건 등록 PATH 배열에 저장
        responseUrls = responseUrls.substr(0, responseUrls.length - 1);
        var responseUrl = responseUrls.split(",");


        // 비교(등록된 PathList != 허용리스트)
        var checkAllowCnt = 0;
        for (var i4 = 0; i4 < iframeSrcArray.length; i4++) {
            for (var j = 0; j < responseUrl.length; j++) {
                if (responseUrl[j].includes(iframeSrcArray[i4]) === true) {
                    checkAllowCnt++;
                }
            }
        }

        if (checkAllowCnt !== responseUrl.length) {
            sMessage = "허용리스트에 없는 주소입니다.<br />";
            bReturn = false;
        }

    }


    if (true === bReturn)
    {
        AA.patch(true
            , {
                url: FS_Api.Board_PostEdit
                , url_Auth: FS_Api.Board_PostEdit_Auth
                , data: jsonData
                , success: function (jsonResult)
                {
                    if ("0" === jsonResult.InfoCode)
                    {//에러 없음
                        BoardCA.PostView(jsonResult.PostID);
                    }
                    else
                    {//에러 있음
                        GlobalStatic.MessageBox_Error(objThis.MessageTitle, jsonData.Message);
                    }
                }
                , error: function (error)
                {
                    console.log(error);
                }
            });
    }
    else
    {
        GlobalStatic.MessageBox_Error(objThis.MessageTitle, sMessage);
    }
};



/**
 * 
 * @param {any} idBoardPost
 */
BoardCommon.prototype.PostDeleteView = function (idBoardPost)
{
    var objThis = this;

    var idBoardTemp = objThis.BoardID;
    var idBoardPostTemp = idBoardPost;

    var bReturn = true;
    var sMsg = "";


    //유효성 검사
    if (0 >= idBoardTemp)
    {
        bReturn = false;
        sMsg = "게시판 정보가 없습니다."
    }
    else if (0 >= idBoardPostTemp)
    {
        bReturn = false;
        sMsg = "게시물 정보가 없습니다."
    }


    if (true === bReturn)
    {//성공

        AA.get(AA.TokenRelayType.CaseByCase
            , {
                url: FS_Api.Board_PostDeleteView
                , url_Auth: FS_Api.Board_PostDeleteView_Auth
                , data: {
                    idBoard: idBoardTemp,
                    idBoardPost: idBoardPostTemp
                }
                , success: function (jsonData)
                {
                    if ("0" === jsonData.InfoCode)
                    {//에러 없음
                        objThis.PostDeleteBind(idBoardTemp, idBoardPostTemp);
                    }
                    else
                    {//에러 있음
                        GlobalStatic.MessageBox_Error(objThis.MessageTitle, jsonData.Message);
                    }
                }
                , error: function (error)
                {
                    console.log(error);
                }
            });

    }
    else
    {
        GlobalStatic.MessageBox_Error(objThis.MessageTitle, sMsg);
    }
};

BoardCommon.prototype.PostDeleteBind = function (idBoardPost)
{
    var objThis = this;

    var idBoardTemp = objThis.BoardID;
    var idBoardPostTemp = idBoardPost;

    DG_MessageBox.Show({
        Title: objThis.MessageTitle,
        Content: "이 게시물을 지우시겠습니까?",

        ButtonShowType: DG_MessageBox.ButtonShowType.YesNo,
        BigIconType: DG_MessageBox.BigIconType.Question,

        ButtonEvent: function (btnType)
        {
            if (DG_MessageBox.ButtonType.Yes === btnType)
            {//예
                AA.delete(AA.TokenRelayType.CaseByCase
                    , {
                        url: FS_Api.Board_PostDelete
                        , url_Auth: FS_Api.Board_PostDelete_Auth
                        , data: {
                            idBoard: idBoardTemp,
                            idBoardPost: idBoardPostTemp
                        }
                        , success: function (jsonData)
                        {
                            //기존 안내창 닫기
                            DG_Popup.Close();


                            if ("0" === jsonData.InfoCode)
                            {//에러 없음
                                DG_MessageBox.Show({
                                    Title: objThis.MessageTitle,
                                    Content: "삭제되었습니다.",

                                    ButtonShowType: DG_MessageBox.ButtonShowType.Ok,
                                    BigIconType: DG_MessageBox.BigIconType.Info,

                                    ButtonEvent: function (btnType)
                                    {
                                        DG_Popup.Close();
                                        //목록으로 이동
                                        BoardCA.ListLink();
                                    }
                                });
                            }
                            else
                            {//에러 있음
                                GlobalStatic.MessageBox_Error(objThis.MessageTitle, jsonData.Message);
                            }
                        }
                        , error: function (error)
                        {
                            console.log(error);
                        }
                    });
            }
            else
            {//아니요
                DG_Popup.Close();
            }
            
        }
    });
};


BoardCommon.prototype.CKEditorNew = function (objThis, jsonData)
{

    //사용할 옵션
    var jsonDataTemp = {
        Content: ""
    };

    //들어온 옵션 합치기
    jsonData = Object.assign(jsonDataTemp, jsonData);


    //CKEDITOR.editorConfig = function (config)
    //{
    //    //config.language = "ko";
    //    //config.removeButtons = 'Image';
    //    config.height = 500;
    //};

    if (objThis.Editor)
    {
        objThis.Editor.destroy();
    }

    if (typeof jsonData === "object")
    {
        //내용 출력
        $("#divEditor").text(jsonData.Content);
        //objThis.Editor.insertHtml(jsonData.Content);
    }
    
    CKEDITOR.replace("divEditor",
        {
            language: "ko",
            height: 500,

            removeButtons: 'Image',
            //https://ckeditor.com/docs/ckeditor4/latest/guide/dev_allowed_content_rules.html
            allowedContent: 'img(left,right)[!src,alt,width,height,ideditordivision];',
            //allowedContent: 'h1 h2 h3 p blockquote strong em;' +
            //    'a[!href];' +
            //    'img(left,right)[!src,alt,width,height];' +
            //    'table tr th td caption;' +
            //    'span{!font-family};' +
            //    'span{!color};' +
            //    'span(!marker);' +
            //    'del ins;' + 'object[data]'

            //embed_provider: '//ckeditor.iframe.ly/api/oembed?url={url}&callback={callback}',

            extraPlugins: 'totubevideo',
            extraAllowedContent:
                //비디오
                'video[src,poster,controls,autoplay,width, height,loop,class]{max-width,height}; source[!src]; p[class]'
                //유튜브용 아이프레임
                + 'div{ *}(*); iframe{*} [!src, !frameborder, !allowfullscreen, !allow]; object param[*]; a[*]; img[*];'
        });


    objThis.Editor = CKEDITOR.instances.divEditor;
    
    
};

/**
 * 파일 선택 인터페이스를 생성한다.
 * @param {any} arrFileList 생성이 끝나면 추가할 파일 리스트
 */
BoardCommon.prototype.FileSelectorNew = function (arrFileList)
{
    var objThis = this;

    //파일 업로드 
    objThis.dgFS = new DG_JsFileSelector({
        Area: $("#divFileList"),
        Debug: objThis.BoardOption.Debug,
        MaxFileCount: objThis.BoardOption.FileMax,

        ExtAllow: objThis.ImageType,
        LoadComplete: function (objThis_Toss)
        {
            objThis.FS_LoadComplete(objThis, objThis_Toss);
        },
        DeleteComplete: function (jsonFIle, domDivItme)
        {
            objThis.FS_DeleteComplete(objThis, jsonFIle, domDivItme);
        }
    });

    if (undefined === arrFileList
        || null === arrFileList)
    {
        arrFileList = [];
    }

    objThis.dgFS.FileAdd_UI_List(arrFileList);

};


/**
 * 댓글 보기/가리기
 * @param {any} bShow 보기여부
 */
BoardCommon.prototype.PostReplyShowHide = function (bShow)
{
    //타이틀
    var divBoardComm_ReplyTitle = $("#divBoardComm_ReplyTitle");

    //대상 찾기
    var domBoardComm_Reply_List = $(".BoardComm_Reply_List");
    var domBoardComm_Reply_Create = $(".BoardComm_Reply_Create");


    if (false === bShow)
    {//가리기
        divBoardComm_ReplyTitle.removeClass("BoardComm_ReplyTitle_Show");
        divBoardComm_ReplyTitle.addClass("BoardComm_ReplyTitle_Hidden");

        domBoardComm_Reply_List.addClass("v-hidden");
        domBoardComm_Reply_Create.addClass("v-hidden");
    }
    else
    {//보기
        divBoardComm_ReplyTitle.addClass("BoardComm_ReplyTitle_Show");
        divBoardComm_ReplyTitle.removeClass("BoardComm_ReplyTitle_Hidden");

        domBoardComm_Reply_List.removeClass("v-hidden");
        domBoardComm_Reply_Create.removeClass("v-hidden");
    }
};



