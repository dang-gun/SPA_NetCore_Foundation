function BoardPostCategory()
{
    var objThis = this;
}

/** 게시판 권한 구분 - 이름 */
BoardPostCategory.prototype.BoardAuthorityTypeKey = null;

/**
 * 
 * @param {any} nBoardId
 * @param {any} nBoardAuthority
 */
BoardPostCategory.prototype.InputHtml = function (
    nBoardId
    , nBoardAuthority)
{
    var objThis = this;
    var sReturn = "";


    return sReturn;
};

/**
 * html에서 체크박스의 값을 정리하여 완성된 플래그값을 리턴한다.
 * @param {int} nBoardAuthorityId 아이템을 구분할 아이디값
 * @returns {BoardAuthorityType} 완성된 게시판 권한 플래그
 */
BoardPostCategory.prototype.GetHtmlValue = function (nBoardAuthorityId)
{
    var objThis = this;
    var nReturn = 0;
    
    return nReturn;
};