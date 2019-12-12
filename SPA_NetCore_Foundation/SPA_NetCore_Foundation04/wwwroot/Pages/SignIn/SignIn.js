
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
                //자동 사인인 여부 저장
                oThis.ckEmailSave = $("#ckEmailSave");
                //자동 사인인
                oThis.ckAutoSignIn = $("#ckAutoSignIn");


                //이메일 저장 여부
                var bEmailSave = false;
                if ("true" === $.cookie(GlobalSign.EmailSaveIs_CookieName))
                {
                    //저장된 이메일 정보 로드
                    oThis.txtEMail.val($.cookie(GlobalSign.EmailSave_CookieName));
                    bEmailSave = true;
                }
                //이메일 저장여부
                oThis.ckEmailSave.prop("checked", bEmailSave);


                var bAutoSignIn = false;
                if ("true" === $.cookie(GlobalSign.AutoSignIn_CookieName))
                {
                    bAutoSignIn = true;
                }
                //자동로그인 여부
                oThis.ckAutoSignIn.prop("checked", bAutoSignIn);
                
            });
    }
}

/** dom txtEMail : 이메일 */
SignIn.prototype.txtEMail = null;
/** dom pwPassword : 비밀 번호 */
SignIn.prototype.pwPassword = null;
/** dom ckEmailSave : 이메일 저장 객체 */
SignIn.prototype.ckEmailSave = null;
/** dom pwPassword : 자동 사인인 객체 */
SignIn.prototype.ckAutoSignIn = null;


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

    //이메일 저장 여부
    if (true === objThis.ckAutoSignIn.prop("checked"))
    {//저장 한다.
        //이메일 정보
        CA.Set(GlobalSign.EmailSave_CookieName
            , sEmail
            , CA.SaveType.Year1);
        //이메일 저장 여부
        CA.Set(GlobalSign.EmailSaveIs_CookieName
            , "true"
            , CA.SaveType.Year1);
    }
    else
    {
        //이메일 저장 여부
        CA.Set(GlobalSign.EmailSaveIs_CookieName
            , "false"
            , CA.SaveType.Year1);
    }

    //자동로그인 체크 확인
    if (true === objThis.ckAutoSignIn.prop("checked"))
    {//자동 저장    
        //자동 저장 여부 여부
        CA.Set(GlobalSign.AutoSignIn_CookieName
            , "true"
            , CA.SaveType.Year1);
    }
    else
    {
        //자동 저장 여부 여부
        CA.Set(GlobalSign.AutoSignIn_CookieName
            , "false"
            , CA.SaveType.Year1);
    }


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

                        GlobalSign.SignIn_ID = jsonData.id;
                        GlobalSign.SignIn_Email = sEmail;
                        

                        //엑세스 토큰 저장
                        GlobalSign.AccessToken_Set(jsonData.access_token);


                        //자동로그인 여부에 따른 리플레시 수명 지정
                        if (true === objThis.ckAutoSignIn.prop("checked"))
                        {//자동 저장
                            //리플레시 토큰 저장
                            GlobalSign.RefreshToken_Set(jsonData.refresh_token, true);
                        }
                        else
                        {
                            //리플레시 토큰 임시 저장
                            GlobalSign.RefreshToken_Set(jsonData.refresh_token, false);
                        }


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
    }//end if  
};
