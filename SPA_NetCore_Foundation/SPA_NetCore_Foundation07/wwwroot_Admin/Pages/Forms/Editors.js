
function Editors()
{
    GlobalStatic.PageType_Now = this.constructor.name;

    var objThis = this;


    //페이지 공통기능 로드
    Page.Load({}, function ()
    {
        Page.divContents.load("/Pages/Forms/Editors.html"
            , function ()
            {
                objThis.StartTest();

                //메뉴 활성화
                Page.MenuActive();
            });
    });
}


Editors.prototype.StartTest = function ()
{

    // Summernote
    $('#summernote').summernote()

    // CodeMirror
    CodeMirror.fromTextArea(document.getElementById("codeMirrorDemo"), {
        mode: "htmlmixed",
        theme: "monokai"
    });
};