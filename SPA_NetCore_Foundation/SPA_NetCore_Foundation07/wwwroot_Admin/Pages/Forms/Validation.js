
function Validation()
{
    GlobalStatic.PageType_Now = this.constructor.name;

    var objThis = this;


    //페이지 공통기능 로드
    Page.Load({}, function ()
    {
        Page.divContents.load("/Pages/Forms/Validation.html"
            , function ()
            {
                $.getScript("/plugins/jquery-validation/jquery.validate.min.js"
                    , function (data, textStatus, jqxhr)
                    {
                        ++objThis.ScriptCount;
                        objThis.StartTest();
                    });
                $.getScript("/plugins/jquery-validation/additional-methods.min.js"
                    , function (data, textStatus, jqxhr)
                    {
                        ++objThis.ScriptCount;
                        objThis.StartTest();
                    });
            });
    });
}


/** 불러올 스크립트 개수 */
Flot.prototype.ScriptCount = 0;
/** StartTest 동작 여부 */
Flot.prototype.StartFirst = false;

Flot.prototype.StartTest = function ()
{
    var objThis = this;

    if (2 > objThis.ScriptCount)
    {//로드된 스크립트가 적다
        return;
    }

    if (true === objThis.StartFirst)
    {
        return;
    }

    //스타트 실행
    objThis.StartFirst = true;


    $.validator.setDefaults({
        submitHandler: function ()
        {
            alert("Form successful submitted!");
        }
    });
    $('#quickForm').validate({
        rules: {
            email: {
                required: true,
                email: true,
            },
            password: {
                required: true,
                minlength: 5
            },
            terms: {
                required: true
            },
        },
        messages: {
            email: {
                required: "Please enter a email address",
                email: "Please enter a vaild email address"
            },
            password: {
                required: "Please provide a password",
                minlength: "Your password must be at least 5 characters long"
            },
            terms: "Please accept our terms"
        },
        errorElement: 'span',
        errorPlacement: function (error, element)
        {
            error.addClass('invalid-feedback');
            element.closest('.form-group').append(error);
        },
        highlight: function (element, errorClass, validClass)
        {
            $(element).addClass('is-invalid');
        },
        unhighlight: function (element, errorClass, validClass)
        {
            $(element).removeClass('is-invalid');
        }
    });
};