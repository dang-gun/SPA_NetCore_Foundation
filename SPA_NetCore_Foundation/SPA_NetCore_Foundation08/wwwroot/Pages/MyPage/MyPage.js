
function MyPage()
{
    GlobalStatic.PageType_Now = this.constructor.name;

    //페이지 공통기능 로드
    Page.Load({}, function () {
        //홈 인터페이스
        Page.divContents.load(FS_FUrl.MyPage_MyPageHtml
            , function ()
            {
                var aAdminMenu = $("#aAdminMenu");

                if (ManagementClassType.AdminTest >= GlobalSign.SignIn_MgtClass)
                {//관리자 메뉴 권한이 있다.
                    //링크 생성
                    aAdminMenu.attr("href", FS_Url.Admin_SettingData);
                }
                else
                {
                    //링크 제거
                    aAdminMenu.remove();
                }
            });
    });
}