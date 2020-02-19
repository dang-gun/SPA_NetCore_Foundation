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
    Delete: "DELETE"
};

/**
 * get로 아작스 요청을 한다.
 * @param {bool} bToken 헤더에 토큰을 넣을지 여부
 * @param {json} jsonOption 아작스 요청에 사용할 옵션 데이터. 지정하지 않은 옵션은기본 옵션을 사용한다.
 */
AA.get = function (bToken, jsonOption) {
    jsonOption.type = AA.AjaxType.Get;
    AA.call(bToken, jsonOption);
};

/**
 * post로 아작스 요청을 한다.
 * @param {bool} bToken 헤더에 토큰을 넣을지 여부
 * @param {json} jsonOption 아작스 요청에 사용할 옵션 데이터. 지정하지 않은 옵션은기본 옵션을 사용한다.
 */
AA.post = function (bToken, jsonOption) {
    jsonOption.type = AA.AjaxType.Post;
    AA.call(bToken, jsonOption);
};

/**
 * put로 아작스 요청을 한다.
 * @param {bool} bToken 헤더에 토큰을 넣을지 여부
 * @param {json} jsonOption 아작스 요청에 사용할 옵션 데이터. 지정하지 않은 옵션은기본 옵션을 사용한다.
 */
AA.put = function (bToken, jsonOption) {
    jsonOption.type = AA.AjaxType.Put;
    AA.call(bToken, jsonOption);
};

/**
 * delete로 아작스 요청을 한다.
 * @param {bool} bToken 헤더에 토큰을 넣을지 여부
 * @param {json} jsonOption 아작스 요청에 사용할 옵션 데이터. 지정하지 않은 옵션은기본 옵션을 사용한다.
 */
AA.delete = function (bToken, jsonOption) {
    jsonOption.type = AA.AjaxType.Delete;
    AA.call(bToken, jsonOption);
};


/**
 * jquery를 이용하여 요청을 처리합니다.
 * @param {bool} bToken 헤더에 토큰을 넣을지 여부
 * @param {json} jsonOption 처리할 옵션 객체
 */
AA.call = function (bToken, jsonOption)
{
    //매개변수 백업
    var bTokenTemp = bToken;

    //이 함수에서 기본값으로 추가할 옵션
    //기본이 비동기다.
    var jsonOpt = {
        async: true,
        //contentType: "application/x-www-form-urlencoded; charset=utf-8",
        dataType: "json"
    };

    //헤더에 토큰을 넣기 처리
    if (true === bToken)
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
                = "Bearer " + GlobalSign.AccessToken_Get();
        }
    }

    //들어온 옵션을 합친다.(들어온 값 우선)
    jsonOpt = $.extend(jsonOpt, jsonOption);

    //success함수를 빼오고
    var funSuccess = jsonOpt.success;
    jsonOpt.success = function (data) {
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

        if ((true === bTokenTemp)
            && (401 === jqXHR.status))
        {//엑세스 키 사용 일때
            //401에러가 났다.

            //이 상황은 엑세스 토큰이 없거나 만료된것이다.
            //갱신 요청
            AA.RefreshToAccess(function () {
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
            if (funError)
            {
                //에러 콜백이 있으면 호출
                funError(jqXHR, textStatus, errorThrown);
            }
        }
    };


    $.ajax(jsonOpt);

};

/**
* 액세스 토큰 갱신
* @param {function} callback 갱신이 성공하면 동작할 콜백
*/
AA.RefreshToAccess = function (callback)
{
    var refresh_token = GlobalSign.RefreshToken_Get();

    if (null === refresh_token || "" === refresh_token)
    {//리플레시 토큰이 없다.
        //리플레시 토큰이 없으면 토큰을 갱신할 수 없으므로
        //로그인이 필요하다.
        GlobalSign.Move_SignIn_Remove(true, "로그인이 필요합니다.");
    }
    else
    {//있다.

        //갱신 시도
        $.ajax({
            type: AA.AjaxType.Put
            , url: FS_Api.Sign_RefreshToAccess
            , data: {
                "nID": GlobalSign.SignIn_ID
                , "sRefreshToken": refresh_token
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
                    TopInfo.UserInfo_Load();

                    //요청한 콜백 진행
                    if (typeof callback === "function")
                    {
                        callback();
                    }

                }
                else
                {//실패
                    //리플래시 토큰 요청이 실패하면 모든 토큰을 지워야 한다.
                    GlobalSign.Move_SignIn_Remove(true, "로그인이 필요합니다.");
                }
            }
            , error: function (jqXHR, textStatus, errorThrown)
            {
                console.log(jqXHR);

                //리플래시 토큰 요청이 실패하면 모든 토큰을 지워야 한다.
                GlobalSign.Move_SignIn_Remove(true, "로그인이 필요합니다.");
            }
        });
    }//end if
};

/**
 * 아작스로 파일을 로드한다.
 * @param {string} sFileUrl 파일 url
 * @param {function} funSuccess 성공시 콜백
 * @param {function} jsonOption 추가 옵션
 */
AA.HtmlFileLoad = function (sFileUrl, funSuccess, jsonOption) {
    AA.get(false
        , {
            url: sFileUrl
            , dataType: "html"
            , success: funSuccess
            , error: function (error) { console.log(error); }
        });
};
