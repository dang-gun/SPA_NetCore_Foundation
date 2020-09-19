/** app 지원 */
var app_Assist = {};

/** 첫 토큰 체크를 했는지 여부 */
app_Assist.bFirstAccessCheck = false;
/** 라우트 진행전에 임시로 저장해두는 콜백 */
app_Assist.RouteCallback = function ()
{
};

/** 이전 주소 */
app_Assist.Path_Previous = "";
/** 이전 주소의 로그인 필수 여부 */
app_Assist.Path_PreviousSignIn = false;

/** 이전 주소 */
app_Assist.Path_Now = "";
/** 이전 주소의 로그인 필수 여부 */
app_Assist.Path_NowSignIn = false;




/**
* 라우트 체크.
* 불허에 따른 작업은 이곳에서 한다.
* 개체를 생성할때 공통으로 해야할 동작이 있다면 이곳에서 처리한다.
* @param {boolean} bSignIn 사인인이 필수 인지 여부
* @param {object} objThis app this를 전달한다.
* @param {function} callback 허가가 났으면 동작시킬 콜백
*/
app_Assist.RouteCheck = function (bSignIn, objThis, callback)
{
    var bSignInTemp = bSignIn;
    var callbackTemp = callback;

    /* */

    //라우트 판단 코드를 콜백으로 만들어 둔다.
    var funCallback = function ()
    {
        var bReturn = true;

        if (true === bSignInTemp)
        {//사인인이 필수다.
            if (false === GlobalSign.SignIn)
            {//로그인 안되있음
                //실패 알림
                bReturn = false;
            }
        }
        else
        {//사인이이 필수가 아니다.
            switch (GlobalStatic.SiteType)
            {
                case 1://어드민 타입
                    //어드민 타입은 로그인 필수다.
                    if (false === GlobalSign.SignIn)
                    {//로그인 안되있음
                        //실패 알림
                        bReturn = false;
                    }
                    break;

                case 0://기본 타입
                default:
                    //기본 타입은 사인인이 필수가 아니면 그냥 동작한다.
                    bReturn = true;
                    break;
            }
        }

        if (true === bReturn)
        {//허가가 났다.

            //이전 주소 백업
            app_Assist.Path_Previous = app_Assist.Path_Now;
            app_Assist.Path_PreviousSignIn = app_Assist.Path_NowSignIn;

            //지금 주소 저장
            app_Assist.Path_Now = objThis.path;
            app_Assist.Path_NowSignIn = bSignInTemp;

            //콜백 호출
            callbackTemp({
                /** 사인인 여부 */
                SignIn: bSignInTemp
            });
        }
        else
        {//실패 했다.

            //if (false === GlobalSign.isAccessToken())
            if (false === GlobalSign.SignIn)
            {//엑세스토큰이 죽어 있다.
                //죽어있을때만 안내를 해준다.
                //어차피 엑세스토큰이 갱신됐을때 메시지가 출력되므로.
                //alert("사인인이 필요합니다.");
                GlobalStatic.MessageBox_Error(GlobalStatic.Title, "사인인이 필요합니다.");
            }

            switch (GlobalStatic.SiteType)
            {
                case 1://어드민 타입
                    //어드민타입은 사인인으로 보냅니다.
                    GlobalSign.Move_SignIn();
                    break;

                case 0://기본 타입
                default:
                    {
                        //이전 페이지
                        if (true === app_Assist.Path_PreviousSignIn)
                        {//이전 페이지가 로그인 필수 페이지다.
                            //홈으로 보낸다.
                            Page.Move_Home();
                        }
                        else
                        {//이전 페이지가 갈수 있는 곳이다.
                            //뒤로가기
                            history.back();
                        }
                    }
                    break;
            }
        }
    };

    //첫 토큰 확인이 끝났는지 확인
    if (true === GlobalSign.SignIn
        || true === app_Assist.bFirstAccessCheck)
    {//로그인이 되어 있거나
        //첫 토큰 확인이 끝났다.

        //바로 체크함수 동작
        funCallback();
    }
    else
    {//첫 토큰 체크가 되지 않았다.

        //첫 토큰 체크가 끝나면 'app_Assist.RouteCallback'를 호출하게 되어 있으므로
        //임시로 저장해 둔다.
        app_Assist.RouteCallback = funCallback;
    }
};





// 라우트 어플리케이션 생성
var app = Sammy(function ()
{
    if (false === dgIsObject.IsBoolValue(GlobalSign.SignIn))
    {//사인인 정보가 없다.
        //엑세스토큰 확인
        GlobalSign.AccessTokenToInfo(
            function ()
            {
                //첫 토큰 체크를 했다.
                app_Assist.bFirstAccessCheck = true;
                app_Assist.RouteCallback();
                app_Assist.RouteCallback = function () { };
            });
    }



    //라우트 설정****
    //Page에 페이지 이동 공통화가 있는데 여기서는 사용하면
    //무한루프에 빠질 수 있다.
    //이곳은 location.href로만 이동해야 한다.

    this.get("/", function ()
    {
        app_Assist.RouteCheck(false, this,
            function (jsonOpt)
            {
                //this.RouteCheck에서 로그인 체크를 해준다.
                //그러니 여기서는 홈으로만 이동하면 된다.
                location.href = FS_Url.Home;
            });
    });

    this.get(FS_Url.Error + "/:code", function ()
    {
        //파라미터 받기
        var nCode = this.params["code"];

        app_Assist.RouteCheck(false, this,
            function ()
            {
                //객체 생성
                GlobalStatic.Page_Now = new Error(nCode);
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


    this.get(FS_Url.About, function ()
    {
        //사인인 필수 페이지
        app_Assist.RouteCheck(false, this,
            function ()
            {
                //객체 생성
                GlobalStatic.Page_Now = new About();
            });
    });

    this.get(FS_Url.Admin, function ()
    {
        //사인인 필수 페이지
        app_Assist.RouteCheck(true, this,
            function ()
            {
                //객체 생성
                GlobalStatic.Page_Now = new Admin();
            });
    });


    this.get(FS_Url.Home, function ()
    {
        app_Assist.RouteCheck(false, this,
            function ()
            {
                //객체 생성
                GlobalStatic.Page_Now = new Home();
            });
    });


    this.get(FS_Url.MyPage, function ()
    {
        //사인인 필수 페이지
        app_Assist.RouteCheck(true, this,
            function ()
            {
                //객체 생성
                GlobalStatic.Page_Now = new MyPage();
            });
    });



    this.get(FS_Url.Test01, function () 
    {
        app_Assist.RouteCheck(false, this,
            function ()
            {
                //객체 생성
                GlobalStatic.Page_Now = new Test01();
            });
    });

    this.get(FS_Url.Test02, function () 
    {
        app_Assist.RouteCheck(false, this,
            function ()
            {
                //객체 생성
                GlobalStatic.Page_Now = new Test02();
            });
    });

    this.get(FS_Url.Test_Popup, function () 
    {
        app_Assist.RouteCheck(false, this,
            function ()
            {
                //객체 생성
                GlobalStatic.Page_Now = new Test_Popup();
            });
    });

    this.get(FS_Url.SamplePage, function () 
    {
        app_Assist.RouteCheck(false, this,
            function ()
            {
                //객체 생성
                GlobalStatic.Page_Now = new SamplePage();
            });
    });

    //this.get("#/", function () {
    //    //인덱스 페이지
    //    //$("#divMain").load("/Pages/index.html");
    //    divMain.html("홈");
    //});

    //this.get("#/param/:id", function () {
    //    //파라미터 받기
    //    var nID = this.params['id'];

    //    $("#divMain").html("넘어온 파라미터 id : " + nID);
    //});

    //에러 처리용
    this.ErrorFun = function (sCode)
    {
        var sCodeTemp = sCode;

        //사인인 필수 페이지
        app_Assist.RouteCheck(false, this,
            function ()
            {
                //객체 생성
                GlobalStatic.Page_Now = new Error(sCodeTemp);
            });
    };

    //404
    this.notFound = function (verb, path) 
    {
        switch (GlobalStatic.SiteType)
        {
            case 0://일반
                Page.Move_Page(FS_Url.Error + "/" + "404");
                break;
            case 1://어드민 타입
                //사인인이 되어 있을때 -> 컨탠츠 영역에 출력한다.
                //사인인이 되어 있지 않을때 -> 사인인 페이지, 메시지 출력
                if (true === GlobalSign.SignIn)
                {
                    Page.Move_Page(FS_Url.Error + "/" + "404");
                }
                else
                {
                    //alert("404, 페이지를 찾지 못했습니다.");
                    GlobalStatic.MessageBox_Error(GlobalStatic.Title, "404, 페이지를 찾지 못했습니다.");
                    Page.Move_Page(FS_Url.SignIn);
                }
                break;
        }
    };
});


//어플리케이션 시작
$(function ()
{
    app.run();

    //공통 메시지 박스 초기화
    DG_MessageBox.Initialize({
        /** 크기 고정 */
        SizeFixed: true
    });
});