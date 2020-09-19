
/*
 * 탑인포 기능
 */
var SignInInfo = {};

/** 사인인 인포 영역 */
SignInInfo.divSignInInfo = null;

SignInInfo.Load = function ()
{
    SignInInfo.divSignInInfo = Page.divPage.find("#divSignInInfo");

    //유저 정보 요청
    SignInInfo.UserInfo_Load();
};

/**
 * 유저 정보 로드
 */
SignInInfo.UserInfo_Load = function ()
{
    if (SignInInfo.divSignInInfo)
    {
        if (true === GlobalSign.SignIn)
        {//사인인 정보가 있음

            SignInInfo.divSignInInfo.load(FS_FUrl.SignInInfo_SignOutHtml
                , function ()
                {
                    $("#aMyPage").attr("href", FS_Url.MyPage);
                    $("#spanEMail").html(GlobalSign.SignIn_ID);
                });
        }
        else
        {//사인인 정보가 없음
            //정보 없데이트
            SignInInfo.divSignInInfo.load(FS_FUrl.SignInInfo_SignInHtml);



            //현제 페이지 검사
            if (true === app_Assist.Path_NowSignIn)
            {//로그인 필수 페이지
                //이전 페이지도 페이지 검사
                if (true === app_Assist.Path_PreviousSignIn)
                {//로그인 필수 페이지
                    //홈으로 보낸다.
                    Page.Move_Home();
                }
                else
                {//로그인 비필수
                    //이전 페이지로 이동
                    Page.Move_Page(app_Assist.Path_Previous);
                }
            }
            else
            {//로그인 비필수
                //그대로 둔다.
            }
        }
    }
    
};



/** 네비에서 aNavInfo 클릭시 이벤트 */
SignInInfo.aNavInfoClick = function ()
{
    if (false === dgIsObject.IsBoolValue(GlobalSign.SignIn))
    {//사인인 정보가 없다.
        //로그인으로 넘긴다
        GlobalSign.Move_SignIn();
    }
};