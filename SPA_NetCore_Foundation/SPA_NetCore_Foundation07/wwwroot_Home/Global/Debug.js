
/**
 * 디버일때 지정한 함수를 실행 한다.
 * @param {any} callback 실행할 함수
 */
function debugfun(callback)
{

    if (true === GlobalStatic.Debug) {
        callback();
    }
}