
function Home()
{
    GlobalStatic.PageType_Now = PageType.Home;

    //페이지 공통기능 로드
    Page.Load(function () {
        //홈 인터페이스
        Page.divContents.load(FS_FUrl.Home_Home);
    });
}