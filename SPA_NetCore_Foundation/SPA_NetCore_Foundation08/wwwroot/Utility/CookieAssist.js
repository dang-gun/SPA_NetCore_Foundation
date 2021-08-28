/**
 * 쿠키 지원
 */

var CA = {};

/** 저장 타입 - 저장할 기간 */
CA.SaveType = {
    Default : 0
    , Month1 : 1
    , Year1: 2
    , Infinite: 9999
};

/**
 * 지정한 쿠키를 불러온다.
 * @param {string} sName 불러올 쿠키 이름
 * @returns {string} 쿠키 내용
 */
CA.Get = function (sName)
{
    //쿠키를 일단 부른다.
    var sReturn = $.cookie(sName);

    if (!sReturn)
    {//쿠키가 없다.
        //없으면 빈값을 준다.
        sReturn = "";
    }

    return sReturn;
};

/**
 * 쿠키 저장
 * @param {string} sName 쿠키 이름
 * @param {string} sValue 저장할 정보
 * @param {type} nType 저장 타입. 'CA.SaveType'기준
 */
CA.Set = function (sName, sValue, nType)
{
    //기간 일수
    var nExpires = 0;

    switch (nType)
    {
        case CA.SaveType.Month1:
            nExpires = 30;
            break;
        case CA.SaveType.Year1:
            nExpires = 365;
            break;

        case CA.SaveType.Infinite:
            nExpires = 3650;
            break;

        case CA.SaveType.Default:
        default:
            nExpires = 0;
            break;
    }

    if (nExpires > 0)
    {
        //쿠키 저장
        $.cookie(sName
            , sValue
            , { expires: nExpires });
    }
    else
    {
        //0일때는 수명을 지정하지 않는다.
        //쿠키 저장
        $.cookie(sName
            , sValue);
    }

    
};
