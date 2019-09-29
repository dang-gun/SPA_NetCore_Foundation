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
 * @param {string} sUrl 요청할 URL
 * @param {json} jsonData 요청에 사용할 데이터.
 * @param {function} funSuccess 아작스 요청 성공시 호출됨.
 * @param {function} funError 아작스 오청이 실패시 호출됨.
 * @param {json} jsonOption 아작스 요청에 사용할 옵션 데이터. 지정하지 않은 옵션은기본 옵션을 사용한다.
 */
AA.get = function (sUrl, jsonData, funSuccess, funError, jsonOption) {
    var objOption = AA.OptionAdd(AA.AjaxType.Get, sUrl, jsonData, funSuccess, funError, jsonOption);
    AA.call(objOption);
};

/**
 * post로 아작스 요청을 한다.
 * @param {string} sUrl 요청할 URL
 * @param {json} jsonData 요청에 사용할 데이터.
 * @param {function} funSuccess 아작스 요청 성공시 호출될 함수.
 * @param {function} funError 아작스 오청이 실패시 호출될 함수.
 * @param {json} jsonOption 아작스 요청에 사용할 옵션 데이터. 지정하지 않은 옵션은기본 옵션을 사용한다.
 */
AA.post = function (sUrl, jsonData, funSuccess, funError, jsonOption) {
    var objOption = AA.OptionAdd(AA.AjaxType.Post, sUrl, jsonData, funSuccess, funError, jsonOption);
    AA.call(objOption);
};

/**
 * put로 아작스 요청을 한다.
 * @param {string} sUrl 요청할 URL
 * @param {json} jsonData 요청에 사용할 데이터.
 * @param {function} funSuccess 아작스 요청 성공시 호출될 함수.
 * @param {function} funError 아작스 오청이 실패시 호출될 함수.
 * @param {json} jsonOption 아작스 요청에 사용할 옵션 데이터. 지정하지 않은 옵션은기본 옵션을 사용한다.
 */
AA.put = function (sUrl, jsonData, funSuccess, funError, jsonOption) {
    var objOption = AA.OptionAdd(AA.AjaxType.Put, sUrl, jsonData, funSuccess, funError, jsonOption);
    AA.call(objOption);
};

/**
 * delete로 아작스 요청을 한다.
 * @param {string} sUrl 요청할 URL
 * @param {json} jsonData 요청에 사용할 데이터.
 * @param {function} funSuccess 아작스 요청 성공시 호출될 함수.
 * @param {function} funError 아작스 오청이 실패시 호출될 함수.
 * @param {json} jsonOption 아작스 요청에 사용할 옵션 데이터. 지정하지 않은 옵션은기본 옵션을 사용한다.
 */
AA.delete = function (sUrl, jsonData, funSuccess, funError, jsonOption) {
    var objOption = AA.OptionAdd(AA.AjaxType.Delete, sUrl, jsonData, funSuccess, funError, jsonOption);
    AA.call(objOption);
};

/**
 * 아작스 요청에 사용될 옵션을 완성한다.
 * @param {string} sType 문자열로 아작스 타입을 넣거나 'AA.AjaxType'을 사용한다.
 * @param {string} sUrl 요청할 URL
 * @param {json} jsonData 요청에 사용할 데이터.
 * @param {function} funSuccess 아작스 요청 성공시 호출될 함수.
 * @param {function} funError 아작스 오청이 실패시 호출될 함수.
 * @param {json} jsonOption 아작스 요청에 사용할 옵션 데이터. 지정하지 않은 옵션은기본 옵션을 사용한다.
 * @returns {json} 완성된 옵션
 */
AA.OptionAdd = function (sType, sUrl, jsonData, funSuccess, funError, jsonOption) {
    if (!jsonOption)
    {//없으면 빈값으로 처리
        jsonOption = {};
    }

    var jsonOptTemp = {
        type: sType,
        url: sUrl,
        data: jsonData,
        success: funSuccess,
        error: funError
    };

    //들어온 옵션을 합친다.(들어온 값 우선)
    var jsonOpt = $.extend(jsonOptTemp, jsonOption);

    return jsonOpt;
};


/**
 * jquery를 이용하여 요청을 처리합니다.
 * @param {json} jsonOption 처리할 옵션 객체
 */
AA.call = function (jsonOption) {
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

        //성공하면 수행할 콜백
        funSuccess(data);
    };

    //error함수를 빼오고
    var funError = jsonOpt.error;
    jsonOpt.error = function (jqXHR, textStatus, errorThrown) {
        //여기에 공통 작업내용을 넣는다.

        //성공하면 수행할 콜백
        funError(jqXHR, textStatus, errorThrown);
    };


    $.ajax(jsonOpt);

};



AA.HtmlFileLoad = function (sFileUrl, funSuccess, jsonOption) {
    //아작스로 파일을 로드한다.
    //objOption.async : 동기 여부

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
