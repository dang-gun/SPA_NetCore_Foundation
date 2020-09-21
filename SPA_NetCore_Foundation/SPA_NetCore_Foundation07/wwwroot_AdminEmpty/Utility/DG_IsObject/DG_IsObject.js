/*
 * DG dgIsObject 1.0
 * https://blog.danggun.net/7449
 * https://github.com/dang-gun/DG_JsDataBind
 *
 * test : https://dang-gun.github.io/DG_JsDataBind/test/index.html
 */

var dgIsObject = {};

/**
 * 들어온 오브젝트가 bool값인지 여부 판단.
 * bool값인지를 확인하는 것이지 들어있는 값이 참인지 거짓인지는 판단하지 않는다.
 * @param {object} objData 판단할 오브젝트
 * @returns {boolean} bool값인지 여부.
 */
dgIsObject.IsBool = function (objData)
{
    var bReturn = false;

    if (typeof objData === "boolean")
    {
        bReturn = true;
    }

    return bReturn;
};

/**
 * 들어온 오브젝트가 bool값인지 여부 판단하고 값을 리턴한다.
 * 들어온 타입이 bool이 아니면 false가 리턴된다.
 * true나 false로 판단이 가능하면 판단된 값이 리턴된다.
 * 들어온 타입이 string 이면 "true"나 "false"값인지 판단하여 리턴한다.
 * @param {object} objData 판단할 오브젝트
 * @returns {boolean} 값의 판단 여부.
 */
dgIsObject.IsBoolValue = function (objData)
{
    var bReturn = false;
    if (true === dgIsObject.IsBool(objData))
    {//들어온 값이 bool값이다.
        bReturn = objData;
    }
    else if (true === dgIsObject.IsString(objData))
    {//들어온 값이 string값이다.
        if ("true" === objData)
        {
            bReturn = true;
        }
    }

    return bReturn;
};

/**
 * 들어온 오브젝트가 string값인지 여부 판단.
 * @param {object} objData 판단할 오브젝트
 * @returns {boolean} string값인지 여부.
 */
dgIsObject.IsString = function (objData)
{
    var bReturn = false;

    if (typeof objData === "string")
    {
        bReturn = true;
    }

    return bReturn;
};

/**
 * 들어온 오브젝트가 string값인지 여부 판단하고 비어있는지 여부를 판단한다.
 * 
 * @param {object} objData 판단할 오브젝트
 * @returns {boolean} string값이 아니거나 비어있을때 false.
 */
dgIsObject.IsStringNotEmpty = function (objData)
{
    var bReturn = false;

    if (true === dgIsObject.IsString(objData))
    {//문자열이 맞다.
        if (0 < objData.length)
        {
            bReturn = true;
        }
    }

    return bReturn;
};

/**
 * 들어온 오브젝트를 문자열로 바꾼다.
 * 
 * @param {object} objData 판단할 오브젝트
 * @returns {string} 변환된 string값
 */
dgIsObject.IsStringValue = function (objData)
{
    var sReturn = "";

    if (false === dgIsObject.IsStringNotEmpty(objData))
    {
        sReturn = "";
    }
    else
    {
        sReturn = String(objData);
    }

    return sReturn;
};

/**
 * 들어온 오브젝트가 int값인지 여부 판단.
 * @param {object} objData 판단할 오브젝트
 * @returns {boolean} int값인지 여부.
 */
dgIsObject.IsInt = function (objData)
{
    var bReturn = false;

    if (true === Number.isInteger(bReturn))
    {
        bReturn = true;
    }

    return bReturn;
};


/**
 * 들어온 오브젝트를 int로 변환한다.
 * int로 변환 불가능한 값인 경우 0으로 변환됨.
 * @param {object} objData 판단할 오브젝트
 * @returns {int} int로 변환된 값
 */
dgIsObject.IsIntValue = function (objData)
{
    var bReturn = 0;

    if (false === isNaN(objData))
    {//숫자형이다.
        bReturn = Number(objData);
    }

    return bReturn;
};



/**
 * 선언이 있는지 확인한다.
 * undefined나 null이 아니면 참이 리턴됨.
 * 
 * 이 함수는 사용할 수 없다.(선언되지 않는 변수를 매개변수로 받을 수 없기 때문)
 * @param {object} objData 확인할 데이터
 * @returns {int} int로 변환된 값
 */
dgIsObject.IsDefined = function (objData)
{
    var bReturn = false;

    if (typeof objData !== "undefined")
    {
        if (null !== objData)
        {
            bReturn = true;
        }
    }

    return bReturn;
};