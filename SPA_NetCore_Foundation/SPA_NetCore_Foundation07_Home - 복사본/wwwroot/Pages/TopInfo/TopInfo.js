
/*
 * 탑인포 기능
 */
var TopInfo = {};

/** 네비 인포 */
TopInfo.aNavInfo = null;
/* 탑 인포 영역 */
TopInfo.divTopInfo = null;



TopInfo.Load = function ()
{
    //영역 찾기
    TopInfo.aNavInfo = Page.divTopInfo.find("#aNavInfo");
    TopInfo.divTopInfo = Page.divTopInfo.find("#divTopInfo");

    //유저 정보 요청
    TopInfo.UserInfo_Load();
};

/**
 * 유저 정보 로드
 */
TopInfo.UserInfo_Load = function ()
{
    //필요에 따라서 외부에서도 호출가능하므로 객체가 있는지 확인해야 한다.
    if (TopInfo.divTopInfo)
    {
        if (true === dgIsObject.IsBoolValue(GlobalSign.SignIn))
        {//사인인 정보가 있음
            //표시 이름 표시
            TopInfo.aNavInfo.html(GlobalSign.SignIn_ViewName);
            //html 로드
            TopInfo.divTopInfo.load(FS_FUrl.TopInfo_UserInfo_SignOut
                , function () {
                    $("#spanViewName").html(GlobalSign.SignIn_ViewName);
                });
        }
        else 
        {//사인인 정보가 없음
            TopInfo.aNavInfo.html("Sign In");
            //TopInfo.divTopInfo.load(FS_FUrl.TopInfo_UserInfo_SignIn);
        }
    }
};

/** 네비에서 aNavInfo 클릭시 이벤트 */
TopInfo.aNavInfoClick = function ()
{
    if (false === dgIsObject.IsBoolValue(GlobalSign.SignIn))
    {//사인인 정보가 없다.
        //로그인으로 넘긴다
        GlobalSign.Move_SignIn();
    }
};