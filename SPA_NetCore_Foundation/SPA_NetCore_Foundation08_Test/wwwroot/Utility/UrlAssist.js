/**
 * url 파라메타를 분리한다.
 * https://www.it-swarm.dev/ko/jquery/url-%EB%A7%A4%EA%B0%9C-%EB%B3%80%EC%88%98-jquery-%EA%B0%80%EC%A0%B8-%EC%98%A4%EA%B8%B0-%EB%98%90%EB%8A%94-js%EC%97%90%EC%84%9C-%EC%BF%BC%EB%A6%AC-%EB%AC%B8%EC%9E%90%EC%97%B4-%EA%B0%92-%EA%B0%80%EC%A0%B8-%EC%98%A4%EB%8A%94-%EB%B0%A9%EB%B2%95/1041635364/
 * @param {string} sKey
 */
function getParamsSPA(sKey)
{
    var p = {};
    var search = "?" + location.hash.split("?")[1];
    search.replace(/[?&]+([^=&]+)=([^&]*)/gi, function (s, k, v) { p[k] = v })
    return sKey ? p[sKey] : p;
}