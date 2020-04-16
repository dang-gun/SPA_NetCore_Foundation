
function Test02()
{
    GlobalStatic.PageType_Now = PageType.Test01;

    var objThis = this;

    //페이지 공통기능 로드
    Page.Load(function ()
    {
        //화면 인터페이스
        Page.divContents.load("/Pages/Test/Test02.html"
            , function () {
                objThis.divOutput = $("#divOutput");
            });
    });
}

/** 출력용 div */
Test02.prototype.divOutput = null;

/**
 * 데이터 호출 테스트
 * @param {int} nData 전달할 값
 */
Test02.prototype.Test01 = function (nData)
{
    var objThis = this;
    AA.get(false
        , {
            url: FS_Api.Test_Test01
            , data: { nData: nData, sData: "테스트 01" }
            , success: function (data) {
                console.log(data);

                if ("0" === data.InfoCode) {//에러 없음
                    objThis.divOutput.html("nTest : " + data.nTest + " sTest : " + data.sTest);
                }
                else {//에러 있음
                    //아웃풋 지우기
                    objThis.divOutput.html("");
                    alert("error code : " + data.InfoCode + "\n"
                        + "내용 : " + data.message);
                }
            }
            , error: function (error) {
                console.log(error);
            }
        });
};

/** 데이터 호출 - 사인인 필요 */
Test02.prototype.Test02 = function ()
{
    var objThis = this;

    if (true === dgIsObject.IsBoolValue(GlobalSign.SignIn))
    {//사인인이 되어 있다.

        AA.get(true
            , {
                url: FS_Api.Test_Test02
                , data: { nData: 100, sData: "테스트 02" }
                , success: function (data) {
                    console.log(data);

                    if ("0" === data.InfoCode) {//에러 없음
                        objThis.divOutput.html("nTest001 : " + data.nTest001 + " sTest002 : " + data.sTest002);
                    }
                    else {//에러 있음
                        //아웃풋 지우기
                        objThis.divOutput.html("");
                        alert("error code : " + data.InfoCode + "\n"
                            + "내용 : " + data.message);
                    }
                }
                , error: function (error) {
                    console.log(error);
                }
            });
    }
    else
    {
        alert("사인을 해야 사용할 수 있습니다.");
    }
};