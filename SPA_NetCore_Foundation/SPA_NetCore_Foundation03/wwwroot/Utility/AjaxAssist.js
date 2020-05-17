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
AA.call = function (typeToken, jsonOption) {
    //이 함수에서 기본값으로 추가할 옵션
    //기본이 비동기다.
    var jsonOpt = {
        async: true,
        //contentType: "application/x-www-form-urlencoded; charset=utf-8",
        dataType: "json"
    };

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
    jsonOpt.error = function (jqXHR, textStatus, errorThrown) {
        //여기에 공통 작업내용을 넣는다.

        if (funError)
        {
            //성공하면 수행할 콜백
            funError(jqXHR, textStatus, errorThrown);
        }
    };


    $.ajax(jsonOpt);
};


/**
 * 아작스로 파일을 로드한다.
 * @param {string} sFileUrl 파일 url
 * @param {function} funSuccess 성공시 콜백
 * @param {function} jsonOption 추가 옵션
 */
AA.HtmlFileLoad = function (sFileUrl, funSuccess, jsonOption) {
    AA.get(AA.TokenRelayType.None
        , {
            url: sFileUrl
            , dataType: "html"
            , success: funSuccess
            , error: function (error) { console.log(error); }
        });
};
