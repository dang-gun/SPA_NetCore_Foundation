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
AA.call = function (bToken, jsonOption) {
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
        if (!jsonOption.headers["authorization"])
        {
            //엑세스 토큰의 변수를 프로젝트에 맞게 수정한다.
            jsonOption.headers["authorization"] = "Bearer " + GlobalSign.access_token;
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

    var bAsync = false;
    if (jsonOption && jsonOption.async) {
        bAsync = jsonOption.async;
    }

    AA.get(sFileUrl
        , {}
        , funSuccess
        , function () { }
        , {
            async: bAsync,
            dataType: "html"
        });
};
