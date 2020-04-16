
function MyPage()
{
    GlobalStatic.PageType_Now = PageType.MyPage;

    //
    AA.get(true
        , {
            url: FS_Api.MyPage_MyPageInfo
            , success: function (jsonData)
            {
                if ("0" === jsonData.InfoCode)
                {//에러 없음
                    //페이지 공통기능 로드
                    Page.Load(function ()
                    {
                        //홈 인터페이스
                        Page.divContents.load(FS_FUrl.MyPage_MyPage
                            , function ()
                            {
                                if (false === jsonData.AdminPer)
                                {//관리자 권한이 없다.
                                    $("#liAdmin").remove();
                                }
                            });
                    });
                }
                else
                {
                    //뒤로
                    history.back();
                }
            }
            , error: function (jqXHR, textStatus, errorThrown)
            {
                console.log(jqXHR);

                alert("알수 없는 오류가 발생했습니다.");

                //뒤로
                history.back();
            }
        });

    
}