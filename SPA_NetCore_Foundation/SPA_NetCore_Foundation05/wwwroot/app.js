// 라우트 어플리케이션 생성
var app = Sammy(function ()
{

    if (false === dgIsObject.IsBoolValue(GlobalSign.SignIn))
    {//사인인 정보가 없다.
        //엑세스토큰 확인
        GlobalSign.AccessTokenToInfo();
    }

    //라우트 설정****
    //Page에 페이지 이동 공통화가 있는데 여기서는 사용하면
    //무한루프에 빠질 수 있다.
    //이곳은 location.href로만 이동해야 한다.

    this.get("/", function ()
    {
        app.RouteCheck(function ()
        {
            //this.RouteCheck에서 로그인 체크를 해준다.
            //그러니 여기서는 홈으로만 이동하면 된다.
            location.href = FS_Url.Home;
        });
    });

    this.get(FS_Url.SignIn, function ()
    {
        if (true === GlobalSign.SignIn)
        {//로그인 되어 있음
            //홈으로 이동
            location.href = FS_Url.Home;
        }
        else
        {//로그인 되어있지 않음
            //객체 생성
            GlobalStatic.Page_Now = new SignIn();
        }

    });


    this.get(FS_Url.Home, function ()
    {
        app.RouteCheck(function ()
        {
            //객체 생성
            GlobalStatic.Page_Now = new Home();
        });
    });




    this.get(FS_Url.Test01, function () 
    {
        app.RouteCheck(function ()
        {
            //객체 생성
            GlobalStatic.Page_Now = new Test01();
        });
    });

    this.get(FS_Url.Test02, function () 
    {
        app.RouteCheck(function ()
        {
            //객체 생성
            GlobalStatic.Page_Now = new Test02();
        });
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
    this.notFound = function (verb, path) 
    {
        switch (GlobalStatic.SiteType)
        {
            case 0://일반
                //일반일때는 무조건 컨탠츠 영역에 출력한다.
                if (null !== Page.DivContents)
                {//
                    Page.DivContents.html("404, 페이지 못찾음");
                }
                else
                {
                    DivMain.html("404, 페이지 못찾음");
                }
                break;
            case 1://어드민 타입
                //어드민 타입일때는 사인인이 되어 있을때만 컨탠츠 영역에 출력한다.
                if (true === GlobalSign.SignIn
                    && null !== Page.DivContents)
                {
                    Page.DivContents.html("404, 페이지 못찾음");
                }
                else
                {
                    DivMain.html("404, 페이지 못찾음");
                }
                break;
        }
    };
});


/**
* 라우트 체크.
* 불허에 따른 작업은 이곳에서 한다.
* @param {function} callback 허가가 났으면 동작시킬 콜백
*/
app.RouteCheck = function (callback)
{
    var bReturn = true;

    switch (GlobalStatic.SiteType)
    {
        case 1://어드민 타입
            if (false === GlobalSign.SignIn)
            {//로그인 안되있음

                //실패 알림
                bReturn = false;
                //로그인 페이지로 이동
                GlobalSign.Move_SignIn();
            }
            break;

        case 0://기본 타입
        default:
            bReturn = true;
            break;
    }

    if (true === bReturn)
    {//허가가 났다.
        //콜백 호출
        callback();
    }
};

//어플리케이션 시작
$(function () {
    app.run();
});