/**
 * 사이트 전체에서 사인관련 코드들
 */
var GlobalSign = {};

/** 사인인 여부 */
GlobalSign.SignIn = false;
/** 사인인 - 아이디 정보 */
GlobalSign.SignIn_ID = "";
/** 사인인 - 토큰 정보 */
GlobalSign.SignIn_Token = "";

/**
 * 사인인 페이지로 이동
 */
GlobalSign.Move_SignIn = function ()
{
    location.href = FS_Url.SignIn;
};

/**
 * 사인아웃 시도
 */
GlobalSign.Move_SignOut = function ()
{
    //사인아웃 시도
    //location.href = FS_Url.SignIn;
    
    if (false === dgIsObject.IsBoolValue(GlobalSign.SignIn))
    {//사인 아웃이 되어 있음
        alert("사인아웃이 되어 있습니다.");
    }
    else
    {
        //사인 아웃 시도
        $.ajax({
            url: FS_Api.Sign_SignOut,
            type: "PUT",
            data: {
                sToken: GlobalSign.SignIn_Token
            },
            dataType: "text",
            success: function (data) {
                console.log(data);
                GlobalSign.SignIn = false;

                alert("사인아웃 성공 : " + data);

                

                switch (GlobalStatic.SiteType)
                {
                    case 1://어드민 타입
                        //사인인 페이지로 이동
                        location.href = FS_Url.SignIn;
                        break;

                    case 0:
                    default:
                        //UI 갱신
                        SignInInfo.UserInfo_Load();
                        break;
                }
            },
            error: function (error) {
                console.log(error);

                if (error.responseJSON
                        && error.responseJSON.InfoCode) {
                    alert("실패코드 : " + error.responseJSON.InfoCode
                        + "\n " + error.responseJSON.message);
                }
            }
        });        
    }

};

/**
 * 엑세스 토큰 가지고오기
 * @returns {string} 엑세스 토큰
 */
GlobalSign.AccessToken_Get = function ()
{
    return CA.Get(GlobalSign.AccessToken_CookieName);
};

/**
 * 엑세스토큰이 있으면 유저 정보를 갱신한다.
 * @param {function} callback 갱신에 성공하면 할 동작
 */
GlobalSign.AccessTokenToInfo = function (callback) 
{
    //이 프로젝트는 엑세스토큰이 없으니 내용이 필요없다.
    if (typeof callback === "function")
    {
        callback();
    }
};