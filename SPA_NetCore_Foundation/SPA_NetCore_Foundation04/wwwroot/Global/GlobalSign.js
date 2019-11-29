﻿/**
 * 사이트 전체에서 사인관련 코드들
 */
var GlobalSign = {};

/** 사인인 여부 */
GlobalSign.SignIn = false;
/** 사인인 - 아이디 정보 */
GlobalSign.SignIn_ID = "";
/** 사인인 - 토큰 정보 */
GlobalSign.SignIn_token = "";

/** 이메일 저장 - 쿠키용 이름 */
GlobalSign.EmailSave_CookieName = "spa_EmailSave";
/** 이메일 저장 여부 - 쿠키용 이름 */
GlobalSign.EmailSaveIs_CookieName = "spa_EmailSaveIs";
/** 자동 사인인 여부 - 쿠키용 이름 */
GlobalSign.AutoSignIn_CookieName = "spa_AutoSignIn";


/** 리플레시 토큰 - 쿠키용 이름 */
GlobalSign.RefreshToken_CookieName = "spa_RefreshToken";



/** 발급된 엑세스 토큰 */
GlobalSign.access_token = "";

/** 
 * 리플레시 토큰 불러오기 
 * @returns {string} 리플레시 토큰
 */
GlobalSign.RefreshToken_Get = function ()
{
    return CA.Get(GlobalSign.RefreshToken_CookieName);
};


GlobalSign.RefreshToken_SetOption = function (sRefreshToken)
{
    //타입 저장
    var nSaveType = CA.SaveType.Default;

    var sAutoSignIn = CA.Get(GlobalSign.AutoSignIn_CookieName);

    if ("true" === GlobalSign.AutoSignIn_CookieName)
    {//자동저장 활성화 되있음
        nSaveType = CA.SaveType.Month1;
    }

    //토큰 저장 시도
    GlobalSign.RefreshToken_Set(sRefreshToken, nSaveType);
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
    GlobalSign.SignIn = false;

    GlobalSign.access_token = "";
    GlobalSign.RefreshToken_Set("", CA.SaveType.Default);

    if (true === bMessage)
    {
        alert(sMessage);
    }
    
    GlobalSign.Move_SignIn();
};

/**
 * 사인아웃 시도
 */
GlobalSign.Move_SignOut = function ()
{
    //사인아웃 시도
    //location.href = FS_Url.SignIn;
    
    if (false === GlobalSign.SignIn)
    {//사인 아웃이 되어 있음
        alert("사인아웃이 되어 있습니다.");
    }
    else
    {
        //사인 아웃 시도
        AA.put(true
            , {
                url: FS_Api.Sign_SignOut,
                type: "PUT",
                data: {
                    sRefreshToken: GlobalSign.RefreshToken_Get()
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
                            TopInfo.UserInfo_Load();
                            break;
                    }
                },
                error: function (error) {
                    console.log(error);

                    if (error.responseJSON
                            && error.responseJSON.infoCode) {
                        alert("실패코드 : " + error.responseJSON.infoCode
                            + "\n " + error.responseJSON.message);
                    }
                }
            }
        );

        
    }
};

/**
 * 엑세스토큰이 살아 있는지 여부
 * @returns {bool} 결과
 */
GlobalSign.isAccessToken = function ()
{
    var bReturn = false;

    if ("" !== GlobalSign.access_token)
    {//엑세스토큰이 있다.
        bReturn = false;
    }

    return bReturn;
};