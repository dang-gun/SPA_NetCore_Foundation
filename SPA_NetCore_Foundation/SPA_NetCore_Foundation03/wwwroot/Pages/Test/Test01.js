
function Test01()
{
    GlobalStatic.PageType_Now = PageType.Test01;

    var objThis = this;

    //페이지 공통기능 로드
    Page.Load(function ()
    {
        //화면 인터페이스
        Page.DivContents.load("/Pages/Test/Test01.html"
            , function () {
                //데이터 바인드 테스트용 div 찾기
                objThis.DivDataBindTest = $("#divDataBindTest");

                //데이터 바인드에 사용할 리스트 아이템 html 받아오기
                AA.HtmlFileLoad(FS_FUrl.Test_Test01_ListItem
                    , function (html) {
                        objThis.DataBindTest_ListItemHtml = html;
                    }
                    , {}
                );
            });
    });
}

/** 데이터 바인드 테스트용 div */
Test01.prototype.DivDataBindTest = undefined;
/** 데이터 바인드에 사용할 리스트 아이템 html */
Test01.prototype.DataBindTest_ListItemHtml = undefined;

/** 테스트용 바인드 데이터 */
Test01.prototype.arrData = [
    { name: "이름1", age: "10" },
    { name: "이름2", age: "20" },
    { name: "이름3", age: "30" },
    { name: "이름4", age: "40" }
];

/** 데이터 바인드 테스트 */
Test01.prototype.DataBind = function ()
{
    //리스트 html을 임시 저장
    var sHtmlTemp = "";

    for (var i = 0; i < this.arrData.length; ++i)
    {
        var jsonItemData = this.arrData[i];
        sHtmlTemp
            += GlobalStatic.DataBind.DataBind(
                    this.DataBindTest_ListItemHtml
                    , ["defult"]
                    , jsonItemData
                    , DG_JsDataBind_MatchType.Select)
                .ResultString;
    }

    this.DivDataBindTest.html(sHtmlTemp);
};