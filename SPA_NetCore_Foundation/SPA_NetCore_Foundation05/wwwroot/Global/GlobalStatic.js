/**
 * 사이트 전체에서 사용되는 정보
 */
var GlobalStatic = {};

/** 프로젝트 제목 */
GlobalStatic.Title = "SPA NetCore Foundation 05";


/** 디버그 여부 */
GlobalStatic.Debug = false;

/** 
 *  사이트 타입
 * 0 : 일반적인 홈페이지(로그인없이 탐색 가능)
 * 1 : 어드민 타입(로그인없이 탐색 불가능)
 */
GlobalStatic.SiteType = 0;

//현재 보고있는 페이지의 인스턴스
GlobalStatic.Page_Now = null;
//현재 보고있는 페이지의 구분용 타이틀
GlobalStatic.PageType_Now = "";


//전역 함수 ◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇

/**
 * 단순 메시지 박스 - 에러
 * @param {string} sTitle 출력할 메시지
 * @param {string} sMsg 출력할 메시지
 */
GlobalStatic.MessageBox_Error = function (sTitle, sMsg)
{
    var sTitleTemp = sTitle;

    if ("" === sTitleTemp)
    {
        sTitleTemp = GlobalStatic.Title;
    }

    DG_MessageBox.Show({
        Title: sTitleTemp,
        Content: sMsg,

        ButtonShowType: DG_MessageBox.ButtonShowType.Ok,
        BigIconType: DG_MessageBox.BigIconType.Error,
        ButtonEvent: function (btnType)
        {
            DG_Popup.Close();
        }
    });
};

/**
 * 단순 메시지 박스 - 인포
 * @param {string} sTitle 출력할 메시지
 * @param {string} sMsg 출력할 메시지
 */
GlobalStatic.MessageBox_Info = function (sTitle, sMsg)
{
    var sTitleTemp = sTitle;

    if ("" === sTitleTemp)
    {
        sTitleTemp = GlobalStatic.Title;
    }

    DG_MessageBox.Show({
        Title: sTitleTemp,
        Content: sMsg,

        ButtonShowType: DG_MessageBox.ButtonShowType.Ok,
        BigIconType: DG_MessageBox.BigIconType.Info,
        ButtonEvent: function (btnType)
        {
            DG_Popup.Close();
        }
    });
};


//전역 유틸 ◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇

/** 데이터 바인드 */
GlobalStatic.DataBind = new DG_JsDataBind();