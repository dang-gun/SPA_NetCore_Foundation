
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
    //필요에 따라서 외부에서도 호출가능하므로 객체가 있는지 확인해야 한다.
    if (TopInfo.DivTopInfo)
    {
        if (true === dgIsObject.IsBoolValue(GlobalSign.SignIn))
        {//사인인 정보가 있음
            TopInfo.DivTopInfo.load(FS_FUrl.TopInfo_UserInfo_SignOut
                , function () {
                    $("#spanViewName").html(GlobalSign.SignIn_ViewName);
                });
        }
        else {//사인인 정보가 없음
            TopInfo.DivTopInfo.load(FS_FUrl.TopInfo_UserInfo_SignIn);
        }
    }
};