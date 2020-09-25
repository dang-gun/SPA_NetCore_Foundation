
function SimpleSearch()
{
    GlobalStatic.PageType_Now = this.constructor.name;

    var objThis = this;


    //페이지 공통기능 로드
    Page.Load({}, function ()
    {
        Page.divContents.load("/Pages/Search/SimpleSearch.html"
            , function ()
            {
                //메뉴 활성화
                Page.MenuActive();
            });
    });
}

/**
 * 검색
 * @param {any} sKeyword
 */
SimpleSearch.prototype.SearchResult = function ()
{
    var sKeyword = $("#searchKeyword").val();

    //임시로 html 출력
    Page.divContents.load("/Pages/Search/SimpleSearch_Results.html"
        , function ()
        {
        });
};