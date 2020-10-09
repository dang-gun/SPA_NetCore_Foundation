/**
 * 사이트 전체에서 사용되는 정보
 */
var GlobalStatic = {};

/** 프로젝트 제목 */
GlobalStatic.Title = "SPA NetCore Foundation 08";

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

/** 로댕 개체 생성 */
GlobalStatic.LoadingDG = new DG_LoadingPopup(
    {
        /** 로딩에 사용할 이미지 경로 */
        LoadingImageUrl: "/Utility/DG_LoadingPopup/DG_LoadingPopupImage.gif",
        /** 기본 출력 메시지 */
        LoadingMessageText: "Loading...",
    }); 

/** 로딩 표시  */
GlobalStatic.LoadingShow = function ()
{
    GlobalStatic.LoadingDG.Show();
};

/** 로딩 닫기 */
GlobalStatic.LoadingClose = function ()
{
    GlobalStatic.LoadingDG.Close();
};


//전역 유틸 ◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇◇

/** 데이터 바인드 */
GlobalStatic.DataBind = new DG_JsDataBind();

/** 자주쓰는 규칙 추가 */
GlobalStatic.DataBind.MatchPatternListAdd(
    "Custom"
    , {
        ":DateTimeBr": function (sOriData, sMatchString, sValue, jsonValue)
        {
            return GlobalStatic.DataBind.ReplaceAll(sOriData, sMatchString, moment(sValue).format("YYYY-MM-DD<br />hh:mm"));
        }
        , ":DateTime": function (sOriData, sMatchString, sValue, jsonValue)
        {
            return GlobalStatic.DataBind.ReplaceAll(sOriData, sMatchString, moment(sValue).format("YYYY-MM-DD hh:mm"));
        }
        , ":Time": function (sOriData, sMatchString, sValue, jsonValue)
        {
            return GlobalStatic.DataBind.ReplaceAll(sOriData, sMatchString, moment("2000-01-01 " + sValue).format("HH:mm"));
        }
        , ":Date": function (sOriData, sMatchString, sValue, jsonValue)
        {
            return GlobalStatic.DataBind.ReplaceAll(sOriData, sMatchString, moment(sValue).format("YYYY-MM-DD"));
        }
        , ":string": function (sOriData, sMatchString, sValue, jsonValue)
        {
            return GlobalStatic.DataBind.ReplaceAll(sOriData, sMatchString, dgIsObject.IsStringValue(sValue));
        }
    });