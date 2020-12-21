
function UserMgt()
{
    GlobalStatic.PageType_Now = this.constructor.name;

    var objThis = this;

    //페이지 공통기능 로드
    Page.Load({}, function ()
    {
        //화면 인터페이스
        Page.divContents.load("Pages/Admin/UserMgt/UserMgt.html"
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
                AA.HtmlFileLoad("Pages/Admin/UserMgt/UserMgt_ListTitle.html"
                    , function (html)
                    {
                        objThis.UserMgt_ListTitleHtml = html;
                        objThis.Reset_ListTitle(true);
                    }
                    , {}
                );
                AA.HtmlFileLoad("Pages/Admin/UserMgt/UserMgt_ListItem.html"
                    , function (html)
                    {
                        objThis.UserMgt_ListItemHtml = html;
                        objThis.Reset_ListTitle(true);
                    }
                    , {}
                );

                AA.HtmlFileLoad("Pages/Admin/UserMgt/UserMgt_Create.html"
                    , function (html) {
                        objThis.BoardAdmin_CreateHtml = html;
                    }
                    , {}
                );
                
                $("#divFind input[name=findBox]").on("keypress", function () {
                    $(this).val($.trim($(this).val()));
                    if (event.keyCode === 13) {
                        var jsonData = objThis.MakeJsonData_FindUser({
                            typeSearch: $("#divFind select").val()
                            , sSearchWord: $(this).val()
                        });

                        objThis.FindUser(jsonData);
                    }
                });

                $("#divFind [name=findBt]").on("click", function () {
                    var jsonData = objThis.MakeJsonData_FindUser({
                        typeSearch: $("#divFind select").val()
                        , sSearchWord: $("#divFind input[name=findBox]").val()
                    });

                    objThis.FindUser(jsonData);
                });

                // 엑세스 토큰 갱신
                GlobalSign.AccessTokenToInfo();
            });
    });
}

/** 유저 관리에서 검색에 사용할 옵션
 * UserMgtSearchType 참고
 * */
UserMgt.prototype.AdminFaculty =
{
    /**
    없음
    */
    None: 0,


    /**
    아이디
    회원 고유번호
    */
    ID: 11,
    /**
    로그인용 이메일
    회원 아이디
    */
    SignId: 12,
    /**
    닉네임
    */
    Name: 13,
};

/** 사용할 영역 */
UserMgt.prototype.divList = null;
/** 리스트 바인드 공통기능 - ListBindTable */
UserMgt.prototype.ListBind = null;
/** 관리자 기능 */
UserMgt.prototype.AdminFaculty = null;

UserMgt.prototype.UserMgt_ListTitleHtml = "";
UserMgt.prototype.UserMgt_ListItemHtml = "";

UserMgt.prototype.BoardAdmin_CreateHtml = "";


/** 게시판 권한 구분 */
UserMgt.prototype.BoardCheckBoxHtml
    = '<label>'
    + '<input type="checkbox" name="{{GroupName}}" {{checked}} value="{{value}}" />'
    + '<span>{{Name}}</span>'
    + '</label>';

/* □□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□
 * 세팅 내용 표시
 * □□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□ */


/**
 * 리스트를 다시 그린다.
 * @param {bool} bItem 아이템 리스트 갱신여부
 */
UserMgt.prototype.Reset_ListTitle = function (bItem)
{
    var objThis = this;

    if (("" === objThis.UserMgt_ListTitleHtml)
        || ("" === objThis.UserMgt_ListItemHtml))
    {
        return;
    }

    //사용할 기능
    objThis.ListBind
        = new ListBindTable(objThis.divList
            , objThis.UserMgt_ListTitleHtml
            , objThis.UserMgt_ListItemHtml);


    var jsonTitle = {
        idUser: "번호",
        SignEmail: "아이디<br />(닉네임)",
        Password: "비밀번호",
        Phone: "전화번호",
        SignUpDate: "가입일",
        SignInDate: "접속시간",
        IpSignIn : "IP",
        AttendanceCount: "출석수",
        Block: "기능"
    };

    //리스트 그리기 - 타이틀
    objThis.ListBind.BindTitle(jsonTitle);

    //아이템 리스트 갱신 여부
    if (true === bItem)
    {
        objThis.Reset_ListItem();
    }

};


UserMgt.prototype.Reset_ListItem = function ()
{
    var objThis = this;


    AA.get(AA.TokenRelayType.HeadAdd
        , {
            url: FS_Api.Admin_UserList
            , success: function (jsonResult)
            {
                if ("0" === jsonResult.InfoCode)
                {//에러 없음
                    //아이템 리스트 갱신
                    objThis.BindItem_View(jsonResult.UserList);
                }
                else
                {//에러 있음
                    //아웃풋 지우기
                    objThis.divDataBind.html("");
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

/**
 * 아이템 그리기
 * @param {any} arrJsonData 아이템 리스트
 */
UserMgt.prototype.BindItem_View = function (arrJsonData)
{
    var objThis = this;

    for (var i = 0; i < arrJsonData.length; ++i)
    {
        var item = arrJsonData[i];

        if (true === dgIsObject.IsBoolValue(item.Block))
        {
            item.BlockTrue = "checked";
            item.BlockFalse = "";
        }
        else
        {
            item.BlockTrue = "";
            item.BlockFalse = "checked";
        }

        if (true === dgIsObject.IsBoolValue(item.FuncLimit_BoardCreate))
        {
            item.BoardCreateTrue = "checked";
            item.BoardCreateFalse = "";
        }
        else
        {
            item.BoardCreateTrue = "";
            item.BoardCreateFalse = "checked";
        }

        if (true === dgIsObject.IsBoolValue(item.FuncLimit_BoardReplyCreate))
        {
            item.BoardReplyCreateTrue = "checked";
            item.BoardReplyCreateFalse = "";
        }
        else
        {
            item.BoardReplyCreateTrue = "";
            item.BoardReplyCreateFalse = "checked";
        }
    }

    //리스트 그리기 - 아이템
    objThis.ListBind.BindItem(arrJsonData);

    //수정버튼 기본 hide
    $("[id ^= 'btnEdit_']").hide();

    //데이터 수정시 수정 버튼 노출
    $('input[name^=adminUserInfo_Bio_], input[name^=boardCreate_Bio_], input[name^=boardReplyCreate_Bio_]').change(function () {
        var targetId = this.id.split('_');
        $("#btnEdit_" + targetId[1]).show();
    });

    // 전화번호 데이터 수정시 수정 버튼 노출
    $('input[name^=txtPhone]').on("propertychange change keyup paste input", function () {

        // 현재 변경된 데이터 셋팅
        newValue = $(this).val();
        var targetId = this.id.split('txtPhone');
        $("#btnEdit_" + targetId[1]).show();
    });

    // 비밀번호 데이터 수정시 수정 버튼 노출
    $('input[name^=txtPassword]').on("propertychange change keyup paste input", function () {

        // 현재 변경된 데이터 셋팅
        newValue = $(this).val();
        var targetId = this.id.split('txtPassword');
        $("#btnEdit_" + targetId[1]).show();
    });

};


/**
 * 유저 수정
 * @param {any} idUser
 */
UserMgt.prototype.UserInfoEdit = function (idUser)
{
    var objThis = this;
    var result_ad01 = "";

    $("input:checkbox[id='ad01_" + idUser + "']").is(":checked") === true ? result_ad01 = "true" : result_ad01 = "false";
    $("input:checkbox[id='ad02_" + idUser + "']").is(":checked") === true ? result_ad02 = "true" : result_ad02 = "false";
    $("input:checkbox[id='ad03_" + idUser + "']").is(":checked") === true ? result_ad03 = "true" : result_ad03 = "false";

    var jsonDataPara = {
        idUser: dgIsObject.IsIntValue(idUser),
        sPassword: dgIsObject.IsStringValue($("#txtPassword" + idUser).val()),
        sEMail: dgIsObject.IsStringValue($("#txtEMail" + idUser).val()),
        sPhone: dgIsObject.IsStringValue($("#txtPhone" + idUser).val()),
        bBlock: result_ad01,
        bFuncLimit_BoardCreate: result_ad02,
        bFuncLimit_BoardReplyCreate: result_ad03
    };

    AA.put(AA.TokenRelayType.HeadAdd
        , {
            url: FS_Api.Admin_UserEdit
            , data: jsonDataPara
            , success: function (jsonData)
            {
                if ("0" === jsonData.InfoCode)
                {//에러 없음
                    GlobalStatic.MessageBox_Info(objThis.title
                        , "수정 완료 회원 번호 : " + jsonDataPara.idUser);
                }
                else
                {//에러 있음
                    GlobalStatic.MessageBox_Error(objThis.title,
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

UserMgt.prototype.MakeJsonData_FindUser = function (o) {
    var objThis = this;

    var jsonData = {
        typeSearch: dgIsObject.IsIntValue($.trim(o.typeSearch))
        , sSearchWord: dgIsObject.IsStringValue($.trim(o.sSearchWord))
    };

    if (jsonData.typeSearch === 0) {
        GlobalStatic.MessageBox_Error(objThis.Title, "검색할 타입을 선택해주십시오.");
        return null;
    }

    return jsonData;
};

UserMgt.prototype.FindUser = function (jsonData) {
    var objThis = this;
    
    AA.get(AA.TokenRelayType.HeadAdd
        , {
            url: FS_Api.Admin_UserList
            , data: jsonData
            , success: function (jsonResult) {
                if ("0" === jsonResult.InfoCode) 
                {//에러 없음
                    //아이템 리스트 갱신
                    objThis.BindItem_View(jsonResult.UserList);
                }
                else {//에러 있음
                    //아웃풋 지우기
                    objThis.divDataBind.html("");
                    GlobalStatic.MessageBox_Error("",
                        "error code : " + data.InfoCode + "<br />"
                        + "내용 : " + data.Message);
                }
            }
            , error: function (error) {
                console.log(error);
            }
        });
};

/**
 * 유저 생성 창 표시
 */
UserMgt.prototype.UserCreateShow = function () {
    var objThis = this;

    //생성창 만들기

    //권한 html
    //var sBoardFacultyHtml = objThis.BoardFaculty.InputHtml(0, 0);

    var sHtml
        = GlobalStatic.DataBind.DataBind_All(
            objThis.BoardAdmin_CreateHtml
            , {
                Title: "유저 생성"
            })
            .ResultString;

    //창표시
    popTemp = DG_Popup.Show({
        Content: sHtml,
        ContentCss: "",

        top: 100,
        StartViewWeight: true
    });

};


UserMgt.prototype.OnClick_SignEmailCheck = function () {
    var sEmail = $("#txtEMail").val();

    if (false === dgIsObject.IsStringNotEmpty(sEmail)) {
        GlobalStatic.MessageBox_Error("", "아이디를 입력하지 않았습니다.");
    }
    else {
        AA.get(AA.TokenRelayType.None
            , {
                url: FS_Api.Sign_SignEmailCheck
                , data: { sEmail: sEmail }
                , success: function (jsonData) {
                    if ("0" === jsonData.InfoCode) {//에러 없음
                        GlobalStatic.MessageBox_Info(""
                            , "사용할 수 있는 아이디입니다.");
                    }
                    else {//에러 있음
                        GlobalStatic.MessageBox_Error("",
                            "실패코드 : " + jsonData.InfoCode + "<br /> "
                            + jsonData.Message);
                    }

                }
                , error: function (jqXHR, textStatus, errorThrown) {
                    GlobalStatic.MessageBox_Error("", "알수 없는 오류가 발생했습니다.");
                }
            });
    }//end if
};

UserMgt.prototype.OnClick_SignNameCheck = function () {
    var sName = $("#txtNickName").val();

    if (false === dgIsObject.IsStringNotEmpty(sName)) {
        GlobalStatic.MessageBox_Error("", "닉네임을 입력하지 않았습니다.");
    }
    else {
        AA.get(AA.TokenRelayType.None
            , {
                url: FS_Api.Sign_ViewNameCheck
                , data: { sViewName: sName }
                , success: function (jsonData) {
                    if ("0" === jsonData.InfoCode) {//에러 없음
                        GlobalStatic.MessageBox_Info(""
                            , "사용할 수 있는 닉네임입니다.");
                    }
                    else {//에러 있음
                        GlobalStatic.MessageBox_Error("",
                            "실패코드 : " + jsonData.InfoCode + "<br /> "
                            + jsonData.Message);
                    }

                }
                , error: function (jqXHR, textStatus, errorThrown) {
                    GlobalStatic.MessageBox_Error("", "알수 없는 오류가 발생했습니다.");
                }
            });
    }//end if
};


/**
 * 
 */
UserMgt.prototype.UserCreate = function () {
    var objThis = this;

    //게시판 아이디
    var nBoardId = 0;
    
    //저장된 값 받기
    var jsonOption = {
        sEmail: dgIsObject.IsStringValue($("#txtEMail").val()),
        sPassword: dgIsObject.IsIntValue($("#pwPassword").val()),
        sEmail2: dgIsObject.IsIntValue($("#txtEMail2").val()),
        sPhone: dgIsObject.IsStringValue($("#txtPhone").val()),
        sViewName: dgIsObject.IsStringValue($("#txtNickName").val())
    };

    //기본 옵션
    var jsonOptDefault = {
        sEmail: "",
        sPassword: "",
        sEmail2: "",
        sPhone: "",
        sViewName: ""
    };

    //옵션 합치기
    var jsonOpt = Object.assign({}, jsonOptDefault, jsonOption);

    var bReturn = true;
    var sMsg = "";

    if (true === bReturn) {
        AA.post(AA.TokenRelayType.HeadAdd, {
            url: FS_Api.Sign_SignUp_Auth,
            data: jsonOpt,
            success: function (data) {
                var sMsg = "";

                if ("0" === data.InfoCode) {//성공
                    //팝업 닫기
                    DG_Popup.Close();

                    sMsg = "유저를 생성하였습니다.";

                    //아이템 리스트 
                    objThis.Reset_ListItem();
                }
                else {// 실패
                    sMsg = data.Message;
                }


                if ("" !== sMsg) {
                    GlobalStatic.MessageBox_Error("", sMsg);
                }


            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(jqXHR);
                alert(GlobalStatic.Lang.Now.UnknownError);
            }
        });

        if (false === bReturn) {
            GlobalStatic.MessageBox_Error("유저 생성", sMsg);
        }
    }

    return bReturn;
};
