/*
 * 리스트형태의 데이터를 바인딩하는 클래스.
 * 
 * 타이틀 ************
 * 정렬기능--- 타이틀 html에
 * 클래스에 'ListBindTitle_Sort'를 추가한다.
 * 속성에 sortValue를 추가한다.
 * 
 */

/**
 * 리스트 공통 기능
 * @param {any} jsonOtpion
 */
function ListBind(jsonOtpion)
{
    var objThis = this;

    //옵션 저장
    objThis.jsonOpt = Object.assign({}, objThis.jsonOptionDefult, jsonOtpion);


    //자주쓰는 변수 백업
    objThis.TableArea = objThis.jsonOpt.TableArea;
    objThis.ListBind_ListTitleHtml = objThis.jsonOpt.ListBind_ListTitleHtml;
    objThis.ListBind_ListItemHtml = objThis.jsonOpt.ListBind_ListItemHtml;

    //정렬기능을 사용할때 기본값
    objThis.Now_SortValue = objThis.jsonOpt.SortValueDefult;
}



/** 테이블 타겟 오브젝트 */
ListBind.prototype.TableArea = null;

/** 타이틀로 사용할 html */
ListBind.prototype.ListBind_ListTitleHtml = "";
/** 타이틀로 사용할 html */
ListBind.prototype.ListBind_ListItemHtml = "";


/** 테이블 타겟 오브젝트 */
ListBind.prototype.TableArea = null;


/** 정렬 관련 - 지금 선택된 정렬값 */
ListBind.prototype.Now_SortValue = "";
/** 정렬 관련 - 지금 선택된 정렬 방향. 1:asc, 2:desc  */
ListBind.prototype.Now_Direction = 0;

/** 타이틀 생성 데이터의 기본값 */
ListBind.prototype.jsonOptionDefult =
{
    /** 사용할 영역 */
    TableArea: null,
    /** 타이틀로 사용할 html */
    ListBind_ListTitleHtml: "",
    /** 아이템으로 사용할 html */
    ListBind_ListItemHtml:"",

    /** 
     *  제목을 이용한 정렬기능 사용 여부 
     *  타이틀 html의
     *  클래스에 'ListBindTitle_Sort'를 추가한다.
     *  속성에 sortValue를 추가해야 동작한다.
     */
    TitleSort: false,

    /** 정렬 데이터 기본값 */
    SortValueDefult: "",

    /**
     * 타이틀 클리시 할 동작
     * @param {any} objSortValue 지정된 sortValue
     * @param {any} nDirection 선택된 방향
     */
    OnClick: function (objSortValue, nDirection)
    {
    }
};

/** 완성된 타이틀 생성 옵션 */
ListBind.prototype.jsonOpt = {};


/**
 * 리스트를 다시 그려준다.
 * @param {any} jsonTitle
 */
ListBind.prototype.BindTitle = function (jsonTitle)
{
    var objThis = this;

    //기존 타이틀 지우기
    var domFind = objThis.TableArea.find(".ListBind_Title");
    domFind.remove();

    //리스트 html을 임시 저장
    var sHtmlTemp = "";
    var divTemp = null;

    //타이틀 그리기**********************
    sHtmlTemp
        = GlobalStatic.DataBind.DataBind_All(
            objThis.ListBind_ListTitleHtml
            , jsonTitle)
            .ResultString;


    //개체로 만들기***************************
    divTemp = $(sHtmlTemp);
    divTemp.addClass("ListBind_Title");

    //이벤트 연결*****************************
    if (true === objThis.jsonOpt.TitleSort)
    {//타이틀 정렬을 사용한다.

        //이벤트 연결할 대상 찾기
        var domTitleSort = divTemp.find(".ListBindTitle_Sort");

        domTitleSort.click(function (event)
        {
            var objThis_Btn = $(this);

            
            //정렬 선택값 받기
            var sSortValue = objThis_Btn.attr("sortValue");
            //정렬 선택값 백업
            objThis.Now_SortValue = sSortValue;

            
            //정렬 방향 바꾸기
            if (2 === objThis.Now_Direction)
            {//desc->asc
                objThis.Now_Direction = 1;
            }
            else
            {//asc->desc
                objThis.Now_Direction = 2;
            }

            //css 입력
            objThis.DirectionSet(divTemp, sSortValue, objThis.Now_Direction);
            

            if (typeof objThis.jsonOpt.OnClick === "function")
            {//연결된 이벤트가 있다.
                objThis.jsonOpt.OnClick(
                    objThis.Now_SortValue
                    , objThis.Now_Direction);
            }

        });
    }
    

    //대상에 추가
    objThis.TableArea.append(divTemp);
};


/**
 * 리스트를 다시 그려준다.
 * @param {any} arrJsonData
 */
ListBind.prototype.BindItem = function (arrJsonData)
{
    var objThis = this;

    if (true === objThis.jsonOpt.TitleSort)
    {//제목 정렬 기능 사용
        //타이틀 css 변경
        objThis.DirectionSet(objThis.TableArea
            , objThis.Now_SortValue
            , objThis.Now_Direction);
    }
    

    //기존 아이템 지우기
    var domFind = objThis.TableArea.find(".ListBind_Item");
    //objThis.TableArea.remove(domFind);
    domFind.remove();


    //리스트 html을 임시 저장
    var sHtmlTemp = "";
    var divTemp = null;


    //컨탠츠 추가***************
    for (var i = 0; i < arrJsonData.length; ++i)
    {
        var jsonItemData = arrJsonData[i];
        sHtmlTemp
            = GlobalStatic.DataBind.DataBind_All(
                objThis.ListBind_ListItemHtml
                , jsonItemData)
                .ResultString;

        //개체로 만들기
        divTemp = $(sHtmlTemp);
        divTemp.addClass("ListBind_Item");

        //대상에 추가
        objThis.TableArea.append(divTemp);
    }

};

/**
 * 정렬 반향 세팅
 * @param {any} domTableArea 사용할 대상 영역
 * @param {any} objSortValue 클릭된 대상의 정렬값
 * @param {any} nDirection
 */
ListBind.prototype.DirectionSet = function (domTableArea, sSortValue, nDirection)
{
    //바꿀 대상 찾기
    var domTitleSort = domTableArea.find(".ListBindTitle_Sort");

    //선택된 대상 찾기
    var domTarget = domTableArea.find("[sortValue=" + sSortValue + "]");

    //정렬 방향 변경******
    //전체 초기화
    domTitleSort.addClass("ListBindTitle_Sort_None");
    domTitleSort.removeClass("ListBindTitle_Sort_Asc");
    domTitleSort.removeClass("ListBindTitle_Sort_Desc");

    //정렬 방향 바꾸기
    if (2 === nDirection)
    {//asc->desc
        //정렬 방향에 따른 css 넣기
        domTarget.addClass("ListBindTitle_Sort_Desc");
        domTarget.removeClass("ListBindTitle_Sort_None");
    }
    else
    {//desc->asc
        //정렬 방향에 따른 css 넣기
        domTarget.addClass("ListBindTitle_Sort_Asc");
        domTarget.removeClass("ListBindTitle_Sort_None");
    }
    
};
