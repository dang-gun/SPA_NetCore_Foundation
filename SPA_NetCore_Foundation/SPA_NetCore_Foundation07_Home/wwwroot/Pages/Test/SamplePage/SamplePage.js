
function SamplePage()
{
    GlobalStatic.PageType_Now = PageType.SamplePage;

    var objThis = this;

    //페이지 공통기능 로드
    Page.Load(function ()
    {
        //화면 인터페이스
        Page.divContents.load(FS_FUrl.Test_SamplePage_SamplePageHtml
            , function () {
                //데이터 바인드 테스트용 div 찾기
                objThis.divSamplePage = $("#divSamplePage");

                objThis.SelectPage("portfolio01");
            });
    });
}

/** 출력 영역 */
SamplePage.prototype.divSamplePage = null;

SamplePage.prototype.SelectPage = function (sType)
{
    var sUrl = "";
    switch (sType)
    {
        case "portfolio01":
            sUrl = "/Pages/Test/SamplePage/Portfolio/portfolio01.html";
            break;
        case "portfolio02":
            sUrl = "/Pages/Test/SamplePage/Portfolio/portfolio02.html";
            break;
        case "portfolio03":
            sUrl = "/Pages/Test/SamplePage/Portfolio/portfolio03.html";
            break;
        case "portfolio04":
            sUrl = "/Pages/Test/SamplePage/Portfolio/portfolio04.html";
            break;
        case "portfolioItem":
            sUrl = "/Pages/Test/SamplePage/Portfolio/portfolioItem.html";
            break;

        case "BlogHome01":
            sUrl = "/Pages/Test/SamplePage/Blog/blog-home-1.html";
            break;
        case "BlogHome02":
            sUrl = "/Pages/Test/SamplePage/Blog/blog-home-2.html";
            break;
        case "BlogPost":
            sUrl = "/Pages/Test/SamplePage/Blog/blog-post.html";
            break;

        case "FullWidthPage":
            sUrl = "/Pages/Test/SamplePage/Other/full-width.html";
            break;
        case "SidebarPage":
            sUrl = "/Pages/Test/SamplePage/Other/sidebar.html";
            break;
        case "FAQ":
            sUrl = "/Pages/Test/SamplePage/Other/faq.html";
            break;
        case "404":
            sUrl = "/Pages/Test/SamplePage/Other/404.html";
            break;
        case "PricingTable":
            sUrl = "/Pages/Test/SamplePage/Other/pricing.html";
            break;
    }

    this.ChangePage(sUrl);
};

SamplePage.prototype.ChangePage = function (sUrl)
{
    var objThis = this;

    //데이터 바인드에 사용할 리스트 아이템 html 받아오기
    AA.HtmlFileLoad(sUrl
        , function (html)
        {
            objThis.divSamplePage.html(html);
        }
        , {}
    );
};