
function E_commerce()
{
    GlobalStatic.PageType_Now = this.constructor.name;

    var objThis = this;


    //페이지 공통기능 로드
    Page.Load({}, function ()
    {
        Page.divContents.load("/Pages/Pages/E_commerce.html"
            , function ()
            {

                //메뉴 활성화
                Page.MenuActive();
            });
    });
}
