/*
 * 내부 URL.
 * 주로 라우트 주소를 선언함
 */

var FS_Api = {};

FS_Api.Api = "/api/";

FS_Api.Sign = FS_Api.Api + "Sign/";
FS_Api.Sign_SignIn = FS_Api.Sign + "SignIn";
FS_Api.Sign_SignOut = FS_Api.Sign + "SignOut";
FS_Api.Sign_RefreshToAccess = FS_Api.Sign + "RefreshToAccess";
FS_Api.Sign_AccessToUserInfo = FS_Api.Sign + "AccessToUserInfo";
FS_Api.Sign_SignEmailCheck = FS_Api.Sign + "SignEmailCheck";
FS_Api.Sign_ViewNameCheck = FS_Api.Sign + "ViewNameCheck";
FS_Api.Sign_SignUp = FS_Api.Sign + "SignUp";

/** 세팅 데이터 */
FS_Api.SettingData = FS_Api.Api + "SettingData/";
FS_Api.SettingData_SettingLoad = FS_Api.SettingData + "SettingLoad";
FS_Api.SettingData_SettingList = FS_Api.SettingData + "SettingList";
FS_Api.SettingData_SettingApply = FS_Api.SettingData + "SettingApply";
FS_Api.SettingData_SettingSet = FS_Api.SettingData + "SettingSet";


//보드 관리 □□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□
FS_Api.BoardMgt = FS_Api.Api + "BoardMgt/";
FS_Api.BoardMgt_TestPostAdd = FS_Api.BoardMgt + "TestPostAdd";
FS_Api.BoardMgt_List = FS_Api.BoardMgt + "List";
FS_Api.BoardMgt_Item = FS_Api.BoardMgt + "Item";
FS_Api.BoardMgt_Edit = FS_Api.BoardMgt + "Edit";
FS_Api.BoardMgt_Create = FS_Api.BoardMgt + "Create";
FS_Api.BoardMgt_AuthList = FS_Api.BoardMgt + "AuthList";
FS_Api.BoardMgt_AuthAdd = FS_Api.BoardMgt + "AuthAdd";
FS_Api.BoardMgt_AuthEdit = FS_Api.BoardMgt + "AuthEdit";


//보드 □□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□
FS_Api.Board = FS_Api.Api + "Board/";
FS_Api.Board_List = FS_Api.Board + "List/";
FS_Api.Board_List_Auth = FS_Api.Board + "List_Auth/";

FS_Api.Board_PostView = FS_Api.Board + "PostView/";
FS_Api.Board_PostView_Auth = FS_Api.Board + "PostView_Auth/";
FS_Api.Board_PostView = FS_Api.Board + "PostView/";
FS_Api.Board_PostView_Auth = FS_Api.Board + "PostView_Auth/";

FS_Api.Board_PostReplyList = FS_Api.Board + "PostReplyList/";
FS_Api.Board_PostReplyList_Auth = FS_Api.Board + "PostReplyList_Auth/";
FS_Api.Board_PostReReplyList = FS_Api.Board + "PostReReplyList/";
FS_Api.Board_PostReReplyList_Auth = FS_Api.Board + "PostReReplyList_Auth/";
FS_Api.Board_PostReplyCreate = FS_Api.Board + "PostReplyCreate/";
FS_Api.Board_PostReplyCreate_Auth = FS_Api.Board + "PostReplyCreate_Auth/";

FS_Api.Board_PostCreateView = FS_Api.Board + "PostCreateView/";
FS_Api.Board_PostCreateView_Auth = FS_Api.Board + "PostCreateView_Auth/";
FS_Api.Board_PostCreate = FS_Api.Board + "PostCreate/";
FS_Api.Board_PostCreate_Auth = FS_Api.Board + "PostCreate_Auth/";

FS_Api.Board_PostEditView = FS_Api.Board + "PostEditView/";
FS_Api.Board_PostEditView_Auth = FS_Api.Board + "PostEditView_Auth/";
FS_Api.Board_PostEdit = FS_Api.Board + "PostEdit/";
FS_Api.Board_PostEdit_Auth = FS_Api.Board + "PostEdit_Auth/";

FS_Api.Board_PostDeleteView = FS_Api.Board + "PostDeleteView/";
FS_Api.Board_PostDeleteView_Auth = FS_Api.Board + "PostDeleteView_Auth/";
FS_Api.Board_PostDelete = FS_Api.Board + "PostDelete/";
FS_Api.Board_PostDelete_Auth = FS_Api.Board + "PostDelete_Auth/";


FS_Api.Board_SummaryList = FS_Api.Board + "SummaryList/";


//테스트 □□□□□□□□□□□□□□□□□□□□□□□□□□□□□□□
FS_Api.Test = FS_Api.Api + "Test/";
FS_Api.Test_Test01 = FS_Api.Test + "Test01";
FS_Api.Test_Test02 = FS_Api.Test + "Test02";
