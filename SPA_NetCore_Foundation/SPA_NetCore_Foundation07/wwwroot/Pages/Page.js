
/*
 * 페이지 공통 기능
 */
var Page = {};

/** 메인 메뉴 영역 */
Page.DivMainMenu = null;
/** 사용자 정보 영역 */
Page.DivTopInfo = null;
/** 컨탠츠 영역 */
Page.DivContents = null;

/**
 * 페이지를 쓰려면 로드를 해야 한다.
 * @param {function} callbackFun 로드가 완료되면 호출될 함수
 */
Page.Load = function (callbackFun)
{
    var callbackFun_Backup = callbackFun;

    if (Page.DivMainMenu === null) {//페이지가 로드 되지 않았다.

        //페이지 html 로드
        DivMain.load(FS_FUrl.Page
            , function () {
                //영역 찾기
                Page.DivMainMenu = DivMain.find("#divMainMenu");
                Page.DivTopInfo = DivMain.find("#divTopInfo");
                Page.DivContents = DivMain.find("#divContents");

                //최상단 정보 출력
                Page.DivTopInfo.load(FS_FUrl.TopInfo_TopInfo
                    , function () {
                        TopInfo.Load();
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

    Page.DivMainMenu = null;
    Page.DivTopInfo = null;
    Page.DivContents = null;
};



/**
 * 페이지 이동
 * @param {boolean } bSignIn 사인인 확인
 * @param {string} sUrl 이동할 Url
 */
Page.Move_Page = function (bSignIn, sUrl)
{
    if (true === bSignIn)
    {//사인인이 필요하다.
        if (true === GlobalSign.SignIn)
        {
            location.href = sUrl;
        }
        else
        {//사인인이 안되있음
            alert("사인인이 필요합니다.");
        }
    }
    else
    {//사인인이 필요없다.
        location.href = sUrl;
    }
};

Page.Move_Admin = function ()
{
    location.href = FS_Url.Admin;
};

Page.Move_Home = function ()
{
    location.href = FS_Url.Home;
};

Page.Move_MyPage = function ()
{
    location.href = FS_Url.MyPage;
};


Page.Move_Test01 = function ()
{
    location.href = FS_Url.Test01;
};

Page.Move_Test02 = function () {
    location.href = FS_Url.Test02;
};