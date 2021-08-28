/**
 * 이 프로젝트에서 자주쓰는 아작스 호출 형식을 미리 정의 합니다.
 * 기본 옵션
 * async: false
 */

var AA = {};

/** 아작스 요청 타입 */
AA.AjaxType = {
    Get: "GET",
    Post: "POST",
    Put: "PUT",
    Patch: "PATCH",
    Delete: "DELETE"
};

/** 아작스 요청시 토큰을 어떻게 처리할지 여부 */
AA.TokenRelayType = {
    /** 전달하지 않음 */
    None: 0,

    /** 무조건 전달 */
    HeadAdd: 1,

    /** 
     *  기존 토큰 없으면 없는 데로 전달.
     *  기존 토큰이 죽어 있으면 갱신후 전달.
     * */
    CaseByCase: 2,
};

/** 
 *  RefreshToken을 갱신하고 있을때 아작스개체를 임시저장해둔다.
 *  짧은 시간 리플레시토큰을 여러번 사용하려고하면 싱크에 문제가 생겨
 *  죽어있는 토큰을 사용하려는 문제가 생긴다.
 *  그래서 여기에 개체를 넣어두고 다른 요청은 RefreshToAccess_WaitingList에 대기시켜둔다.
 * */
AA.RefreshToAccess_CallBool = false;
/** 이미 다른 리플레시 토큰이 갱신에 들어갔다면 다른 요청은 여기에 넣는다. */
AA.RefreshToAccess_WaitingList = {};
/** 리스트로 사용할 고유이름을 생성하기 위한 카운트 */
AA.RefreshToAccess_WaitingListNameCount = 0;

/**
 * get로 아작스 요청을 한다.
 * @param {TokenRelayType} typeToken 헤더에 토큰을 넣을지 여부
 * @param {json} jsonOption 아작스 요청에 사용할 옵션 데이터. 지정하지 않은 옵션은기본 옵션을 사용한다.
 */
AA.get = function (typeToken, jsonOption)
{
    jsonOption.type = AA.AjaxType.Get;
    AA.call(typeToken, jsonOption);
};

/**
 * post로 아작스 요청을 한다.
 * @param {TokenRelayType} typeToken 헤더에 토큰을 넣을지 여부
 * @param {json} jsonOption 아작스 요청에 사용할 옵션 데이터. 지정하지 않은 옵션은기본 옵션을 사용한다.
 */
AA.post = function (typeToken, jsonOption)
{
    jsonOption.type = AA.AjaxType.Post;
    AA.call(typeToken, jsonOption);
};

/**
 * put로 아작스 요청을 한다.
 * @param {TokenRelayType} typeToken 헤더에 토큰을 넣을지 여부
 * @param {json} jsonOption 아작스 요청에 사용할 옵션 데이터. 지정하지 않은 옵션은기본 옵션을 사용한다.
 */
AA.put = function (typeToken, jsonOption)
{
    jsonOption.type = AA.AjaxType.Put;
    AA.call(typeToken, jsonOption);
};

/**
 * patch로 아작스 요청을 한다.
 * @param {TokenRelayType} typeToken 헤더에 토큰을 넣을지 여부
 * @param {json} jsonOption 아작스 요청에 사용할 옵션 데이터. 지정하지 않은 옵션은기본 옵션을 사용한다.
 */
AA.patch = function (typeToken, jsonOption)
{
    jsonOption.type = AA.AjaxType.Patch;
    AA.call(typeToken, jsonOption);
};

/**
 * delete로 아작스 요청을 한다.
 * @param {TokenRelayType} typeToken 헤더에 토큰을 넣을지 여부
 * @param {json} jsonOption 아작스 요청에 사용할 옵션 데이터. 지정하지 않은 옵션은기본 옵션을 사용한다.
 */
AA.delete = function (typeToken, jsonOption)
{
    jsonOption.type = AA.AjaxType.Delete;
    AA.call(typeToken, jsonOption);
};



/**
 * jquery를 이용하여 요청을 처리합니다.
 * @param {TokenRelayType} typeToken 헤더에 토큰을 넣을지 여부
 * @param {json} jsonOption 처리할 옵션 객체
 */
AA.call = function (typeToken, jsonOption)
{
    //매개변수 백업(여기서는 이걸 원본취급한다.)
    let typeTokenTemp = typeToken;
    let jsonOptionTemp = jsonOption;

    if (false === AA.RefreshToAccess_CallBool)
    {//리플레시 토큰이 갱신중이 아니다.
        //이때만 정상 진행을 한다.


        //이 함수에서 기본값으로 추가할 옵션
        //기본이 비동기다.
        var jsonOpt = {
            async: true,
            //contentType: "application/x-www-form-urlencoded; charset=utf-8",
            dataType: "json"
        };

        //헤더에 토큰을 넣기 처리**********************
        //토큰 미리 읽기
        var sAccessToken = GlobalSign.AccessToken_Get();

        if ((AA.TokenRelayType.None === typeToken)
            || (AA.TokenRelayType.CaseByCase === typeToken
                && "" === sAccessToken))
        {//토큰을 전달하지 안거나.
            //상황에 맞게 보내야 하는데 토큰이 없다.

            //전달하지 않음
        }
        else
        {//헤더 넣기
            //모든 옵션은 무조건 입력이 우선이다.
            //그러니 토큰을 전달할 'authorization'가 있는지 확인한다.
            if (!jsonOption.headers)
            {
                //헤더 옵션 만들기
                jsonOption.headers = {};
            }
            //인증키가 있는지 확인한다.
            if (!jsonOption.headers["authorization"])
            {
                //엑세스 토큰의 변수를 프로젝트에 맞게 수정한다.
                jsonOption.headers["authorization"]
                    = "Bearer " + sAccessToken;

                if (false === (undefined === jsonOption.url_Auth
                    || null === jsonOption.url_Auth
                    || "" === jsonOption.url_Auth))
                {//jsonOption.url_Auth가 있다.
                    //로그인/비로그인을 같이 처리할때
                    //jsonOption.url_Auth는
                    //토큰이 있는데 죽었는지 살았는지 알수 없을때 호출하는 주소이다.
                    //토큰이 죽어있는 비로그인을 같이 처리하면 비회원 처리가 되므로 필요하다.

                    //토큰이 있다면 인증용으로 요청을 보낸다.
                    jsonOption.url = jsonOption.url_Auth;
                }
            }
        }

        //들어온 옵션을 합친다.(들어온 값 우선)
        jsonOpt = $.extend(jsonOpt, jsonOption);

        //success함수를 빼오고
        var funSuccess = jsonOpt.success;
        jsonOpt.success = function (data)
        {
            //여기에 공통 작업내용을 넣는다.

            if (funSuccess)
            {
                //성공하면 수행할 콜백
                funSuccess(data);
            }
        };

        //error함수를 빼오고
        var funError = jsonOpt.error;
        jsonOpt.error = function (jqXHR, textStatus, errorThrown)
        {
            //여기에 공통 작업내용을 넣는다.

            if (((AA.TokenRelayType.HeadAdd === typeTokenTemp)
                || (AA.TokenRelayType.CaseByCase === typeTokenTemp
                    && "" !== sAccessToken))
                && (401 === jqXHR.status))
            {//엑세스 키 사용 일때
                //401에러가 났다.
                //이 상황은 엑세스 토큰이 없거나 만료된것이다.

                //빠르게 요청이 여러번 되는경우 위에서 걸러지지 않고 여기까지 들어올수 있다.
                //여기서도 똑같이 예외처리를 해준다.
                if (false === AA.RefreshToAccess_CallBool)
                {//리플레시 토큰이 갱신중이 아니다.
                    AA.RefreshToAccess_CallBool = true;

                    //갱신 요청
                    AA.RefreshToAccess(function ()
                    {
                        //엑세스 토큰 갱신이 성공하면 다시 진행
                        //진행전에 엑세스토큰 갱신을 안하게 하도록 에러를 복구한다.
                        jsonOpt.error = funError;
                        //엑세스 토큰을 다시 입력한다.
                        jsonOpt.headers["authorization"]
                            = "Bearer " + GlobalSign.AccessToken_Get();
                        //여기서는 독자 호출한다.
                        $.ajax(jsonOpt);
                    });
                }
                else
                {
                    //리플레시 토큰의 갱신이 진행중이다.
                    //일단 정보를 리스트에 저장해둔다.
                    AA.RefreshToAccess_WaitingList_Add(
                        typeTokenTemp
                        , jsonOptionTemp);
                }
            }
            else
            {
                if (funError)
                {
                    //에러 콜백이 있으면 호출
                    funError(jqXHR, textStatus, errorThrown);
                }
            }
        };


        $.ajax(jsonOpt);
    }
    else
    {//리플레시 토큰의 갱신이 진행중이다.
        //일단 정보를 리스트에 저장해둔다.
        AA.RefreshToAccess_WaitingList_Add(
            typeTokenTemp
            , jsonOptionTemp);
    }

};

/**
* 액세스 토큰 갱신
* @param {function} callback 갱신이 성공하면 동작할 콜백
*/
AA.RefreshToAccess = function (callback)
{
    //위에서 값을 빼먹었을때를 대비해서 입력
    AA.RefreshToAccess_CallBool = true;

    //가지고 있는 토큰
    var refresh_token = GlobalSign.RefreshToken_Get();

    if (null === refresh_token || "" === refresh_token)
    {//리플레시 토큰이 없다.
        //리플레시 토큰이 없으면 토큰을 갱신할 수 없으므로
        //로그인이 필요하다.
        GlobalSign.Move_SignIn_Remove(true, "로그인이 필요합니다.<br /> 토큰 상실");
    }
    else
    {//토큰은 있는데
        //이미 인증이 진행중이 아니다.

        //갱신 시도
        //리플래시토큰을 갱신중이라는걸 임시저장한다.
        AA.RefreshToAccess_CallBool = true;

        $.ajax({
            type: AA.AjaxType.Put
            , url: FS_Api.Sign_RefreshToAccess
            , data: {
                "nID": GlobalSign.SignIn_ID
                , "sRefreshToken": refresh_token
                , "sPlatformInfo": GlobalStatic.PlatformInfo
            }
            , dataType: "json"
            , success: function (jsonResult)
            {
                console.log(jsonResult);

                if (jsonResult.InfoCode === "0")
                {//성공

                    //받은 정보 다시 저장
                    GlobalSign.SignIn_ID = jsonResult.idUser;
                    GlobalSign.SignIn_Email = jsonResult.Email;
                    GlobalSign.SignIn_ViewName = jsonResult.ViewName;

                    GlobalSign.AccessToken_Set(jsonResult.access_token);
                    GlobalSign.RefreshToken_SetOption(jsonResult.refresh_token);


                    //유저 정보를 갱신한다.
                    SignInInfo.UserInfo_Load();


                    //임시저장한 아작스개체를 삭제한다.
                    AA.RefreshToAccess_CallBool = false;

                    //요청한 콜백 진행
                    if (typeof callback === "function")
                    {
                        callback();
                    }



                    //가지고 있는 대기 리스트를 진행시킨다. ****************
                    //RefreshToAccess_WaitingList에 있는 요청을 진행한다.
                    var arrKey = Object.keys(AA.RefreshToAccess_WaitingList);
                    var nCount = arrKey.length;

                    for (var i = 0; i < nCount; ++i)
                    {
                        //문자열로 변경
                        let itemTemp = arrKey[i];

                        //약간의 딜레이를 주고 요청을 진행한다.
                        setTimeout(function ()
                        {
                            var item = AA.RefreshToAccess_WaitingList[itemTemp];
                            //요청을 전달하고
                            AA.call(item[0], item[1]);
                            //사용한 요청을 지운다.
                            delete AA.RefreshToAccess_WaitingList[itemTemp];
                        }, 10 * i);
                    }


                }
                else if (jsonResult.InfoCode === "-103")
                {//너무 짧은 시간에 여러번 호출됐다.
                    //이 오류가 났다는건 DB에 있는 토큰으로 다시빠르게 재요청했다는 의미다.
                    //가지고 있는 토큰자체가 만료되거나 잘못됐으면 다른 에러가 나기 때문이다.
                    //결국 가지고 있는 토큰이 멀쩡한 토큰이라는 소리다.
                    //그러니 기존 토큰을 지우지 말고 콜백을 진행시킨다.

                    //요청한 콜백 진행
                    if (typeof callback === "function")
                    {
                        callback();
                    }

                    //이건 프론트엔드에서 막아야 한다.
                    //GlobalSign.Move_SignIn_Remove(true
                    //    , "로그인이 필요합니다.<br /> 사유 : 너무 짧은시간에 인증요청을 여러번하였습니다.");
                    //
                }
                else
                {//실패
                    //리플래시 토큰 요청이 실패하면 모든 토큰을 지워야 한다.
                    GlobalSign.Move_SignIn_Remove(true
                        , "로그인이 필요합니다.<br /> 사유 : " + jsonResult.Message);
                }
            }
            , error: function (jqXHR, textStatus, errorThrown)
            {
                console.log(jqXHR);

                //순간적으로 인터넷이 끊길 가능성이 있으므로 요청이 실패했음만 알리고 토큰을 그대로 둔다.
                GlobalStatic.MessageBox_Error(""
                    , "알수 없는 이유로 인증정보 갱신이 실패하였습니다.<br /> 코드 : "
                    + jqXHR.status + "(" + errorThrown + ")");
            }
        });
    }//end if
};

/**
 * 리플레시 토큰의 갱신이 진행중인경우 요청 리스트를 임시저장해준다.
 * @param {any} typeToken 저장할 토큰 타입
 * @param {any} jsonOption 저장할 옵션
 */
AA.RefreshToAccess_WaitingList_Add = function (typeToken, jsonOption)
{
    AA.RefreshToAccess_WaitingList["" + (++AA.RefreshToAccess_WaitingListNameCount)]
        = [typeToken, jsonOption];
};

/**
 * 아작스로 파일을 로드한다.
 * @param {string} sFileUrl 파일 url
 * @param {function} funSuccess 성공시 콜백
 * @param {function} jsonOption 추가 옵션
 */
AA.HtmlFileLoad = function (sFileUrl, funSuccess, jsonOption)
{
    AA.get(AA.TokenRelayType.None
        , {
            url: sFileUrl
            , dataType: "html"
            , success: funSuccess
            , error: function (error) { console.log(error); }
        });
};



