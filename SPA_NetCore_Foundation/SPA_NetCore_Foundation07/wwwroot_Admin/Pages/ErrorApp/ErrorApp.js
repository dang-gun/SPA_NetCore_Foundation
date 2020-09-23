
function ErrorApp(sCode)
{
    var objThis = this;

    GlobalStatic.PageType_Now = this.constructor.name;

    //에러 코드 저장
    objThis.Code = sCode;

    //페이지 공통기능 로드
    Page.Load({}, function ()
    {
        Page.divContents.load(FS_FUrl.Error_Home
            , function ()
            {
                objThis.divError = $("#divError");
                objThis.divErrorMore = $("#divErrorMore");

                //페이지 표시
                objThis.SelectPage(objThis.Code);
            });
    });
}

ErrorApp.prototype.Code = "";

ErrorApp.prototype.divError = null;
ErrorApp.prototype.divErrorMore = null;

ErrorApp.prototype.SelectPage = function (sCode)
{
    var objThis = this;

    var sUrl = "";
    switch (sCode)
    {
        case "404":
            sUrl = "/Pages/Error/404.html";
            break;
        default:
            sUrl = "/Pages/Error/ErrorDefult.html";
            break;
    }

    objThis.ChangePage(sUrl);
};

ErrorApp.prototype.ChangePage = function (sUrl)
{
    var objThis = this;

    //데이터 바인드에 사용할 리스트 아이템 html 받아오기
    AA.HtmlFileLoad(sUrl
        , function (html)
        {
            objThis.divErrorMore.html(html);
        }
        , {}
    );
};