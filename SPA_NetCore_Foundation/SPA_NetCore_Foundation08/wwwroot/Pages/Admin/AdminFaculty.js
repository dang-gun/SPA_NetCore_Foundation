/**
 * 관리자 기능
 * @param {any} jsonOption
 */
function AdminFaculty(jsonOption)
{
    var objThis = this;

    //옵션 합치기
    objThis.jsonOpt = Object.assign(objThis.jsonDefault, jsonOption);
}

/** 완성된 옵션 */
AdminFaculty.prototype.jsonOpt = {};

/** 옵션 기본값 */
AdminFaculty.prototype.jsonDefault =
{
    MenuArea: null,
    BtnName: "",
};

/** 메뉴 URL */
AdminFaculty.prototype.MenuUrl = FS_FUrl.Admin_Admin_MenuHtml;

/** 메뉴 불러오기 */
AdminFaculty.prototype.MenuLoad = function ()
{
    var objThis = this;

    var sHtml =
    objThis.MenuUrl

    //메뉴 html 받아오기
    AA.HtmlFileLoad(objThis.MenuUrl
        , function (sHtml)
        {
            //
            var sHtmlTemp = "";
            sHtmlTemp
                += GlobalStatic.DataBind.DataBind(
                    sHtml
                    , ["defult"]
                    , {
                        AdminMenu_SettingData: FS_Url.Admin_SettingData
                        , AdminMenu_BoardMgt: FS_Url.Admin_BoardMgt
                    }
                    , DG_JsDataBind_MatchType.Select)
                    .ResultString;

            //메뉴 표시
            objThis.jsonOpt.MenuArea.html(sHtmlTemp);

            //메뉴 버튼 활성 표시
            $("#" + objThis.jsonOpt.BtnName).addClass("on");
        });
};