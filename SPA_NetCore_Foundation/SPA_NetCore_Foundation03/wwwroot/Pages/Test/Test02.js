
function Test02()
{
    GlobalStatic.PageType_Now = PageType.Test01;

    var objThis = this;

    //페이지 공통기능 로드
    Page.Load(function ()
    {
        //화면 인터페이스
        Page.DivContents.load("/Pages/Test/Test02.html"
            , function () {
                objThis.divOutput = $("#divOutput");
            });
    });
}

/** 출력용 div */
Test02.prototype.divOutput = null;

/**
 * 데이터 바인드 테스트
 * @param {int} nData 전달할 값
 */
Test02.prototype.Test01 = function (nData)
{
    var objThis = this;

    AA.get(FS_Api.Test_Test01
        , { nData: nData, sData: "테스트 01" }
        , function (data) {
            console.log(data);

            if ("0" === data.infoCode)
            {//에러 없음
                objThis.divOutput.html("nTest : " + data.nTest + " sTest : " + data.sTest );
            }
            else
            {//에러 있음
                //아웃풋 지우기
                objThis.divOutput.html("");
                alert("error code : " + data.infoCode + "\n"
                    + "내용 : " + data.message);
            }
        }
        , function (error) {
            console.log(error);

            //예측가능한 에러를 200으로 처리하지 않았다면 아래와 같이 만들어야 한다.
            //if (error.responseJSON && error.responseJSON.infoCode)
            //{
            //    alert("실패코드 : " + error.responseJSON.infoCode
            //        + "\n " + error.responseJSON.message);
            //}

        });
};

/** 데이터 바인드 테스트 */
Test02.prototype.Test02 = function () {
};