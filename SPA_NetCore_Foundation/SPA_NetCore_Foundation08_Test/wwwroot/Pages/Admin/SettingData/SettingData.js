
function SettingData()
{
    GlobalStatic.PageType_Now = this.constructor.name;


    var objThis = this;

    //페이지 공통기능 로드
    Page.Load({}, function () {
        //홈 인터페이스
        Page.divContents.load(FS_FUrl.Admin_SettingData_SettingDataHtml
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
                AA.HtmlFileLoad(FS_FUrl.Admin_SettingData_SettingData_ListTitleHtml
                    , function (html)
                    {
                        objThis.SettingData_ListTitleHtml = html;
                        objThis.Reset_ListTitle(true);
                    }
                    , {}
                );
                AA.HtmlFileLoad(FS_FUrl.Admin_SettingData_SettingData_ListItemHtml
                    , function (html)
                    {
                        objThis.SettingData_ListItemHtml = html;
                        objThis.Reset_ListTitle(true);
                    }
                    , {}
                );
            });
    });
}

/** 이 클래스 제목 */
SettingData.prototype.Title = "세팅 데이터";

/** 사용할 영역 */
SettingData.prototype.divList = null;
/** 리스트 바인드 공통기능 - ListBindTable */
SettingData.prototype.ListBind = null;
/** 관리자 기능 */
SettingData.prototype.AdminFaculty = null;

SettingData.prototype.SettingData_ListTitleHtml = "";
SettingData.prototype.SettingData_ListItemHtml = "";


/* □□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□
 * 세팅 내용 표시
 * □□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□ */


/**
 * 리스트를 다시 그린다.
 * @param {bool} bItem 아이템 리스트 갱신여부
 */
SettingData.prototype.Reset_ListTitle = function (bItem)
{
    var objThis = this;

    if (("" === objThis.SettingData_ListTitleHtml)
        || ("" === objThis.SettingData_ListItemHtml))
    {
        return;
    }

    //사용할 기능
    objThis.ListBind
        = new ListBindTable(objThis.divList
            , objThis.SettingData_ListTitleHtml
            , objThis.SettingData_ListItemHtml);


    var jsonTitle = {
        idSetting_Data: -1
        , Number: "순서"
        , Name: "이름"
        , ValueData: "값"
        , Description: "설명"
    };

    //리스트 그리기 - 타이틀
    objThis.ListBind.BindTitle(jsonTitle);

    //아이템 리스트 갱신 여부
    if (true === bItem)
    {
        objThis.Reset_ListItem();
    }

};


SettingData.prototype.Reset_ListItem = function ()
{
    var objThis = this;


    AA.get(AA.TokenRelayType.HeadAdd
        , {
            url: FS_Api.SettingData_SettingList
            , success: function (jsonResult)
            {
                if ("0" === jsonResult.InfoCode)
                {//에러 없음
                    //리스트 그리기 - 아이템
                    objThis.BindItem_View(jsonResult.SettingList);
                }
                else
                {//에러 있음
                    GlobalStatic.MessageBox_Error(objThis.Title,
                        "error code : " + data.InfoCode + "<br />"
                        + "내용 : " + data.Message);
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
SettingData.prototype.BindItem_View = function (arrJsonData)
{
    var objThis = this;

    //리스트 그리기 - 아이템
    objThis.ListBind.BindItem(arrJsonData);
};


/**
 * 수정
 * @param {int} nidSetting_Data 수정할 대상 고유키
 */
SettingData.prototype.SettingSet = function (nidSetting_Data)
{
    //내 개체 찾기
    var divMe = $("#div" + nidSetting_Data);

    //데이터 읽기
    var sNumber = divMe.find(".SettingItem_Number > input").val();
    var sName = divMe.find(".SettingItem_Name > input").val();
    var sValueData = divMe.find(".SettingItem_ValueData > input").val();
    var sDescription = divMe.find(".SettingItem_Description > input").val();

    AA.post(AA.TokenRelayType.HeadAdd
        , {
            url: FS_Api.SettingData_SettingSet
            , data: {
                idSetting_Data: nidSetting_Data
                , Number: Number(sNumber)
                , Name: sName
                , ValueData: sValueData
                , Description: sDescription
            }
            , success: function (jsonData)
            {
                if ("0" === jsonData.InfoCode)
                {//에러 없음
                    GlobalStatic.MessageBox_Info(objThis.Title, "수정 완료");
                }
                else
                {//에러 있음
                    GlobalStatic.MessageBox_Error(objThis.Title,
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


/* □□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□
 * 세팅 적용 내용 확인
 * □□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□ */


SettingData.prototype.SettingApplyGet = function ()
{
    var objThis = this;


    AA.get(AA.TokenRelayType.HeadAdd
        , {
            url: FS_Api.SettingData_SettingApply
            , success: function (jsonResult)
            {
                if ("0" === jsonResult.InfoCode)
                {//에러 없음
                    //리스트 그리기 - 아이템
                    objThis.BindItem_View(jsonResult.SettingList);
                }
                else
                {//에러 있음
                    GlobalStatic.MessageBox_Error(objThis.Title,
                        "error code : " + data.InfoCode + "<br />"
                        + "내용 : " + data.Message);
                    objThis.ListBind.BindItem([]);
                }
            }
            , error: function (error)
            {
                console.log(error);
            }
        });
};



/* □□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□
 * 세팅 적용
 * □□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□ */

/** DB에 있는 세팅정보를 메모리로 읽어들인다. */
SettingData.prototype.SettingLoad = function ()
{
    var objThis = this;

    AA.put(AA.TokenRelayType.HeadAdd
        , {
            url: FS_Api.SettingData_SettingLoad
            , success: function (jsonData)
            {
                if ("0" === jsonData.InfoCode)
                {//에러 없음
                    //alert("불러오기 완료");
                    GlobalStatic.MessageBox_Info(objThis.Title, "불러오기 완료");
                }
                else
                {//에러 있음
                    //아웃풋 지우기
                    objThis.divDataBind.html("");
                    GlobalStatic.MessageBox_Error(objThis.Title,
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