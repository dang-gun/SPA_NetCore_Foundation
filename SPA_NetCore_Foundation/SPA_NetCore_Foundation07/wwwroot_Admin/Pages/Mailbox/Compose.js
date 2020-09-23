
function Compose()
{
    GlobalStatic.PageType_Now = this.constructor.name;

    var objThis = this;


    //페이지 공통기능 로드
    Page.Load({}, function ()
    {
        Page.divContents.load("/Pages/Mailbox/Compose.html"
            , function ()
            {
                objThis.StartTest();

                //메뉴 활성화
                Page.MenuActive();
            });
    });
}

Compose.prototype.StartTest = function ()
{
    var objThis = this;

    //Add text editor
    $('#compose-textarea').summernote()
};