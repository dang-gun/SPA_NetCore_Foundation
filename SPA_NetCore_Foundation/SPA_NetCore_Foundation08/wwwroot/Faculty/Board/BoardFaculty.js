function BoardFaculty(sCheckHtml)
{
    var objThis = this;

    objThis.CheckHtml = sCheckHtml;
    objThis.BoardFacultyTypeKey = Object.keys(objThis.BoardFacultyType);
}

/** 게시판 권한 구분 */
BoardFaculty.prototype.BoardFacultyType =
{
    /**
    없음
    */
    None: 0,

    /**
    한페이지 컨탠츠 수 - 서버 데이터 사용.
    */
    ShowCount_Server: 1 << 0,
    /**
    리플 리스트를 표시할지 여부
    */
    ReplyList: 1 << 1,

    /**
    자기리스트만 보이는 게시판인지 여부
    */
    MyList: 1 << 2,

};


/** 게시판 권한 구분 - 이름 */
BoardFaculty.prototype.BoardFacultyTypeName = {
    None: "없음",

    ShowCount_Server: "한페이지 컨탠츠 수(서버 데이터 사용)",
    ReplyList: "리플 리스트를 표시할지 여부",

    MyList: "자신의 리스트만 표시할지 여부",
};

/** 체크 박스 생성에 사용할 Html */
BoardFaculty.prototype.CheckHtml = "";

/** 게시판 권한 구분 - 이름 */
BoardFaculty.prototype.BoardFacultyTypeKey = null;

BoardFaculty.prototype.InputHtml = function (nTypeIndexId, nTypeValue)
{
    var objThis = this;
    var sReturn = "";

    //이름 만들기
    var sCheckName = "BoardFacultyType" + nTypeIndexId;

    
    //그룹 정보를 넣는다.
    var sCheckHtmlTemp = objThis.CheckHtml.replace(/{{GroupName}}/g, sCheckName);

    sReturn
        = DG_FlagEnum.ToHtml(
            sCheckHtmlTemp
            , objThis.BoardFacultyType
            , objThis.BoardFacultyTypeName
            , nTypeValue
        );

    sReturn = "<div id='divBoardFacultyType" + nTypeIndexId + "'>"
        + sReturn + "</div>";

    return sReturn;
};

/**
 * html에서 체크박스의 값을 정리하여 완성된 플래그값을 리턴한다.
 * @param {int} nBoardAuthorityId 아이템을 구분할 아이디값
 * @returns {BoardFacultyType} 완성된 게시판 권한 플래그
 */
BoardFaculty.prototype.GetHtmlData = function (nBoardAuthorityId)
{
    //찾을 이름
    var sCheckName = "BoardFacultyType" + nBoardAuthorityId;

    var typeAuth = DG_FlagEnum.GetCheckBoxData(sCheckName);

    return typeAuth;
};