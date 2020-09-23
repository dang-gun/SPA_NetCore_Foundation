
function Widgets()
{
    GlobalStatic.PageType_Now = this.constructor.name;

    var objThis = this;

    //페이지 공통기능 로드
    Page.Load({}, function () 
    {
        //홈 인터페이스
        Page.divContents.load("/Pages/Widgets/Widgets.html"
            , function ()
            {

                //메뉴 활성화
                Page.MenuActive();
            });

        //메뉴 활성화
        Page.MenuActive();
    });
}