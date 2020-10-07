
function BoardMgtAuth(nBoardId)
{
    var objThis = this;


    GlobalStatic.PageType_Now = objThis.constructor.name;

    //선택된 게시판 아이디
    objThis.BoardId = nBoardId;
    //게시판 권한
    objThis.BoardAuth = new BoardAuth(objThis.BoardCheckBoxHtml);
    
    //페이지 공통기능 로드
    Page.Load({}, function ()
    {
        //화면 인터페이스
        Page.divContents.load("Pages/Admin/BoardAdmin/BoardMgtAuth/BoardMgtAuth.html"
            , function () 
            {
                //데이터 바인드 영역
                objThis.divList = $("#divList");

                //메뉴
                AA.HtmlFileLoad(FS_FUrl.Admin_Admin_Menu
                    , function (html)
                    {
                        $("#divAdmin_Menu").html(html);
                    }
                    , {}
                );


                //데이터 바인드에 사용할 리스트 아이템 html 받아오기
                AA.HtmlFileLoad("Pages/Admin/BoardAdmin/BoardMgtAuth/BoardMgtAuth_ListTitle.html"
                    , function (html)
                    {
                        objThis.BoardMgtAuth_ListTitleHtml = html;
                        objThis.Reset_ListTitle(true);
                    }
                    , {}
                );
                AA.HtmlFileLoad("Pages/Admin/BoardAdmin/BoardMgtAuth/BoardMgtAuth_ListItem.html"
                    , function (html)
                    {
                        objThis.BoardMgtAuth_ListItemHtml = html;
                        objThis.Reset_ListTitle(true);
                    }
                    , {}
                );


                // 엑세스 토큰 갱신
                GlobalSign.AccessTokenToInfo();
            });
    });
} 

/** 게시판 권한 구분 */
BoardMgtAuth.prototype.BoardCheckBoxHtml
    = '<label>'
    + '<input type="checkbox" name="{{GroupName}}" {{checked}} value="{{value}}" />'
    + '<span>{{Name}}</span>'
    + '</label>';

/** 리스트 바인딩 영역 */
BoardMgtAuth.prototype.divList = null;

BoardMgtAuth.prototype.BoardMgtAuth_ListTitleHtml = "";
BoardMgtAuth.prototype.BoardMgtAuth_ListItemHtml = "";

/** 첫바인딩을 했는지 여부 */
BoardMgtAuth.prototype.bFirstBind = false;


/** 사용할 게시판 아이디 */
BoardMgtAuth.prototype.BoardId = 0;



/** 리스트 바인드 공통기능 */
BoardMgtAuth.prototype.ListBind = null;



/**
 * 리스트를 다시 그린다.
 * @param {bool} bItem 아이템 리스트 갱신여부
 */
BoardMgtAuth.prototype.Reset_ListTitle = function (bItem)
{
    var objThis = this;

    if ((true === objThis.bFirstBind)
        || ("" === objThis.BoardMgtAuth_ListTitleHtml)
        || ("" === objThis.BoardMgtAuth_ListItemHtml))
    {
        return;
    }



    //사용할 기능
    objThis.ListBind
        = new ListBind(
            {
                TableArea: objThis.divList
                , ListBind_ListTitleHtml: objThis.BoardMgtAuth_ListTitleHtml
                , ListBind_ListItemHtml: objThis.BoardMgtAuth_ListItemHtml
            });


    //리스트 그리기 - 타이틀
    objThis.ListBind.BindTitle(
        {
            UserName: "대상자",
            AuthState: "상태",
            BoardAuthHtml: "개별권한",
            EditDate: "수정날짜",
            Memo: "설명"
        });

    //아이템 리스트 갱신 여부
    if (true === bItem)
    {
        objThis.Reset_ListItem();
    }

};


BoardMgtAuth.prototype.Reset_ListItem = function ()
{
    var objThis = this;

    AA.get(AA.TokenRelayType.HeadAdd
        , {
            url: FS_Api.Admin_BoardAuthList
            , data: { nBoardId: objThis.BoardId }
            , success: function (jsonData)
            {
                if ("0" === jsonData.InfoCode)
                {//에러 없음
                    //리스트 그리기 - 아이템
                    objThis.BindItem_View(jsonData.List);
                }
                else
                {//에러 있음
                    objThis.ListBind.BindItem([]);
                }
            }
            , error: function (error)
            {
                console.log(error);
            }
        });
};

/**
 * 아이템 그리기
 * @param {any} arrJsonData 아이텔 리스트
 */
BoardMgtAuth.prototype.BindItem_View = function (arrJsonData)
{
    var objThis = this;

    for (var i = 0; i < arrJsonData.length; ++i)
    {
        var itemJson = arrJsonData[i];
        itemJson.BoardAuthHtml
            = objThis.BoardAuth.InputHtml(
                itemJson.idBoardAuthority
                , itemJson.Authority);
    }


    //리스트 그리기 - 아이템
    objThis.ListBind.BindItem(arrJsonData);
};

/**
 * 유저 권한 추가
 */
BoardMgtAuth.prototype.AddUser = function ()
{
    var objThis = this;

    var bReturn = true;
    var sMsg = "";

    //아이디 받기
    var nUserId = dgIsObject.IsIntValue($("#txtUserId").val());

    if (0 >= nUserId)
    {
        bReturn = false;
        sMsg = "유저를 지정하세요.";
    }

    if (true === bReturn)
    {
        AA.post(AA.TokenRelayType.HeadAdd
            , {
                url: FS_Api.Admin_BoardAuthAdd
                , data: {
                    nBoardId: objThis.BoardId,
                    nUserId: nUserId
                }
                , success: function (jsonData)
                {
                    if ("0" === jsonData.InfoCode)
                    {//에러 없음
                        DG_MessageBox.Show({
                            Title: "게시판 권한 유저 추가",
                            Content: "사용자를 추가하였습니다.",

                            ButtonShowType: DG_MessageBox.ButtonShowType.Ok,
                            BigIconType: DG_MessageBox.BigIconType.Success,

                            ButtonEvent: function (btnType)
                            {
                                DG_Popup.Close();
                            }
                        });

                        //리스트 갱신
                        objThis.Reset_ListItem();
                    }
                    else
                    {//에러 있음
                        objThis.ListBind.BindItem([]);
                    }
                }
                , error: function (error)
                {
                    console.log(error);
                }
            });
    }


    if (false === bReturn)
    {
        GlobalStatic.MessageBox_Error("게시판 권한 유저 추가", sMsg);
    }
};


BoardMgtAuth.prototype.BoardAuthEdit = function (nBoardAuthorityId)
{
    var objThis = this;

    //수정할 대상
    var divBAA = $("#divBoardMgtAuth_" + nBoardAuthorityId);
    //체크된 권한 계산
    var nAuthority = objThis.BoardAuth.GetHtmlAuth(nBoardAuthorityId);

    //상태 받기
    var nAuthState = dgIsObject.IsIntValue(divBAA.find(".selectAuthState").val());
    //메모 받기
    var sMemo = dgIsObject.IsStringValue(divBAA.find(".BoardAuthMemo").val());

    AA.patch(AA.TokenRelayType.HeadAdd
        , {
            url: FS_Api.Admin_BoardAuthEdit
            , data: {
                nBoardAuthority: nBoardAuthorityId,
                nAuthority: nAuthority,
                nAuthState: nAuthState,
                sMemo: sMemo,
            }
            , success: function (jsonData)
            {
                if ("0" === jsonData.InfoCode)
                {//에러 없음
                    DG_MessageBox.Show({
                        Title: "게시판 권한 유저 수정",
                        Content: "권한을 수정하였습니다",

                        ButtonShowType: DG_MessageBox.ButtonShowType.Ok,
                        BigIconType: DG_MessageBox.BigIconType.Success,

                        ButtonEvent: function (btnType)
                        {
                            DG_Popup.Close();
                        }
                    });

                    //리스트 갱신
                    objThis.Reset_ListItem();
                }
                else
                {//에러 있음
                    objThis.ListBind.BindItem([]);
                }
            }
            , error: function (error)
            {
                console.log(error);
            }
        });
};