function BoardPostState()
{
}
/** BoardPostStateType.cs 참고 */
BoardPostState.prototype.BoardPostStateType =
{
    /** 정상 */
    Normal: 1,

    /** 관리자에 의해 블럭됨 */
    Block: 3,

/** 전체 공지 */
    Notice_All: 1000,
/** 그룹 공지 */
    Notice_Group: 1001,
/** 해당 게시판 공지 */
    Notice_Board: 1002,
};

/**
 * 
 * @param {any} nBoardId
 * @param {any} jsonData
 * @returns {string} 완성된 html 문자열
 */
BoardPostState.prototype.InputHtml = function (
    nBoardId
    , jsonData)
{
    var objThis = this;
    var sReturn = "";

    var sOpthtml = "";

    //공지권한 옵션
    if (true === jsonData.NoticeAll)
    {//공지 - 전체
        sOpthtml += "<option value='" + objThis.BoardPostStateType.Notice_All + "'>전체 공지</option>";
    }
    if (true === jsonData.NoticeGroup)
    {//공지 - 전체
        sOpthtml += "<option value='" + objThis.BoardPostStateType.Notice_Group + "'>게시판 그룹 공지</option>";
    }
    if (true === jsonData.NoticeBoard)
    {//공지 - 전체
        sOpthtml += "<option value='" + objThis.BoardPostStateType.Notice_Board + "'>게시판 공지</option>";
    }
    
    if ("" !== sOpthtml)
    {//옵션값이 있다.
        sReturn += "<select id='selectBoardPostState" + nBoardId
                    + "' value='" + jsonData.PostState + "'>";
        //일반 타입을 추가해준다.
        sReturn += "<option value='" + objThis.BoardPostStateType.Normal + "'>일반</option>";
        //완성된 옵션 추가
        sReturn += sOpthtml;
        sReturn += "</select>";
    }

    return sReturn;
};

/**
 * html에서 체크박스의 값을 정리하여 완성된 플래그값을 리턴한다.
 * @param {int} nBoardId 아이템을 구분할 아이디값
 */
BoardPostState.prototype.GetHtmlValue = function (nBoardId)
{
    var objThis = this;
    //기본값은 노멀
    var nReturn = objThis.BoardPostStateType.Normal;

    //찾을 이름 만들기
    var sSelectBoardPostState = $("#selectBoardPostState" + nBoardId).val();

    if (typeof sSelectBoardPostState !== "undefined")
    {//값이 있다.
        nReturn = dgIsObject.IsIntValue(sSelectBoardPostState);
    }
    
    
    return nReturn;
};