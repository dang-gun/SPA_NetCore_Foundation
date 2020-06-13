
/*
 * 탑인포 기능
 */
var TopInfo = {};

TopInfo.divTopLeft = null;
TopInfo.divTopInfo = null;

TopInfo.Load = function ()
{
    TopInfo.divTopLeft = Page.divInfo.find("#divTopLeft");
    TopInfo.divTopInfo = Page.divInfo.find("#divTopInfo");

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
        TopInfo.divTopInfo.load(FS_FUrl.TopInfo_UserInfo_SignOut
            , function () {
                $("#spanEMail").html(GlobalSign.SignIn_ID);
            });
    }
    else
    {//사인인 정보가 없음
        TopInfo.divTopInfo.load(FS_FUrl.TopInfo_UserInfo_SignIn);
    }
};