/*
 * 리스트형태의 데이터를 바인딩하는 클래스.
 * 테이블 형태
 */

/**
 * 게시판 공통 기능
 * @param {any} objTableArea
 * @param {any} sListTitle
 * @param {any} sListItme
 */
function ListBindTable(objTableArea
    , sListTitle
    , sListItme)
{
    var objThis = this;
    objThis.TableArea = objTableArea;
    objThis.ListBind_ListTitleHtml = sListTitle;
    objThis.ListBind_ListItemHtml = sListItme;

}

/** 테이블 타겟 오브젝트 */
ListBindTable.prototype.TableArea = null;

/** 타이틀로 사용할 html */
ListBindTable.prototype.ListBind_ListTitleHtml = "";
/** 타이틀로 사용할 html */
ListBindTable.prototype.ListBind_ListItemHtml = "";


ListBindTable.prototype.ListBind_CssName = "ListBindTable";
ListBindTable.prototype.ListBind_Title_CssName = "ListBindTable_Title";
ListBindTable.prototype.ListBind_Itme_CssName = "ListBindTable_Itme";


/**
 * 리스트를 다시 그려준다.
 * @param {any} jsonTitle
 */
ListBindTable.prototype.BindTitle = function (jsonTitle)
{
    var objThis = this;

    //기존 타이틀 지우기
    var domFind = objThis.TableArea.find("." + objThis.ListBind_Title_CssName);
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
    //개체로 만들기
    divTemp = $(sHtmlTemp);
    divTemp.addClass(objThis.ListBind_Title_CssName);
    //대상에 추가
    objThis.TableArea.append(divTemp);
};


/**
 * 리스트를 다시 그려준다.
 * @param {any} arrJsonData
 */
ListBindTable.prototype.BindItem = function (arrJsonData)
{
    var objThis = this;

    //기존 아이템 지우기
    var domFind = objThis.TableArea.find("." + objThis.ListBind_Itme_CssName);
    //objThis.TableArea.remove(domFind);
    domFind.remove();


    //리스트 html을 임시 저장
    var sHtmlTemp = "";
    var divTemp = null;

    //var divItmeTemp = $("<tbody class='ListBind_ItemArea'></tbody>");
    var divItmeTemp = "";

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
        divTemp.addClass(objThis.ListBind_Itme_CssName);
        //대상에 추가
        objThis.TableArea.append(divTemp);    
    }

    

};

