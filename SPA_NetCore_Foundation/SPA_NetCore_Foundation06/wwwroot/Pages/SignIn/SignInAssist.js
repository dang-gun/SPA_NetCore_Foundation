
/**
 * 사인인 클래스
 * @param {any} jsonOption
 */
function SignInAssist(jsonOption)
{
    var objThis = this;
    objThis.Reset(jsonOption);
}

/** 기능 기본 옵션 */
SignInAssist.prototype.OptionDefault = {
    /** dom txtEMail : 이메일 */
    txtEMail: null,
    /** dom pwPassword : 비밀 번호 */
    pwPassword: null,
    /** dom ckEmailSave : 이메일 저장 객체 */
    ckEmailSave: null,
    /** dom pwPassword : 자동 사인인 객체 */
    ckAutoSignIn: null,
};

/** 완성된 옵션 */
SignInAssist.prototype.Option = {};

/**
 * 개체 초기화
 * @param {any} jsonOption
 */
SignInAssist.prototype.Reset = function (jsonOption)
{
    var objThis = this;

    //옵션 합치기
    objThis.Option = Object.assign({}, objThis.Option, jsonOption);

    //이메일 저장 여부
    if ((null !== objThis.Option.txtEMail)
        && (undefined !== objThis.Option.txtEMail))
    {
        var bEmailSave = false;
        if ("true" === $.cookie(GlobalSign.EmailSaveIs_CookieName))
        {
            //저장된 이메일 정보 로드
            objThis.Option.txtEMail.val($.cookie(GlobalSign.EmailSave_CookieName));
            bEmailSave = true;
        }
        //이메일 저장여부
        objThis.Option.ckEmailSave.prop("checked", bEmailSave);
    }


    //자동로그인 여부
    if ((null !== objThis.Option.ckAutoSignIn)
        && (undefined !== objThis.Option.ckAutoSignIn))
    {
        var bAutoSignIn = false;
        if ("true" === $.cookie(GlobalSign.AutoSignIn_CookieName))
        {
            bAutoSignIn = true;
        }
        //자동로그인 여부
        objThis.Option.ckAutoSignIn.prop("checked", bAutoSignIn);
    }

};


/**
 * 사인인 시도
 */
SignInAssist.prototype.SignIn_onclick = function ()
{
    var objThis = this;

    var sEmail = objThis.Option.txtEMail.val();
    var sPW = objThis.Option.pwPassword.val();

    GlobalSign.SignIn = false;

    //이메일 저장 여부
    if (true === objThis.Option.ckAutoSignIn.prop("checked"))
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
    if (true === objThis.Option.ckAutoSignIn.prop("checked"))
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


    if (true === dgIsObject.IsBoolValue(GlobalSign.SignIn))
    {
        //alert("이미 사인인이 되어 있습니다.");
        GlobalStatic.MessageBox_Error(GlobalStatic.Title, "이미 사인인이 되어 있습니다.");
    }
    else if (false === dgIsObject.IsStringNotEmpty(sEmail))
    {
        //alert("이메일을 입력하지 않았습니다.");
        GlobalStatic.MessageBox_Error(GlobalStatic.Title, "이메일을 입력하지 않았습니다.");
    }
    else if (false === dgIsObject.IsStringNotEmpty(sPW))
    {
        //alert("비밀번호를 입력하지 않았습니다.");
        GlobalStatic.MessageBox_Error(GlobalStatic.Title, "비밀번호를 입력하지 않았습니다.");
    }
    else
    {//성공
        AA.put(AA.TokenRelayType.None
            , {
                url: FS_Api.Sign_SignIn
                , data: { sEmail: sEmail, sPW: sPW }
                , success: function (jsonData)
                {
                    console.log(jsonData);

                    if ("0" === jsonData.InfoCode)
                    {//에러 없음
                        GlobalSign.SignIn = true;

                        GlobalSign.SignIn_ID = jsonData.id;
                        GlobalSign.SignIn_Email = sEmail;
                        GlobalSign.SignIn_ViewName = 


                        //엑세스 토큰 저장
                        GlobalSign.AccessToken_Set(jsonData.access_token);


                        //자동로그인 여부에 따른 리플레시 수명 지정
                        if (true === objThis.Option.ckAutoSignIn.prop("checked"))
                        {//자동 저장
                            //리플레시 토큰 저장
                            GlobalSign.RefreshToken_Set(jsonData.refresh_token, true);
                        }
                        else
                        {
                            //리플레시 토큰 임시 저장
                            GlobalSign.RefreshToken_Set(jsonData.refresh_token, false);
                        }


                        //alert("사인 인 성공");
                        GlobalStatic.MessageBox_Info(GlobalStatic.Title, "사인 인 성공");

                        //홈으로 이동
                        Page.Move_Home();
                    }
                    else 
                    {//에러 있음
                        //alert("error code : " + jsonData.InfoCode + "\n"
                        //    + "내용 : " + jsonData.Message);
                        GlobalStatic.MessageBox_Error(GlobalStatic.Title
                            , "error code : " + jsonData.InfoCode + "\n"
                            + "내용 : " + jsonData.Message);
                    }

                }
                , error: function (error) 
                {
                    console.log(error);

                    //alert("알수 없는 오류가 발생했습니다.");
                    GlobalStatic.MessageBox_Error(GlobalStatic.Title, "알수 없는 오류가 발생했습니다.");
                }
            });
    }//end if
};
