/**
 * 사이트 전체에서 사인관련 코드들
 */
var GlobalSign = {};

/** 사인인 여부 */
GlobalSign.SignIn = false;
/** 사인인 - 이메일 정보 */
GlobalSign.SignIn_Email = "";
/** 사인인 - 아이디 정보 */
GlobalSign.SignIn_ID = 0;
/** 사인인 - 표시 이름 */
GlobalSign.SignIn_ViewName = "";
/** 사인인 - 가지고 있는 관리 등급 : ManagementClassType 참고 */
GlobalSign.SignIn_MgtClass = "";


/** 이메일 저장 - 쿠키용 이름 */
GlobalSign.EmailSave_CookieName = "spa_EmailSave";
/** 이메일 저장 여부 - 쿠키용 이름 */
GlobalSign.EmailSaveIs_CookieName = "spa_EmailSaveIs";
/** 자동 사인인 여부 - 쿠키용 이름 */
GlobalSign.AutoSignIn_CookieName = "spa_AutoSignIn";


/** 엑세스 토큰 - 쿠키용 이름 */
GlobalSign.AccessToken_CookieName = "spa_AccessToken";
/** 리플레시 토큰 - 쿠키용 이름 */
GlobalSign.RefreshToken_CookieName = "spa_RefreshToken";



/**
 * 엑세스 토큰 가지고오기
 * @returns {string} 엑세스 토큰
 */
GlobalSign.AccessToken_Get = function () 
{
    return CA.Get(GlobalSign.AccessToken_CookieName);
};
/**
 * 엑세스 토큰 저장하기
 * @param {string} sAccessToken 저장할 엑세스 토큰
 */
GlobalSign.AccessToken_Set = function (sAccessToken)
{
    //토큰 저장 시도
    CA.Set(GlobalSign.AccessToken_CookieName
        , sAccessToken
        , CA.SaveType.Default);
};

/** 
 * 리플레시 토큰 불러오기 
 * @returns {string} 리플레시 토큰
 */
GlobalSign.RefreshToken_Get = function ()
{
    return CA.Get(GlobalSign.RefreshToken_CookieName);
};


/**
 * 리플레시 토큰 저장하기
 * @param {string} sRefreshToken 저장할 리플레시 토큰
 * @param {bool} bMonth1 토큰을 한달동안 보관할지 여부
 */
GlobalSign.RefreshToken_Set = function (sRefreshToken, bMonth1)
{
    //타입 저장
    var nSaveType = CA.SaveType.Default;

    //한달 보관 여부 저장
    if (true === bMonth1)
    {
        nSaveType = CA.SaveType.Month1;
    }

    //최대 한달짜리 쿠키
    CA.Set(GlobalSign.RefreshToken_CookieName
        , sRefreshToken
        , nSaveType);

};


/**
 *
 * @param {any} sRefreshToken
 */
GlobalSign.RefreshToken_SetOption = function (sRefreshToken)
{
    //타입 저장
    var nSaveType = CA.SaveType.Default;

    var sAutoSignIn = CA.Get(GlobalSign.AutoSignIn_CookieName);

    if ("true" === sAutoSignIn)
    {//자동저장 활성화 되있음
        nSaveType = CA.SaveType.Month1;
    }

    //토큰 저장 시도
    GlobalSign.RefreshToken_Set(sRefreshToken, nSaveType);
};





/**
 * 사인인 페이지로 이동
 */
GlobalSign.Move_SignIn = function ()
{
    location.href = FS_Url.SignIn;
};


/**
  서버쪽에서는 사인인이 안되있는 경우(토큰 만료 같은)
 * 프론트엔드에서 로그인 페이지로 넘기기 전에 사인인 관련 정보를 지워준다.
 * @param {bool} bMessage 메시지 출력 여부
 * @param {string} sMessage 출력할 메시지
 */
GlobalSign.Move_SignIn_Remove = function (bMessage, sMessage)
{
    var sMsg = sMessage;

    GlobalSign.SignIn = false;

    GlobalSign.AccessToken_Set("");
    GlobalSign.RefreshToken_Set("", CA.SaveType.Default);

    var funSiteMove = function ()
    {
        switch (GlobalStatic.SiteType)
        {
            case 0://일반타입
                //주소를 보고 동작을 결정한다.
                if ("" === location.hash)
                {//주소가 없다.
                    //홈으로 이동
                    //location.href = FS_Url.Home;
                    setTimeout(function () { location.href = "/"; }, 0);
                }
                else
                {//주소가 있다.
                    //UI 갱신
                    if (null !== SignInInfo.divSignInInfo)
                    {//ui대상이 있으면 갱신
                        SignInInfo.UserInfo_Load();
                    }
                    else
                    {//없으면 홈으로 보낸다.
                        Page.Move_Home();
                    }
                    
                }
                break;
            case 1://관리자타입
            default:
                //사인인 페이지로 이동
                GlobalSign.Move_SignIn();
                break;
        }
    }



    if (true === bMessage)
    {
        //alert(sMsg);
        GlobalStatic.MessageBox_Info(GlobalStatic.Title, sMsg);
        funSiteMove();
    }
    else
    {
        funSiteMove();
    }

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
        //alert("사인아웃이 되어 있습니다.");
        GlobalStatic.MessageBox_Info(GlobalStatic.Title, "사인아웃이 되어 있습니다.");
    }
    else
    {
        //사인 아웃 시도
        AA.put(AA.TokenRelayType.HeadAdd
            , {
                url: FS_Api.Sign_SignOut,
                type: "PUT",
                data: {
                    nID: GlobalSign.SignIn_ID
                    , sRefreshToken: GlobalSign.RefreshToken_Get()
                },
                dataType: "text",
                success: function (data)
                {
                    console.log(data);

                    //사인아웃 표시 
                    GlobalSign.SignIn = false;
                    //엑세스 토큰 제거
                    GlobalSign.AccessToken_Set("");

                    // 엑세스 토큰 갱신
                    GlobalSign.AccessTokenToInfo();

                    //alert("사인아웃이 되어 있습니다.");
                    GlobalStatic.MessageBox_Info(GlobalStatic.Title, "사인아웃이 되었습니다.");

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
                error: function (error)
                {
                    console.log(error);

                    if (error.responseJSON
                        && error.responseJSON.InfoCode) 
                    {
                        //alert(error.responseJSON.Message);
                        GlobalStatic.MessageBox_Error(GlobalStatic.Title,
                            "실패코드 : " + error.responseJSON.InfoCode + "<br /> "
                            + error.responseJSON.Message);
                    }
                }
            }
        );


    }//end if
};

/**
 * 엑세스토큰이 살아 있는지 여부
 * @returns {bool} 결과
 */
GlobalSign.isAccessToken = function ()
{
    var bReturn = false;

    if ("" !== GlobalSign.AccessToken_Get())
    {//엑세스토큰이 있다.
        bReturn = true;
    }

    return bReturn;
};



/**
 * 엑세스토큰이 있으면 유저 정보를 갱신한다.
 * @param {function} callback 갱신에 성공하면 할 동작
 */
GlobalSign.AccessTokenToInfo = function (callback) 
{
    if (true === GlobalSign.isAccessToken())
    {//엑세스 토큰이 
        AA.get(AA.TokenRelayType.HeadAdd
            , {
                url: FS_Api.Sign_AccessToUserInfo
                , success: function (jsonData)
                {
                    if ("0" === jsonData.InfoCode)
                    {//에러 없음
                        //사인인 되어있다고 확인해줌
                        GlobalSign.SignIn = true;

                        GlobalSign.SignIn_ID = jsonData.id;
                        GlobalSign.SignIn_Email = jsonData.email;
                        GlobalSign.SignIn_ViewName = jsonData.ViewName;

                        GlobalSign.SignIn_MgtClass = jsonData.MgtClass;


                        //UI 갱신
                        try
                        {
                            SignInInfo.UserInfo_Load();
                        }
                        catch (ex)
                        {
                            console.log(ex);
                            debugger;
                        }
                        

                        if (typeof callback === "function")
                        {
                            callback();
                        }
                    }
                    else
                    {//에러 있음

                    }
                }
                , error: function (jqXHR, textStatus, errorThrown) { }
            });

    }
    else
    {
        if (typeof callback === "function")
        {
            callback();
        }
    }
};