
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
                $.getScript("/plugins/codemirror/codemirror.js"
                    , function (data, textStatus, jqxhr)
                    {
                        ++objThis.ScriptCount;
                        objThis.StartTest();
                    });
                $.getScript("/plugins/codemirror/mode/css/css.js"
                    , function (data, textStatus, jqxhr)
                    {
                        ++objThis.ScriptCount;
                        objThis.StartTest();
                    });
                $.getScript("/plugins/codemirror/mode/xml/xml.js"
                    , function (data, textStatus, jqxhr)
                    {
                        ++objThis.ScriptCount;
                        objThis.StartTest();
                    });
                $.getScript("/plugins/codemirror/mode/htmlmixed/htmlmixed.js"
                    , function (data, textStatus, jqxhr)
                    {
                        ++objThis.ScriptCount;
                        objThis.StartTest();
                    });
            });
    });
}


/** 불러올 스크립트 개수 */
Editors.prototype.ScriptCount = 0;
/** StartTest 동작 여부 */
Editors.prototype.StartFirst = false;

Editors.prototype.StartTest = function ()
{

    if (4 > objThis.ScriptCount)
    {//로드된 스크립트가 적다
        return;
    }

    if (true === objThis.StartFirst)
    {
        return;
    }

    //스타트 실행
    objThis.StartFirst = true;



    // Summernote
    $('#summernote').summernote()

    // CodeMirror
    CodeMirror.fromTextArea(document.getElementById("codeMirrorDemo"), {
        mode: "htmlmixed",
        theme: "monokai"
    });
};