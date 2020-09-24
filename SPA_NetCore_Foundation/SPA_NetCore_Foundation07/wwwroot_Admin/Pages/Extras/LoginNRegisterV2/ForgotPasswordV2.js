
function ForgotPasswordV2()
{
    GlobalStatic.PageType_Now = this.constructor.name;

    var objThis = this;


    //페이지 공통기능 로드
    Page.Load({}, function ()
    {
        Page.divContents.load("/Pages/Extras/LoginNRegisterV2/ForgotPasswordV2.html"
            , function ()
            {
                //메뉴 활성화
                Page.MenuActive();
            });
    });
}