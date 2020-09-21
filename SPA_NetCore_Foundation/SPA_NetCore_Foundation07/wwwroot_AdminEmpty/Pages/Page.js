
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
            , function () {
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
                                    MoveHome2: FS_Url.Home2,
                                    MoveHome3: FS_Url.Home3,

                                    MoveTest01: FS_Url.Test01,
                                    MoveTest02: FS_Url.Test02
                                })
                                .ResultString;

                        //메뉴 바인딩
                        Page.divMainMenu.html(sHtml);

                        //메뉴 활성화 시도
                        Page.MenuActive();
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
    Page.divTop = null;
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


Page.MenuActive = function (objTemp)
{
    //매뉴 아이디 규칙
    //aMenu + 클래스명
    var sID = "";

    //이외의 이름은 별도 처리한다.
    switch (GlobalStatic.PageType_Now)
    {
        case "Home"://홈
            {
                sID = "aMenu" + GlobalStatic.PageType_Now;

                //뷰타입 판단
                var nViewType = 0;
                if (undefined === objTemp
                    || null === objTemp)
                {//넘어온 옵션이 없다.
                    //글로벌 개체를 사용한다.
                    nViewType = GlobalStatic.Page_Now.ViewType;
                }
                else
                {
                    nViewType = dgIsObject.IsIntValue(objTemp);
                }

                //홈은 뷰타입에 따라 이름이 다르다.
                switch (nViewType)
                {
                    case 2:
                        sID += "2";
                        break;
                    case 3:
                        sID += "3";
                        break;

                    case 1:
                    default:
                        break;
                }
            }
            break;

        default:
            sID = "aMenu" + GlobalStatic.PageType_Now;
            break;
    }//end switch (GlobalStatic.PageType_Now)



    //대상에 엑티브 해주기
    if ("" !== sID)
    {
        //기존 엑티브 제거
        var arrDom = $("a.nav-link.active");
        arrDom.removeClass("active");

        //대상에 엑티브 추가
        $("#" + sID).addClass("active");
    }//end if 
};