
function TestBoardRoot()
{
    GlobalStatic.PageType_Now = this.constructor.name;

    var objThis = this;

    //페이지 공통기능 로드
    Page.Load({}, function ()
    {
        //화면 인터페이스
        Page.divContents.load("/Pages/Test/TestBoardRoot.html"
            , function () {
                objThis.divList = $("#divList");

                //게시판 기능초기화 및 게시판 표시
                objThis.BoardComm
                    = new BoardCommon(objThis.BoardID, true
                        , {
                            TableArea: objThis.divList,
                            FileUrl_Body: BoardCA.FileUrl.Body,
                            FileUrl_Title: BoardCA.FileUrl.Title,
                            FileUrl_ListItem: BoardCA.FileUrl.ListItem,
                            FileUrl_PostView: BoardCA.FileUrl.PostView,

                            PostView_List: true
                        }
                        , function ()
                        {
                        });
            });
    });
}


/** 게시판으로 사용할 영역 */
TestBoardRoot.prototype.divList = null;

/** 사용할 게시판 아이디 */
TestBoardRoot.prototype.BoardID = 1;
/** 게시판 공통 */
TestBoardRoot.prototype.BoardComm = null;

TestBoardRoot.prototype.Action = function ()
{
    BoardCA.Action();
};