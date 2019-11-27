
/** 사인인 클래스 */
function SignIn()
{
    GlobalStatic.PageType_Now = PageType.SignIn;

    if (true === GlobalSign.SignIn)
    {//이미 사인인이 되어있다.
        //홈으로 이동
        Page.Move_Home();
    }
    else
    {
        //내 인스턴스 전달
        var oThis = this;

        //페이지 공통 기능 제거
        Page.Remove();

        //사인 인 인터페이스
        DivMain.load(FS_FUrl.SignIn_SignIn
            , function () {
                //이메일
                oThis.txtEMail = $("#txtEMail");
                //비밀번호
                oThis.pwPassword = $("#pwPassword");
            });
    }
}

/** dom txtEMail : 이메일 */
SignIn.prototype.txtEMail = null;
/** dom pwPassword : 비밀 번호 */
SignIn.prototype.pwPassword = null;


/**
 * 사인인 시도
 */
SignIn.prototype.btnSignIn_onclick = function ()
{
    //내 인스턴스 전달
    var objThis = this;

    var sEmail = this.txtEMail.val();
    var sPW = this.pwPassword.val();

    GlobalSign.SignIn = false;

    if (true === GlobalSign.SignIn)
    {
        alert("이미 사인인이 되어 있습니다.");
    }
    else if ("" === sEmail)
    {
        alert("이메일을 입력하지 않았습니다.");
    }
    else if ("" === sPW)
    {
        alert("비밀번호를 입력하지 않았습니다.");
    }
    else
    {//성공
        AA.put(false
            , {
                url: FS_Api.Sign_SignIn
                , data: { sEmail: sEmail, sPW: sPW }
                , success: function (jsonData) {
                    console.log(jsonData);

                    if ("0" === jsonData.infoCode) {//에러 없음
                        GlobalSign.SignIn = true;
                        GlobalSign.SignIn_token = jsonData.token;
                        GlobalSign.SignIn_ID = sEmail;

                        alert("사인 인 성공");

                        //홈으로 이동
                        Page.Move_Home();
                    }
                    else {//에러 있음
                        alert("error code : " + jsonData.infoCode + "\n"
                            + "내용 : " + jsonData.message);
                    }

                }
                , error: function (error) {
                    console.log(error);

                    alert("알수 없는 오류가 발생했습니다.");
                }
            });
        
    }

    
};
