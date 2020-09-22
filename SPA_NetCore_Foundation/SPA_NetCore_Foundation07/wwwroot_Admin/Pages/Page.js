
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
                                    MoveCharts_ChartJS: FS_Url.Charts_ChartJS,
                                    MoveCharts_Flot: FS_Url.Charts_Flot,
                                    MoveCharts_Inline: FS_Url.Charts_Inline,

                                    MoveForms_GeneralElements: FS_Url.Forms_GeneralElements,
                                    MoveForms_AdvancedElements: FS_Url.Forms_AdvancedElements,
                                    MoveForms_Editors: FS_Url.Forms_Editors,
                                    MoveForms_Validation: FS_Url.Forms_Validation,

                                    MoveHome: FS_Url.Home,
                                    MoveHome2: FS_Url.Home2,
                                    MoveHome3: FS_Url.Home3,

                                    MoveTables_SimpleTablesn: FS_Url.Tables_SimpleTables,
                                    MoveTables_DataTables: FS_Url.Tables_DataTables,
                                    MoveTables_jsGrid: FS_Url.Tables_jsGrid,

                                    MoveUiElements_General: FS_Url.UiElements_General,
                                    MoveUiElements_Icons: FS_Url.UiElements_Icons,
                                    MoveUiElements_Buttons: FS_Url.UiElements_Buttons,
                                    MoveUiElements_Sliders: FS_Url.UiElements_Sliders,
                                    MoveUiElements_ModalsNAlerts: FS_Url.UiElements_ModalsNAlerts,
                                    MoveUiElements_NavbarNTabs: FS_Url.UiElements_NavbarNTabs,
                                    MoveUiElements_Timeline: FS_Url.UiElements_Timeline,
                                    MoveUiElements_Ribbons: FS_Url.UiElements_Ribbons,

                                    MoveWidgets: FS_Url.Widgets,

                                    MoveTest01: FS_Url.Test01,
                                    MoveTest02: FS_Url.Test02
                                })
                                .ResultString;

                        //메뉴 바인딩
                        Page.divMainMenu.html(sHtml);

                        //메뉴 이벤트 연결
                        Page.MenuOpenEvent_On();

                        //메뉴 활성화 시도
                        Page.MenuActive();

                        //AdminLTE3 공통 js
                        $.getScript("/dist/js/adminlte.js"
                            , function (data, textStatus, jqxhr)
                            {
                            });
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

/** 
 *  메뉴 오픈 이벤트 연결 
 *  원래는 AdminLTE3 공통 js에서 연결이 되야 하는데....
 *  이유는 알 수 없지만 동작을 하지 않아 별도로 만들었다.
 *  메뉴가 완성되면 호출해 준다.
 */
Page.MenuOpenEvent_On = function ()
{
    //이벤트 연결할 대상 찾기
    var aTargets = $(".nav-sidebar a.nav-link[href='#']");


    //이벤트 연결
    aTargets.click(function (event)
    {
        event.preventDefault();
        event.stopPropagation();

        var domParent = $(this).parent("li");

        //메뉴가 열려있는지 판단한다.
        var bOpen = domParent.hasClass("menu-open");

        if (true === bOpen)
        {//열려있다,
            //닫아 준다.
            Page.MenuOpenEvent_Close(event, domParent);
        }
        else
        {//닫쳐있다.
            //열어 준다.
            Page.MenuOpenEvent_Open(event, domParent);
        }
        
    });
};

/**
 * 메뉴 열기 동작
 * @param {any} event
 * @param {any} domParent
 */
Page.MenuOpenEvent_Open = function (event, domParent)
{
    var domTarget = domParent;

    //메뉴가 열려있는지 판단한다.
    var bOpen = domParent.hasClass("menu-open");

    if (false === bOpen)
    {//닫쳐 있다.
        //열어준다.
        domTarget.addClass("menu-open");
        var nHeight = domTarget.height();
        domTarget.height(40);

        domTarget
            .animate(
                {
                    height: nHeight
                },
                {
                    complete: function ()
                    {
                        domTarget.addClass("menu-open");
                        domTarget.height("auto");
                    }
                });
    }//end if
};

/**
 * 메뉴 닫기 동작
 * @param {any} event
 * @param {any} domParent
 */
Page.MenuOpenEvent_Close = function (event, domParent)
{
    var domTarget = domParent;

    //메뉴가 열려있는지 판단한다.
    var bOpen = domParent.hasClass("menu-open");

    if (true === bOpen)
    {//열려 있다.
        //닫아 준다.
        domTarget
            .animate(
                {
                    height: 40
                },
                {
                    complete: function ()
                    {
                        domTarget.removeClass("menu-open");
                        domTarget.height("auto");
                    }
                });
    }
};

/** 메뉴 모두 열기 */
Page.MenuOpenEvent_OpenAll = function ()
{
    //이벤트 연결할 대상 찾기
    var aTargets = $(".nav-sidebar a.nav-link[href='#']");

    for (var i = 0; i < aTargets.length; ++i)
    {
        var domParent = $(aTargets[i]).parent("li");
        //열어 준다.
        Page.MenuOpenEvent_Open(null, domParent);
    }
};

/** 메뉴 모두 닫기 */
Page.MenuOpenEvent_OpenClose = function ()
{
    //이벤트 연결할 대상 찾기
    var aTargets = $(".nav-sidebar a.nav-link[href='#']");

    for (var i = 0; i < aTargets.length; ++i)
    {
        var domParent = $(aTargets[i]).parent("li");
        //열어 준다.
        Page.MenuOpenEvent_Close(null, domParent);
    }
};


/**
 * 메뉴 활성화 요청
 * @param {any} objTemp
 */
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