
/*
 * 페이지 공통 기능
 */
var Page = {};

/** 메인 메뉴 영역 */
Page.divMainMenu = null;
/** 사용자 정보 영역 */
Page.divTopInfo = null;
/** 컨탠츠 영역 */
Page.divContents = null;
/** 네비게이션 바 */
Page.navbarResponsive = null;

/**
 * 페이지를 쓰려면 로드를 해야 한다.
 * @param {function} callbackFun 로드가 완료되면 호출될 함수
 */
Page.Load = function (callbackFun)
{
    var callbackFun_Backup = callbackFun;

    if (Page.divMainMenu === null) 
    {//페이지가 로드 되지 않았다.

        //페이지 html 로드
        DivMain.load(FS_FUrl.Page
            , function () {
                //영역 찾기
                Page.divMainMenu = DivMain.find("#divMainMenu");
                Page.divTopInfo = DivMain.find("#divTopInfo");
                Page.divContents = DivMain.find("#divContents");
                

                //최상단 정보 출력
                Page.divTopInfo.load(FS_FUrl.TopInfo_TopInfo
                    , function () {
                        TopInfo.Load();
                        Page.navbarResponsive = DivMain.find("#navbarResponsive");
                    });

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
    DivMain.html("");

    Page.divMainMenu = null;
    Page.divTopInfo = null;
    Page.divContents = null;
};



/**
 * 페이지 이동
 * @param {boolean } bSignIn 사인인 확인
 * @param {string} sUrl 이동할 Url
 */
Page.Move_Page = function (bSignIn, sUrl)
{
    //네비바를 닫을지 여부
    var bnavbarResponsive = false;

    if (true === bSignIn)
    {//사인인이 필요하다.
        if (true === GlobalSign.SignIn)
        {
            bnavbarResponsive = true;
            location.href = sUrl;
        }
        else
        {//사인인이 안되있음
            GlobalStatic.MessageBox_Info("사인인이 필요합니다.");
        }
    }
    else
    {//사인인이 필요없다.
        bnavbarResponsive = true;
        location.href = sUrl;
    }

    if (true === bnavbarResponsive)
    {
        if (true === Page.navbarResponsive.hasClass("show"))
        {
            $("button.navbar-toggler.navbar-toggler-right").click();
        }
        //Page.navbarResponsive.removeClass("show");
    }

};

Page.Move_Home = function ()
{
    Page.Move_Page(false, FS_Url.Home);
};
