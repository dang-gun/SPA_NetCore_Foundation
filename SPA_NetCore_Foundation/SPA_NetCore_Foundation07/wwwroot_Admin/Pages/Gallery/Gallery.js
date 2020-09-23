
function Gallery()
{
    GlobalStatic.PageType_Now = this.constructor.name;

    var objThis = this;


    //페이지 공통기능 로드
    Page.Load({}, function ()
    {
        Page.divContents.load("/Pages/Gallery/Gallery.html"
            , function ()
            {
                objThis.StartTest();

                //메뉴 활성화
                Page.MenuActive();
            });
    });
}

Gallery.prototype.StartTest = function ()
{
    var objThis = this;

    $(document).on('click', '[data-toggle="lightbox"]', function (event)
    {
        event.preventDefault();
        $(this).ekkoLightbox({
            alwaysShowClose: true
        });
    });

    $('.filter-container').filterizr({ gutterPixels: 3 });
    $('.btn[data-filter]').on('click', function ()
    {
        $('.btn[data-filter]').removeClass('active');
        $(this).addClass('active');
    });
};