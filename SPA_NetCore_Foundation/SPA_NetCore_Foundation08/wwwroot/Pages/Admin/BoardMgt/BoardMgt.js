
function BoardMgt()
{
    GlobalStatic.PageType_Now = this.constructor.name;

    var objThis = this;

    //게시판 기능 지원 클래스
    objThis.BoardFaculty = new BoardFaculty(objThis.BoardCheckBoxHtml);
    objThis.BoardFacultyType = objThis.BoardFaculty.BoardFacultyType;

    //게시판 권한 지원클래스
    objThis.BoardAuth = new BoardAuth(objThis.BoardCheckBoxHtml);
    //게시판 권한 백업
    objThis.BoardAuthorityType = objThis.BoardAuth.BoardAuthorityType;
    
    //페이지 공통기능 로드
    Page.Load({}, function ()
    {
        //화면 인터페이스
        Page.divContents.load("Pages/Admin/BoardMgt/BoardMgt.html"
            , function () 
            {
                //데이터 바인드 영역
                objThis.divList = $("#divList");

                //관리자 기능
                objThis.AdminFaculty
                    = new AdminFaculty(
                        {
                            MenuArea: $("#divAdmin_Menu"),
                            BtnName: "liAdminMenu_" + GlobalStatic.PageType_Now
                        });

                //메뉴 불러오기
                objThis.AdminFaculty.MenuLoad();

                //데이터 바인드에 사용할 리스트 아이템 html 받아오기
                AA.HtmlFileLoad("Pages/Admin/BoardMgt/BoardMgt_ListTitle.html"
                    , function (html)
                    {
                        objThis.BoardMgt_ListTitleHtml = html;
                        objThis.Reset_ListTitle(true);
                    }
                    , {}
                );
                AA.HtmlFileLoad("Pages/Admin/BoardMgt/BoardMgt_ListItem.html"
                    , function (html)
                    {
                        objThis.BoardMgt_ListItemHtml = html;
                        objThis.Reset_ListTitle(true);
                    }
                    , {}
                );

                AA.HtmlFileLoad("Pages/Admin/BoardMgt/BoardMgt_Create.html"
                    , function (html)
                    {
                        objThis.BoardMgt_CreateHtml = html;
                    }
                    , {}
                );
                AA.HtmlFileLoad("Pages/Admin/BoardMgt/BoardMgt_Edit.html"
                    , function (html)
                    {
                        objThis.BoardMgt_EditHtml = html;
                    }
                    , {}
                );

                // 엑세스 토큰 갱신
                GlobalSign.AccessTokenToInfo();
            });
    });
} 

/** 이 클래스에서 사용할 제목 */
BoardMgt.prototype.Title = "게시판 관리";

/** 사용할 영역 */
BoardMgt.prototype.divList = null;
/** 관리자 기능 */
BoardMgt.prototype.AdminFaculty = null;

BoardMgt.prototype.BoardMgt_ListTitleHtml = "";
BoardMgt.prototype.BoardMgt_ListItemHtml = "";

BoardMgt.prototype.BoardMgt_CreateHtml = "";
BoardMgt.prototype.BoardMgt_EditHtml = "";



/** 게시판 기능 관련 */
BoardMgt.prototype.BoardFaculty = null;
/** 게시판 기능 구분 */
BoardMgt.prototype.BoardFacultyType = {};
/** 게시판 권한 관련 */
BoardMgt.prototype.BoardAuth = null;
/** 게시판 권한 구분 */
BoardMgt.prototype.BoardAuthorityType = {};


/** 게시판 권한 구분 */
BoardMgt.prototype.BoardCheckBoxHtml
    = '<label>'
    + '<input type="checkbox" name="{{GroupName}}" {{checked}} value="{{value}}" />'
    + '<span>{{Name}}</span>'
    + '</label>';


/** 리스트 바인드 공통기능 */
BoardMgt.prototype.ListBind = null;

BoardMgt.prototype.BoardStateType =
{
    None: 0,
    Use: 1,
    NotUse : 2
};


/** 첫바인딩을 했는지 여부 */
BoardMgt.prototype.bFirstBind = false;


/**
 * 리스트를 다시 그린다.
 * @param {bool} bItem 아이템 리스트 갱신여부
 */
BoardMgt.prototype.Reset_ListTitle = function (bItem)
{
    var objThis = this;

    if ((true === objThis.bFirstBind)
        || ("" === objThis.BoardMgt_ListTitleHtml)
        || ("" === objThis.BoardMgt_ListItemHtml))
    {
        return;
    }

    //첫 바인딩이 완료 되었는지 여부
    objThis.bFirstBind = true;

    //사용할 기능
    objThis.ListBind
        = new ListBindTable(objThis.divList
            , objThis.BoardMgt_ListTitleHtml
            , objThis.BoardMgt_ListItemHtml);

    //리스트 그리기 - 타이틀
    objThis.ListBind.BindTitle(
        {
            idBoard: "번호",
            Title: "제목",
            BoardState: "상태",
            BoardAuth: "개별권한",
            CreateDate: "생성일",
            AuthorityDefault: "권한",
            Memo: "설명"
        });

    //아이템 리스트 갱신 여부
    if (true === bItem)
    {
        objThis.Reset_ListItem();
    }

};

BoardMgt.prototype.Reset_ListItem = function ()
{
    var objThis = this;

    AA.get(AA.TokenRelayType.HeadAdd
        , {
            url: FS_Api.BoardMgt_List
            , success: function (jsonData)
            {
                if ("0" === jsonData.InfoCode)
                {//에러 없음
                    //리스트 그리기 - 아이템
                    objThis.ListBind.BindItem(jsonData.List);
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
 * 게시판 생성 창 표시
 */
BoardMgt.prototype.BoardCreateShow = function ()
{
    var objThis = this;

    //생성창 만들기

    //기능 html
    var sBoardAuthHtml = objThis.BoardAuth.InputHtml(0, 0);

    //권한 html
    var sBoardFacultyHtml = objThis.BoardFaculty.InputHtml(0, 0);

    var sHtml
        = GlobalStatic.DataBind.DataBind_All(
            objThis.BoardMgt_CreateHtml
            , {
                Title: "게시판 생성",
                BoardAuthHtml: sBoardAuthHtml
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

};

/**
 * 
 */
BoardMgt.prototype.BoardCreate = function ()
{
    var objThis = this;

    //게시판 아이디
    var nBoardId = 0;
    //체크된 권한 계산
    var typeAuth = objThis.BoardAuth.GetHtmlAuth(nBoardId);

    //저장된 값 받기
    var jsonOption = {
        sTitle: dgIsObject.IsStringValue($("#txtBoardMgt_Create_Title").val()),
        typeBoardState: dgIsObject.IsIntValue($("#selectBoardMgt_Create_State").val()),
        nAuthorityDefault: typeAuth,
        sMemo: dgIsObject.IsStringValue($("#txtBoardMgt_Create_Memo").val())
    };

    //기본 옵션
    var jsonOptDefault = {
        sTitle: "",
        typeBoardState: 0,
        nAuthorityDefault: 0,
        sMemo: ""
    };

    //옵션 합치기
    var jsonOpt = Object.assign({}, jsonOptDefault, jsonOption);

    var bReturn = true;
    var sMsg = "";

    if ("" === jsonOpt.sTitle)
    {
        sMsg = "제목을 입력해 주세요.";
        bReturn = false;
    }

    if (true === bReturn)
    {
        AA.post(AA.TokenRelayType.HeadAdd, {
            url: FS_Api.BoardMgt_Create,
            data: jsonOpt,
            success: function (data)
            {
                var sMsg = "";

                if ("0" === data.InfoCode)
                {//성공
                    //팝업 닫기
                    DG_Popup.Close();

                    sMsg = "게시판을 생성하였습니다.";

                    //아이템 리스트 
                    objThis.Reset_ListItem();
                }
                else
                {// 실패
                    sMsg = data.Message;
                }


                if ("" !== sMsg)
                {
                    GlobalStatic.MessageBox_Error(objThis.Title, sMsg);
                }


            },
            error: function (jqXHR, textStatus, errorThrown)
            {
                console.log(jqXHR);
                GlobalStatic.MessageBox_Error(objThis.Title, GlobalStatic.Lang.Now.UnknownError);
            }
        });
    }

    if (false === bReturn)
    {
        GlobalStatic.MessageBox_Error(objThis.Title, sMsg);
    }

    return bReturn;
};

/**
 * 수정할 게시판 표시
 * @param {int} nBoardId 수정할 게시판 번호
 */
BoardMgt.prototype.BoardEidtShow = function (nBoardId)
{
    var bReturn = true;
    var sMsg = "";

    var objThis = this;

    if (0 >= nBoardId)
    {
        bReturn = false;
        sMsg = "선택된 게시판이 없습니다.";
    }

    if (true === bReturn)
    {
        AA.get(AA.TokenRelayType.HeadAdd
            , {
                url: FS_Api.BoardMgt_Item
                , data: { nBoardId: nBoardId}
                , success: function (jsonData)
                {
                    console.log(jsonData);

                    if ("0" === jsonData.InfoCode)
                    {//에러 없음

                        if (0 < jsonData.List.length)
                        {//선택된 아이템이 있다.
                            var jsonItem = jsonData.List[0];
                            //제목
                            jsonItem.TitleBoard = jsonItem.Title;
                            //제목줄 표시용 제목
                            jsonItem.Title = "게시판 수정";

                            //상태*******************************
                            jsonItem.State_Use = "";
                            jsonItem.State_NotUse = "";
                            switch (jsonItem.BoardState)
                            {
                                case objThis.BoardStateType.Use:
                                    jsonItem.State_Use = "selected";
                                    break;
                                case objThis.BoardStateType.NotUse:
                                    jsonItem.State_NotUse = "selected";
                                    break;
                            }

                            //기본 권한***************************
                            //체크항목 초기화

                            //각 항목 값있는지 확인
                            jsonItem.BoardFacultyHtml
                                = objThis.BoardFaculty.InputHtml(
                                    jsonItem.idBoard
                                    , jsonItem.BoardFaculty);

                            //각 항목 값있는지 확인
                            jsonItem.BoardAuthHtml
                                = objThis.BoardAuth.InputHtml(
                                    jsonItem.idBoard
                                    , jsonItem.AuthorityDefault);


                            //수정창 만들기
                            var sHtmlItem
                                = GlobalStatic.DataBind.DataBind_All(
                                    objThis.BoardMgt_EditHtml
                                    , jsonItem)
                                    .ResultString;

                            //창표시
                            popTemp = DG_Popup.Show({
                                Content: sHtmlItem,
                                ContentCss: "",

                                top: 100,
                                left: 100,
                                StartViewWeight: true
                            });
                        }
                        else
                        {//없다.
                        }
                        
                    }
                    else
                    {//에러 있음
                        //아웃풋 지우기
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
        GlobalStatic.MessageBox_Error("게시판 수정", sMsg);
    }
};

BoardMgt.prototype.BoardEidt = function ()
{
    var objThis = this;

    //게시판 아이디
    var nBoardId = dgIsObject.IsIntValue($("#txtBoardMgt_Edit_idBoard").val());

    //체크된 기능 계산
    var typeFaculty = objThis.BoardFaculty.GetHtmlData(nBoardId);
    //체크된 권한 계산
    var typeAuth = objThis.BoardAuth.GetHtmlAuth(nBoardId);

    //저장된 값 받기
    var jsonOption = {
        nBoardId: nBoardId,
        sTitle: dgIsObject.IsStringValue($("#txtBoardMgt_Edit_Title").val()),
        nShowCount: dgIsObject.IsIntValue($("#txtBoardMgt_Edit_ShowCount").val()),
        typeBoardState: dgIsObject.IsIntValue($("#selectBoardMgt_Edit_State").val()),
        typeBoardFaculty: typeFaculty,
        nAuthorityDefault: typeAuth,
        sMemo: dgIsObject.IsStringValue($("#txtBoardMgt_Edit_Memo").val())
    };

    //기본 옵션
    var jsonOptDefault = {
        nBoardId:0,
        sTitle: "",
        typeBoardState: 0,
        nAuthorityDefault: 0,
        sMemo: ""
    };

    //옵션 합치기************************************
    var jsonOpt = Object.assign({}, jsonOptDefault, jsonOption);

    var bReturn = true;
    var sMsg = "";

    if ("" === jsonOpt.sTitle)
    {
        sMsg = "제목을 입력해 주세요.";
        bReturn = false;
    }
    else if (0 >= jsonOpt.nBoardId)
    {
        sMsg = "선택된 게시판이 없습니다.";
        bReturn = false;
    }


    
    if (true === bReturn)
    {
        //값 전달
        AA.put(AA.TokenRelayType.HeadAdd, {
            url: FS_Api.BoardMgt_Edit,
            data: jsonOpt,
            success: function (data)
            {
                var sMsg = "";

                if ("0" === data.InfoCode)
                {//성공
                    //팝업 닫기
                    DG_Popup.Close();

                    sMsg = "게시판을 수정하였습니다.";

                    //아이템 리스트 
                    objThis.Reset_ListItem();

                    //
                    GlobalStatic.MessageBox_Info(objThis.Title, sMsg);
                }
                else
                {// 실패
                    sMsg = data.Message;
                    GlobalStatic.MessageBox_Error(objThis.Title, sMsg);
                }

            },
            error: function (jqXHR, textStatus, errorThrown)
            {
                console.log(jqXHR);
                //alert(GlobalStatic.Lang.Now.UnknownError);
            }
        });
    }


    if (false === bReturn)
    {
        GlobalStatic.MessageBox_Error("게시판 생성", sMsg);
    }

    return bReturn;

};




BoardMgt.prototype.AddItems = function ()
{
    var objThis = this;
    AA.post(AA.TokenRelayType.HeadAdd
        , {
            url: FS_Api.BoardMgt_TestPostAdd
            , data: { nBoardId: 1 }
            , success: function (data)
            {
                console.log(data);

                if ("0" === data.InfoCode)
                {//에러 없음
                    DG_MessageBox.Show({
                        Title: "게시판 아이템 생성",
                        Content: "생성 완료",

                        ButtonShowType: DG_MessageBox.ButtonShowType.Ok,
                        BigIconType: DG_MessageBox.BigIconType.Success,

                        ButtonEvent: function (btnType)
                        {
                            DG_Popup.Close();
                        }
                    });
                }
                else
                {//에러 있음
                    //아웃풋 지우기
                    objThis.divOutput.html("");
                    GlobalStatic.MessageBox_Error("", 
                        "error code : " + data.InfoCode + "<br />"
                        + "내용 : " + data.Message);
                }
            }
            , error: function (error)
            {
                console.log(error);
            }
        });
};
