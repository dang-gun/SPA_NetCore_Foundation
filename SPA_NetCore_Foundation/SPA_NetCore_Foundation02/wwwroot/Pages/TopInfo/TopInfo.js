
/*
 * 탑인포 기능
 */
var TopInfo = {};

TopInfo.DivTopLeft = null;
TopInfo.DivTopInfo = null;

TopInfo.Load = function ()
{
    TopInfo.DivTopLeft = Page.divTopInfo.find("#divTopLeft");
    TopInfo.DivTopInfo = Page.divTopInfo.find("#divTopInfo");

    //유저 정보 요청
    TopInfo.UserInfo_Load();
};

/**
 * 유저 정보 로드
 */
TopInfo.UserInfo_Load = function ()
{
    if (true === GlobalSign.SignIn)
    {//사인인 정보가 있음
        TopInfo.DivTopInfo.load(FS_FUrl.TopInfo_UserInfo_SignOut
            , function () {
                $("#spanEMail").html(GlobalSign.SignIn_ID);
            });
    }
    else
    {//사인인 정보가 없음
        TopInfo.DivTopInfo.load(FS_FUrl.TopInfo_UserInfo_SignIn);
    }
};