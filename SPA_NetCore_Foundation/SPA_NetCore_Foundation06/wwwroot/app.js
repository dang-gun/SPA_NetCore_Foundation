// 라우트 어플리케이션 생성
var app = Sammy(function ()
{
    if (false === dgIsObject.IsBoolValue(GlobalStatic.SignIn))
    {//사인인 정보가 없다.
        //엑세스토큰 확인
        GlobalSign.AccessTokenToInfo();
    }
    

    //라우트 설정****

    this.get("/", function ()
    {
        location.href = FS_Url.Home;

        switch (GlobalStatic.SiteType) {
            case 1://어드민 타입
                location.href = FS_Url.SignIn;
                break;

            case 0:
            default:
                location.href = FS_Url.Home;
                break;
        }
    });

    this.get(FS_Url.SignIn, function ()
    {
        //객체 생성
        GlobalStatic.Page_Now = new SignIn();
    });


    this.get(FS_Url.Home, function ()
    {
        //객체 생성
        GlobalStatic.Page_Now = new Home();
    });




    this.get(FS_Url.Test01, function () {
        //객체 생성
        GlobalStatic.Page_Now = new Test01();
    });

    this.get(FS_Url.Test02, function () {
        //객체 생성
        GlobalStatic.Page_Now = new Test02();
    });

    //this.get("#/", function () {
    //    //인덱스 페이지
    //    //$("#divMain").load("/Pages/index.html");
    //    DivMain.html("홈");
    //});

    //this.get("#/param/:id", function () {
    //    //파라미터 받기
    //    var nID = this.params['id'];

    //    $("#divMain").html("넘어온 파라미터 id : " + nID);
    //});

    //404
    this.notFound = function (verb, path) {
        //인덱스 페이지
        //$("#divMain").load("/Pages/index.html");
        DivMain.html("404, 페이지 못찾음");
    };
});

//어플리케이션 시작
$(function () {
    app.run();
});