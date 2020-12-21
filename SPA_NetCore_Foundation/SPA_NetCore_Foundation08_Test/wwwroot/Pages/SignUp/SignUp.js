
/** 사인인 클래스 */
function SignUp()
{
    GlobalStatic.PageType_Now = this.constructor.name;

    if (true === dgIsObject.IsBoolValue(GlobalSign.SignIn))
    {//이미 사인인이 되어있다.
        //홈으로 이동
        Page.Move_Home();
    }
    else
    {
        //내 인스턴스 전달
        var oThis = this;

        //사인 인 인터페이스
        divMain.load(FS_FUrl.SignUp_SignUp
            , function () 
                {
                    oThis.txtEMail = $("#txtEMail");
                    oThis.txtViewName = $("#txtViewName");
                    oThis.pwPassword = $("#pwPassword");
                    oThis.pwPasswordRe = $("#pwPasswordRe");
                });
    }
}

/** dom txtEMail : 로그인 이메일 */
SignUp.prototype.txtEMail = null;
/** dom txtViewName : 뷰네임 */
SignUp.prototype.txtViewName = null;
/** dom pwPassword : 비밀번호 */
SignUp.prototype.pwPassword = null;
/** dom pwPasswordRe : 비밀번호 재입력 */
SignUp.prototype.pwPasswordRe = null;

/** 이 클래스 제목 */
SignUp.prototype.Title = "회원 가입";

/** 사인 이메일 중복 확인 */
SignUp.prototype.OnClick_SignEmailCheck = function ()
{
    var oThis = this;
    var sEmail = oThis.txtEMail.val();

    if (false === dgIsObject.IsStringNotEmpty(sEmail))
    {
        GlobalStatic.MessageBox_Error("", "아이디를 입력하지 않았습니다.");
    }
    else
    {
        AA.get(AA.TokenRelayType.None
            , {
                url: FS_Api.Sign_SignEmailCheck
                , data: { sEmail: sEmail}
                , success: function (jsonData)
                {
                    if ("0" === jsonData.InfoCode)
                    {//에러 없음
                        GlobalStatic.MessageBox_Info(""
                            , "사용할 수 있는 아이디입니다.");
                    }
                    else
                    {//에러 있음
                        GlobalStatic.MessageBox_Error("",
                            "실패코드 : " + jsonData.InfoCode + "<br /> "
                            + jsonData.Message);
                    }

                }
                , error: function (jqXHR, textStatus, errorThrown)
                {
                    GlobalStatic.MessageBox_Error("", "알수 없는 오류가 발생했습니다.");
                }
            });
    }//end if
};

/** 사인 뷰네임 중복 확인 */
SignUp.prototype.OnClick_SignNameCheck = function () 
{
    var oThis = this;
    var sName = oThis.txtViewName.val();

    if (false === dgIsObject.IsStringNotEmpty(sName)) 
    {
        GlobalStatic.MessageBox_Error("", "닉네임을 입력하지 않았습니다.");
    }
    else
    {
        AA.get(AA.TokenRelayType.None
            , {
                url: FS_Api.Sign_ViewNameCheck
                , data: { sViewName: sName }
                , success: function (jsonData)
                {
                    if ("0" === jsonData.InfoCode) 
                    {//에러 없음
                        GlobalStatic.MessageBox_Info(""
                            , "사용할 수 있는 닉네임입니다.");
                    }
                    else 
                    {//에러 있음
                        GlobalStatic.MessageBox_Error("",
                            "실패코드 : " + jsonData.InfoCode + "<br /> "
                            + jsonData.Message);
                    }

                }
                , error: function (jqXHR, textStatus, errorThrown)
                {
                    GlobalStatic.MessageBox_Error("", "알수 없는 오류가 발생했습니다.");
                }
            });
    }//end if
};

/** 가입 시작 */
SignUp.prototype.OnClick_SingUp = function ()
{
    var oThis = this;

    var jsonData = {
        sEmail: dgIsObject.IsStringValue(oThis.txtEMail.val()),
        sViewName: dgIsObject.IsStringValue(oThis.txtViewName.val()),
        sPassword: dgIsObject.IsStringValue(oThis.pwPassword.val()),
        sPassword2: dgIsObject.IsStringValue(oThis.pwPasswordRe.val()),
    };


    if ("" === jsonData.sEmail)
    {
        GlobalStatic.MessageBox_Error(oThis.Title, "아이디를 입력해주세요");
    }
    else if ("" === jsonData.sViewName)
    {
        GlobalStatic.MessageBox_Error(oThis.Title, "닉네임을 입력해주세요");
    }
    else if ("" === jsonData.sPassword)
    {
        GlobalStatic.MessageBox_Error(oThis.Title, "비밀번호를 입력해주세요");
    }
    else if ("" === jsonData.sPassword2)
    {
        GlobalStatic.MessageBox_Error(oThis.Title, "비밀번호 확인을 입력해주세요.");
    }
    else if (jsonData.sPassword !== jsonData.sPassword2)
    {
        GlobalStatic.MessageBox_Error(oThis.Title, "비밀번호 확인이 틀렸습니다.");
    }
    else
    {
        AA.post(AA.TokenRelayType.None
            , {
                url: FS_Api.Sign_SignUp
                , data: jsonData
                , success: function (jsonData)
                {
                    if ("0" === jsonData.InfoCode)
                    {//에러 없음
                        Page.Move_Home();
                        GlobalStatic.MessageBox_Info(oThis.Title
                            , "가입이 완료되었습니다.<br />로그인을 해주세요.");
                    }
                    else
                    {//에러 있음
                        GlobalStatic.MessageBox_Error(oThis.Title, jsonData.Message);
                    }

                }
                , error: function (jqXHR, textStatus, errorThrown)
                {
                    GlobalStatic.MessageBox_Error(oThis.Title
                        , "알수 없는 오류가 발생했습니다.");
                }
            });
    }
};
