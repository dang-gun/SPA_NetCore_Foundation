
function Admin()
{
    GlobalStatic.PageType_Now = this.constructor.name;

    var objThis = this;

    //페이지 공통기능 로드
    Page.Load(function ()
    {
        //화면 인터페이스
        Page.divContents.load(FS_FUrl.Admin_Home
            , function () 
            {
                //데이터 바인드 영역
                objThis.divDataBind = $("#divDataBind");

                //데이터 바인드에 사용할 리스트 아이템 html 받아오기
                AA.HtmlFileLoad(FS_FUrl.Admin_Setting_ListItem
                    , function (html)
                    {
                        objThis.Admin_Setting_ListItemHtml = html;
                    }
                    , {}
                );
                AA.HtmlFileLoad(FS_FUrl.Admin_Setting_ListTitle
                    , function (html)
                    {
                        objThis.Admin_Setting_ListTitleHtml = html;
                    }
                    , {}
                );
                AA.HtmlFileLoad(FS_FUrl.Admin_User_ListItem
                    , function (html)
                    {
                        objThis.Admin_User_ListItemHtml = html;
                    }
                    , {}
                );
            });
    });
}

/** 데이터 바인드 영역 */
Admin.prototype.divDataBind = null;

Admin.prototype.Admin_Setting_ListItemHtml = "";
Admin.prototype.Admin_Setting_ListTitleHtml = "";
Admin.prototype.Admin_User_ListItemHtml = "";


Admin.prototype.SettingLoad = function ()
{
    var objThis = this;

    AA.put(AA.TokenRelayType.HeadAdd
        , {
            url: FS_Api.Admin_SettingLoad
            , success: function (jsonData)
            {
                console.log(jsonData);

                if ("0" === jsonData.InfoCode)
                {//에러 없음
                    alert("불러오기 완료");
                }
                else
                {//에러 있음
                    //아웃풋 지우기
                    objThis.divDataBind.html("");
                    alert("error code : " + data.InfoCode + "\n"
                        + "내용 : " + data.message);
                }
            }
            , error: function (error)
            {
                console.log(error);
            }
        });
};

Admin.prototype.SettingListGet = function ()
{
    var objThis = this;

    AA.get(AA.TokenRelayType.HeadAdd
        , {
            url: FS_Api.Admin_SettingList
            , success: function (jsonData)
            {
                console.log(jsonData);

                if ("0" === jsonData.InfoCode)
                {//에러 없음
                    objThis.SettingListBind(jsonData.SettingList);
                }
                else
                {//에러 있음
                    //아웃풋 지우기
                    objThis.divDataBind.html("");
                    alert("error code : " + data.InfoCode + "\n"
                        + "내용 : " + data.message);
                }
            }
            , error: function (error)
            {
                console.log(error);
            }
        });
};

Admin.prototype.SettingListBind = function (arrData)
{
    //리스트 html을 임시 저장
    var sHtmlTemp = "";

    sHtmlTemp
        += GlobalStatic.DataBind.DataBind_All(
            this.Admin_Setting_ListTitleHtml
            , {
                idSetting_Data: -1
                , Number: "순서"
                , Name: "이름"
                , ValueData: "값"
                , Description: "설명"
            })
            .ResultString;

    for (var i = 0; i < arrData.length; ++i)
    {
        var jsonItemData = arrData[i];
        sHtmlTemp
            += GlobalStatic.DataBind.DataBind_All(
                this.Admin_Setting_ListItemHtml
                , jsonItemData)
                .ResultString;
    }

    this.divDataBind.html(sHtmlTemp);
};



Admin.prototype.SettingApplyGet = function ()
{
    var objThis = this;

    AA.get(AA.TokenRelayType.HeadAdd
        , {
            url: FS_Api.Admin_SettingApply
            , success: function (jsonData)
            {
                console.log(jsonData);

                if ("0" === jsonData.InfoCode)
                {//에러 없음
                    objThis.SettingApplyBind(jsonData.SettingList);
                }
                else
                {//에러 있음
                    //아웃풋 지우기
                    objThis.divDataBind.html("");
                    alert("error code : " + data.InfoCode + "\n"
                        + "내용 : " + data.message);
                }
            }
            , error: function (error)
            {
                console.log(error);
            }
        });
};


Admin.prototype.SettingApplyBind = function (arrData)
{
    //리스트 html을 임시 저장
    var sHtmlTemp = "";

    sHtmlTemp
        += GlobalStatic.DataBind.DataBind_All(
            this.Admin_Setting_ListTitleHtml
            , {
                idSetting_Data: -1
                , Number: "순서"
                , Name: "이름"
                , ValueData: "값"
                , Description: "설명"
            })
            .ResultString;

    for (var i = 0; i < arrData.length; ++i)
    {
        var jsonItemData = arrData[i];
        sHtmlTemp
            += GlobalStatic.DataBind.DataBind_All(
                this.Admin_Setting_ListTitleHtml
                , jsonItemData)
                .ResultString;
    }

    this.divDataBind.html(sHtmlTemp);
};


/**
 * 
 * @param {int} nidSetting_Data 수정할 대상 고유키
 */
Admin.prototype.SettingSet = function (nidSetting_Data)
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
            url: FS_Api.Admin_SettingSet
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
                    alert("수정 완료");
                }
                else
                {//에러 있음
                    alert("error code : " + data.InfoCode + "\n"
                        + "내용 : " + data.message);
                }
            }
            , error: function (error)
            {
                console.log(error);
            }
        });
};



Admin.prototype.UserListGet = function ()
{
    var objThis = this;

    AA.get(AA.TokenRelayType.HeadAdd
        , {
            url: FS_Api.Admin_UserList
            , success: function (jsonData)
            {
                console.log(jsonData);

                if ("0" === jsonData.InfoCode)
                {//에러 없음
                    objThis.UserListBind(jsonData.UserList);
                }
                else
                {//에러 있음
                    //아웃풋 지우기
                    objThis.divDataBind.html("");
                    alert("error code : " + data.InfoCode + "\n"
                        + "내용 : " + data.message);
                }
            }
            , error: function (error)
            {
                console.log(error);
            }
        });
};

Admin.prototype.UserListBind = function (arrData)
{
    //리스트 html을 임시 저장
    var sHtmlTemp = "";

    for (var i = 0; i < arrData.length; ++i)
    {
        var jsonItemData = arrData[i];
        sHtmlTemp
            += GlobalStatic.DataBind.DataBind_All(
                    this.Admin_User_ListItemHtml
                    , jsonItemData)
                .ResultString;
    }

    this.divDataBind.html(sHtmlTemp);
};