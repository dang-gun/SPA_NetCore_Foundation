
function Test_Popup()
{
    GlobalStatic.PageType_Now = PageType.Test_Popup;

    var objThis = this;

    //페이지 공통기능 로드
    Page.Load(function ()
    {
        //화면 인터페이스
        Page.divContents.load("/Pages/Test/Test_Popup.html"
            , function () {
            });
    });
}

Test_Popup.prototype.Count = 0;

Test_Popup.prototype.ShowMsg = function ()
{
    var nCount = ++this.Count;


    DG_MessageBox.Show({
        Title: "테스트 메시지 박스 " + nCount,
        Content: "여기는 메시지 박스 출력입니다. ㅇㄻㄴㅇㄻㄴㅇㄻㄴㅇㄹ<br /> asdfasdfasdfdd아찿타토처ㅏ차ㅣ치쳐챠ㅕ처ㅓ어ㅓㅇdjdjdjdjd " + nCount,

        top: 200,
        left: 200,

        ButtonShowType: Number($("#selectButtonShowType").val()),
        BigIconType: Number($("#selectBigIconType").val()),
        ButtonEvent: function (btnType)
        {
            console.log("button : " + btnType);
            DG_Popup.Close();

            var TestContentsHtml = ' \
                <div class="divTitle DG_PopupTitle">버튼 결과! </div> \
                <div class="divContents"> \
                    결과 :'+ btnType + ' <br />\
                    <br />\
                    <br />\
                    <button class="btn btn-danger" onclick="DG_Popup.CloseAll()">All Close</button>\
                    <br />\
                    <button class="btn btn-warning" onclick="DG_Popup.Close()">Close</button>\
                </div>';

            DG_Popup.Show({
                Content: TestContentsHtml,
                ContentCss: "DG_PopupContentCssAdd",

                top: 200,
                left: 200,

                OverlayClick: function (nPopupIndex, divPopupParent)
                {
                    console.log(nPopupIndex + ", " + divPopupParent);
                    DG_Popup.CloseTarget(divPopupParent);
                }
            });
        }
    });
};

Test_Popup.prototype.ShowPopup = function ()
{
    var objThis = this;
    var nCount = ++this.Count;

    var TestContentsHtml = '\
        <div><div><div class="divTitle DG_PopupTitle">생성 순서 : '
        + nCount +
        '</div> \
            <div class="divContents"> \
                내용물<br />\
                내용물2\
                <br />\
                    <button class="btn btn-success" onclick="GlobalStatic.Page_Now.ShowPopup()">Add New Popup!!</button>\
                <br />\
                <br />\
                <br />\
                <br />\
                <button class="btn btn-danger" onclick="DG_Popup.CloseAll();">All Close</button>\
                <br />\
                <button class="btn btn-warning" onclick="DG_Popup.Close();">Close</button>\
            </div></div></div>';

    popTemp = DG_Popup.Show({
        Content: TestContentsHtml,
        ContentCss: "DG_PopupContentCssAdd",

        top: objThis.RandInt(100, 400),
        left: objThis.RandInt(100, 400),

        OverlayClick: function (nPopupIndex, divPopupParent)
        {
            console.log(nPopupIndex + ", " + divPopupParent);
            DG_Popup.CloseTarget(divPopupParent);
        }
    });
};

Test_Popup.prototype.RandInt = function (nMin, nMax)
{
    nMin = Math.ceil(nMin);
    nMax = Math.floor(nMax);
    return Math.floor(Math.random() * (nMax - nMin)) + nMin; 
};