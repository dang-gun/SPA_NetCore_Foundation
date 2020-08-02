
/*
 * 페이지 공통 기능
 */
var Page = {};

/** 페이지 영역  */
Page.divPage = null;

/** 메인 메뉴 영역 */
Page.divMainMenu = null;
/** 사용자 정보 영역 */
Page.divTop = null;
/** 컨탠츠 영역 */
Page.divContents = null;

/**
 * 페이지를 쓰려면 로드를 해야 한다.
 * @param {json} jsonOption 넘겨줘야할 데이터가 있을때 넘길 옵션값
 * @param {function} callbackFun 로드가 완료되면 호출될 함수
 */
Page.Load = function (jsonOption, callbackFun)
{
    var callbackFun_Backup = callbackFun;

    if (Page.divMainMenu === null) 
    {//페이지가 로드 되지 않았다.

        //페이지 html 로드
        divMain.load(FS_FUrl.Page
            , function ()
            {
                //영역 찾기
                Page.divPage = divMain.find("#divPage");
                Page.divTop = divMain.find("#divTop");
                Page.divMainMenu = divMain.find("#divMainMenu");
                Page.divContents = divMain.find("#divContents");

                AA.HtmlFileLoad(FS_FUrl.Page_Page_Menu
                    , function (html)
                    {
                        //메뉴 바인딩
                        var sHtml
                            = GlobalStatic.DataBind.DataBind_All(
                                html
                                , {
                                    MoveHome: FS_Url.Home,
                                    MoveTest01: FS_Url.Test01,
                                    MoveTest02: FS_Url.Test02
                                })
                                .ResultString;

                        Page.divMainMenu.html(sHtml);
                    });

                //사인인 정보 로드
                SignInInfo.Load();

                callbackFun_Backup();
            });
    }
    else
    {//이미 로드가 되었다.

        //바로 실행
        callbackFun_Backup();
    }
};

/**
 * 페이지를 사용하지 않아야 하는 경우 명시적으로 호출하여 제거 해준다.
 */
Page.Remove = function ()
{
    divMain.html("");

    Page.divMainMenu = null;
    Page.divTopInfo = null;
    Page.divContents = null;
};


/**
 * 페이지 이동
 * @param {string} sUrl 이동할 Url
 */
Page.Move_Page = function (sUrl)
{
    location.href = sUrl;
};

Page.Move_Home = function ()
{
    Page.Move_Page(FS_Url.Home);
};

