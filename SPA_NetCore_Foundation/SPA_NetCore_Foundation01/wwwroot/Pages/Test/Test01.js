
function Test01()
{
    GlobalStatic.PageType_Now = PageType.Test01;

    //페이지 공통기능 로드
    Page.Load(function ()
    {
        //홈 인터페이스
        Page.DivContents.load("/Pages/Test/Test01.html");
    });
}