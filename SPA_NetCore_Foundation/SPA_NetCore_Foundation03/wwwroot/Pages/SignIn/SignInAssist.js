
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

                        GlobalSign.SignIn_Token = jsonData.token;
                        GlobalSign.SignIn_ID = sEmail;



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
