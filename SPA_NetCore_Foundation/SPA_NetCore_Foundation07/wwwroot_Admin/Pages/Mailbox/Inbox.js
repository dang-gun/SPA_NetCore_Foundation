
function Inbox()
{
    GlobalStatic.PageType_Now = this.constructor.name;

    var objThis = this;


    //페이지 공통기능 로드
    Page.Load({}, function ()
    {
        Page.divContents.load("/Pages/Mailbox/Inbox.html"
            , function ()
            {
                objThis.StartTest();

                //메뉴 활성화
                Page.MenuActive();
            });
    });
}

Inbox.prototype.StartTest = function ()
{
    var objThis = this;

    //Enable check and uncheck all functionality
    $('.checkbox-toggle').click(function ()
    {
        var clicks = $(this).data('clicks')
        if (clicks)
        {
            //Uncheck all checkboxes
            $('.mailbox-messages input[type=\'checkbox\']').prop('checked', false);
            $('.checkbox-toggle .far.fa-check-square').removeClass('fa-check-square').addClass('fa-square');
        } else
        {
            //Check all checkboxes
            $('.mailbox-messages input[type=\'checkbox\']').prop('checked', true);
            $('.checkbox-toggle .far.fa-square').removeClass('fa-square').addClass('fa-check-square');
        }
        $(this).data('clicks', !clicks);
    });

    //Handle starring for font awesome
    $('.mailbox-star').click(function (e)
    {
        e.preventDefault();
        //detect type
        var $this = $(this).find('a > i');
        var fa = $this.hasClass('fa');

        //Switch states
        if (fa)
        {
            $this.toggleClass('fa-star');
            $this.toggleClass('fa-star-o');
        }
    })
};