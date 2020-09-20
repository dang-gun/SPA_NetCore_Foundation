/**
 * 홈 클래스
 * @param {any} nType 
 */
function Home(nType)
{
    GlobalStatic.PageType_Now = this.constructor.name;

    //불러올 html url
    var sHtmlUrl = "";
    //불러올 Script url
    var sScriptUrl = "";

    switch (nType)
    {
        case 2:
            sHtmlUrl = FS_FUrl.Home_Home2;
            sScriptUrl = "/dist/js/pages/dashboard2.js";
            break;
        case 3:
            sHtmlUrl = FS_FUrl.Home_Home3;
            sScriptUrl = "/dist/js/pages/dashboard3.js";
            break;

        case 0:
        case 1:
        default:
            sHtmlUrl = FS_FUrl.Home_Home;
            sScriptUrl = "/dist/js/pages/dashboard.js";
            break;
    }

    //페이지 공통기능 로드
    Page.Load({}, function () 
    {
        //홈 인터페이스
        Page.divContents.load(sHtmlUrl, function ()
        {
            $.getScript(sScriptUrl
                , function (data, textStatus, jqxhr)
                {
                });
        });
    });
}