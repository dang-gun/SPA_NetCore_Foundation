/**
 * 홈 클래스
 * @param {any} nViewType
 */
function Home(nViewType)
{
    GlobalStatic.PageType_Now = this.constructor.name;

    var objThis = this;
    var nViewTypeTemp = nViewType;
    objThis.ViewType = nViewTypeTemp;

    //불러올 html url
    var sHtmlUrl = "";

    switch (nViewType)
    {
        case 2:
            sHtmlUrl = FS_FUrl.Home_Home2;
            break;
        case 3:
            sHtmlUrl = FS_FUrl.Home_Home3;
            break;

        case 0:
        case 1:
        default:
            sHtmlUrl = FS_FUrl.Home_Home;
            break;
    }

    //페이지 공통기능 로드
    Page.Load({}, function () 
    {
        //홈 인터페이스
        Page.divContents.load(sHtmlUrl, function ()
        {
        });

        //메뉴 활성화
        Page.MenuActive(nViewTypeTemp);
    });
}

/** 홈 보기 타입 */
Home.prototype.ViewType = 0;