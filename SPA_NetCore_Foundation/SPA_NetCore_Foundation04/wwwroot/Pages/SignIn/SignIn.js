
/** 사인인 클래스 */
function SignIn()
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

        //페이지 공통 기능 제거
        Page.Remove();

        //사인 인 인터페이스
        divMain.load(FS_FUrl.SignIn_SignIn
            , function () 
            {
                oThis.SignInAssist
                    = new SignInAssist(
                        {
                            //이메일
                            txtEMail: $("#txtEMail"),
                            //비밀번호
                            pwPassword: $("#pwPassword"),
                            //자동 사인인 여부 저장
                            ckEmailSave: $("#ckEmailSave"),
                            //자동 사인인
                            ckAutoSignIn: $("#ckAutoSignIn"),
                        });
            });
    }
}

/** 사인인 지원 개체 */
SignIn.prototype.SignInAssist = null;


/**
 * 사인인 시도
 */
SignIn.prototype.btnSignIn_onclick = function ()
{
    var objThis = this;
    objThis.SignInAssist.SignIn_onclick();
};
