
function About()
{
    GlobalStatic.PageType_Now = PageType.About;

    var objThis = this;

    //페이지 공통기능 로드
    Page.Load(function ()
    {
        //화면 인터페이스
        Page.divContents.load("/Pages/About/About.html"
            , function () {
            });
    });
}
