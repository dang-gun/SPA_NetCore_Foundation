function BoardAuth(sCheckHtml)
{
    var objThis = this;

    objThis.CheckHtml = sCheckHtml;
    objThis.BoardAuthorityTypeKey = Object.keys(objThis.BoardAuthorityType);
}

/** 게시판 권한 구분 */
BoardAuth.prototype.BoardAuthorityType =
{
    /**
    없음
    */
    None: 0,

    /**
    리스트 보기 권한.
    */
    ReadList: 1 << 0,
    /**
    리스트 보기 권한.(비회원)
    */
    ReadListNonMember: 1 << 1,
    /**
    읽기 권한.
    리스트 보기권한이 없는 상태로 이 권한이 있으면
    직접 주소를 넣어야 접근 가능하다.
    */
    Read: 1 << 2,
    /** 비회원은 읽기 가능 */
    ReadNonMember: 1 << 3,

    /**
    쓰기 권한.
    */
    Write: 1 << 4,
    /**
    댓글 쓰기 권한
    */
    WriteReply: 1 << 5,
    /** 비회원은 쓰기 가능 */
    WriteNonMember: 1 << 6,
    /** 비회원은 댓글 쓰기 가능 */
    WriteReplyNonMember: 1 << 7,

    /**
    지우기 권한.
    자기 글만 지울 수 있는 권한이다.
    */
    Delete: 1 << 8,
    /**
    다른 사람 글을 지울수 있는지 권한
    */
    DeleteOther: 1 << 9,

    /**
    수정 권한.
    자기글만 수정할 수 있다.
    */
    Edit: 1 << 10,
    /**
    다른사람 글을 수정할 수 있는 권한.
    */
    EditOther: 1 << 11,

    /** 공지 - 전체글 작성권한 */
    NoticeAll: 1 << 12,
    /** 공지 - 전체글 작성권한 */
    NoticeGroup: 1 << 13,
    /** 공지 - 게시판 공지 작성 권한 */
    NoticeBoard: 1 << 14,
};


/** 게시판 권한 구분 - 이름 */
BoardAuth.prototype.BoardAuthorityTypeName = {
    None: "없음",

    ReadList: "리스트 보기",
    ReadListNonMember: "리스트 보기(비회원)",
    Read: "읽기",
    ReadNonMember: "읽기(비회원)",

    Write: "쓰기",
    WriteReply: "댓글 쓰기",
    WriteNonMember: "쓰기(비회원)",
    WriteReplyNonMember: "댓글 쓰기(비회원)",

    Delete: "지우기",
    DeleteOther: "지우기(다른사람)",

    Edit: "수정",
    EditOther: "수정(다른사람)",

    NoticeAll: "공지(전체)",
    NoticeGroup: "공지(그룹)",
    NoticeBoard: "공지(게시판)",
};

/** 체크 박스 생성에 사용할 Html */
BoardAuth.prototype.CheckHtml = "";

/** 게시판 권한 구분 - 이름 */
BoardAuth.prototype.BoardAuthorityTypeKey = null;

BoardAuth.prototype.InputHtml = function (nTypeIndexId, nTypeValue)
{
    var objThis = this;
    var sReturn = "";

    //이름 만들기
    var sCheckName = "BoardAuthorityType" + nTypeIndexId;


    //그룹 정보를 넣는다.
    var sCheckHtmlTemp = objThis.CheckHtml.replace(/{{GroupName}}/g, sCheckName);

    sReturn
        = DG_FlagEnum.ToHtml(
            sCheckHtmlTemp
            , objThis.BoardAuthorityType
            , objThis.BoardAuthorityTypeName
            , nTypeValue
        );

    sReturn = "<div id='divBoardAuthorityType" + nTypeIndexId + "'>"
        + sReturn + "</div>";

    return sReturn;
};

/**
 * html에서 체크박스의 값을 정리하여 완성된 플래그값을 리턴한다.
 * @param {int} nBoardAuthorityId 아이템을 구분할 아이디값
 * @returns {BoardAuthorityType} 완성된 게시판 권한 플래그
 */
BoardAuth.prototype.GetHtmlAuth = function (nBoardAuthorityId)
{
    var objThis = this;


    //찾을 이름
    var sCheckName = "BoardAuthorityType" + nBoardAuthorityId;

    var typeAuth = DG_FlagEnum.GetCheckBoxData(sCheckName);

    return typeAuth;
};