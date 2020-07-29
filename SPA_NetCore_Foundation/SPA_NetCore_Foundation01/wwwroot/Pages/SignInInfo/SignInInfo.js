
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
    if (true === GlobalSign.SignIn)
    {//사인인 정보가 있음
        SignInInfo.divSignInInfo.load(FS_FUrl.SignInInfo_SignOutHtml
            , function () {
                $("#spanEMail").html(GlobalSign.SignIn_ID);
            });
    }
    else
    {//사인인 정보가 없음
        SignInInfo.divSignInInfo.load(FS_FUrl.SignInInfo_SignInHtml);
    }
};