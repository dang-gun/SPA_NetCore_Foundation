
function DG_LoadingPopup(jsonOption)
{
    var objThis = this;
    //옵션 생성
    objThis.OptionSet(jsonOption);
}


/** 온셥 - 기본 */
DG_LoadingPopup.prototype.OptionDefult =
{
    /** 부모에 들어갈 CSS */
    LoadingPopupParentCss: "DG_LoadingPopupParentCss",
    /** 로딩 내용 영역에 들어갈 CSS */
    LoadingPopupCss: "DG_LoadingPopupCss",

    /** 로딩 팝업에 사용할 html 
     * 이미지로 사용할 대상은 class에 'DG_LoadingPopupImage'를 추가해야한다.
     * 메시지로 사용할 대상은 class에 'DG_LoadingPopupMessage'를 추가해야한다.
     */
    LoadingPopupHtml: ""
        + "<div>"
        + "<img class='DG_LoadingPopupImage' src='' /><br />"
        + "<span class='DG_LoadingPopupMessage'></span>"
        + "</div>",
    /** 로딩에 사용할 이미지 경로 */
    LoadingImageUrl: "DG_LoadingPopupImage.gif",
    /** 로딩에 사용할 CSS */
    LoadingImageCss: "",

    /** 기본 출력 메시지 */
    LoadingMessageText: "Loading...",

    /** 로딩에 오버레이에 사용할 CSS */
    LoadingOverlayCss: "DG_PopupOverlayCss",

    /** 기본으로 사용할 z 인덱스 */
    ZIndex: 1000,
};

/** 옵션 - 저장된 표시 */
DG_LoadingPopup.prototype.OptionShow = {};

/** 완성된 로딩 팝업 원본 div */
DG_LoadingPopup.prototype.divLoadingPopup_Original = null;
/** 로딩 팝업 div */
DG_LoadingPopup.prototype.divLoadingPopup = null;
/** 로딩 팝업 내용 div */
DG_LoadingPopup.prototype.divLoadingPopup_Content = null;
/** 로딩 이미지 대상 */
DG_LoadingPopup.prototype.imgLoadingPopupImage = null;
/** 로딩 메시지 대상 */
DG_LoadingPopup.prototype.spanLoadingPopupMessage = null;

/** 출력할 메시지 임시 저장 */
DG_LoadingPopup.prototype.ShowMessageText = "";


/**
 * 사용할 옵션 저장.
 * 옵션이 저장되면서 로딩 팝업을 생성하기위한 준비를 해둔다.
 * @param {any} jsonOption
 */
DG_LoadingPopup.prototype.OptionSet = function (jsonOption)
{
    var objThis = this;

    //옵션 합치지
    objThis.OptionShow = Object.assign({}, objThis.OptionDefult, jsonOption);

    //부모용 div*************
    var divPopupParent = $("<div id='divDG_LoadingPopupParent'></div>");
    divPopupParent.addClass(objThis.OptionShow.LoadingPopupParentCss);

    //로딩용 div ************
    var divLoadingPopup = $("<div id='divDG_LoadingPopup' ></div>");
    divLoadingPopup.addClass(objThis.OptionShow.LoadingPopupCss);
    divLoadingPopup.html(objThis.OptionShow.LoadingPopupHtml);
    divLoadingPopup.css("z-index", objThis.OptionShow.ZIndex + 1);
    divLoadingPopup.css("position", "fixed");
    divLoadingPopup.css("top", "0px");
    divLoadingPopup.css("left", "0px");

    //로딩 이미지용 img ************
    var imgLoadingImage = divLoadingPopup.find(".DG_LoadingPopupImage");
    imgLoadingImage.attr("src", objThis.OptionShow.LoadingImageUrl);

    //로딩 메시지 임시 저장 **************
    objThis.ShowMessageText = objThis.OptionShow.LoadingMessageText;

    //로딩 메시지용 span **************
    var imgLoadingMessage = divLoadingPopup.find(".DG_LoadingPopupMessage");
    imgLoadingMessage.html(objThis.OptionShow.LoadingMessage);
    objThis.spanLoadingPopupMessage = imgLoadingMessage;


    //오버레이용 div*************
    var divOverlay = $("<div id='divDG_PopupOverlay' ></div>");
    divOverlay.addClass(objThis.OptionShow.LoadingOverlayCss);
    divOverlay.css("background", "#aaa");
    divOverlay.css("opacity", "0.3");

    divOverlay.css("position", "fixed");
    divOverlay.css("top", "0px");
    divOverlay.css("left", "0px");
    divOverlay.css("width", "100%");
    divOverlay.css("height", "100%");

    divOverlay.css("z-index", objThis.OptionShow.ZIndex);


    //div에 추가하기
    divPopupParent.append(divLoadingPopup);
    divPopupParent.append(divOverlay);

    //원본 저장
    objThis.divLoadingPopup_Original = divPopupParent;
};


/**
 * 로딩 팝업 표시
 * @param {any} sMessageText
 */
DG_LoadingPopup.prototype.Show = function (sMessageText)
{
    var objThis = this;

    //확정된 출력할 메시지
    var sOutMsgText = "";

    //출력할 메시지 있는지 확인
    if (undefined === sMessageText
        || null === sMessageText
        || "" === sMessageText)
    {//메시지가 없다.
        //기본 출력 메시지 사용
        sOutMsgText = objThis.ShowMessageText;
    }

    //원본을 클론
    objThis.divLoadingPopup
        = $(objThis.divLoadingPopup_Original[0].cloneNode(true));

    //클론된 엘리먼트 찾기 - 이미지 img
    objThis.divLoadingPopup_Content
        = objThis.divLoadingPopup
                .find(".DG_LoadingPopupCss");
    //클론된 엘리먼트 찾기 - 이미지 img
    objThis.imgLoadingPopupImage
        = objThis.divLoadingPopup
                .find(".DG_LoadingPopupImage");
    //클론된 엘리먼트 찾기 - 메시지 span
    objThis.spanLoadingPopupMessage
        = objThis.divLoadingPopup
                .find(".DG_LoadingPopupMessage");


    //메시지 출력
    objThis.spanLoadingPopupMessage.html(sOutMsgText);
    

    //바디 끝에 사용할 div를 만들어 준다.
    $("body").append(objThis.divLoadingPopup);


    //중앙값을 계산한다.
    var nTopCenter
        = ($(window).height() / 2)
            - (objThis.divLoadingPopup_Content.height() / 2)
            + objThis.divLoadingPopup_Content.position().top;
    objThis.divLoadingPopup_Content.css("top", nTopCenter + "px");
    //중앙값을 계산한다.
    var nLeftCenter
        = ($(window).width() / 2)
            - (objThis.divLoadingPopup_Content.width() / 2)
            + objThis.divLoadingPopup_Content.position().left;
    objThis.divLoadingPopup_Content.css("left", nLeftCenter + "px");
};

/**
 * 로딩이 출력중에 메시지를 설정한다.
 * @param {any} sMessageText
 */
DG_LoadingPopup.prototype.ShowMessage = function (sMessageText)
{
    var objThis = this;

    //확정된 출력할 메시지
    var sOutMsgText = "";

    //출력할 메시지 있는지 확인
    if (undefined === sMessageText
        || null === sMessageText
        || "" === sMessageText)
    {//메시지가 없다.
        //기본 출력 메시지 사용
        sOutMsgText = objThis.ShowMessageText;
    }
    else
    {
        sOutMsgText = sMessageText;
    }

    //메시지 출력
    objThis.spanLoadingPopupMessage.html(sOutMsgText);

};

DG_LoadingPopup.prototype.Close = function ()
{
    var objThis = this;

    objThis.divLoadingPopup.remove();
};