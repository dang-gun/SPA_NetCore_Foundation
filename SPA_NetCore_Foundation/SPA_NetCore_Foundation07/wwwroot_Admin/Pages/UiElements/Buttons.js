
function Buttons()
{
    GlobalStatic.PageType_Now = this.constructor.name;

    var objThis = this;


    //페이지 공통기능 로드
    Page.Load({}, function ()
    {
        Page.divContents.load("/Pages/UiElements/Buttons.html"
            , function ()
            {
            });
    });
}