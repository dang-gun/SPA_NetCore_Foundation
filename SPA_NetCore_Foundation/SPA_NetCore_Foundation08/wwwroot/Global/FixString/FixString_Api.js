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


FS_Api.Test = FS_Api.Api + "Test/";
FS_Api.Test_Test01 = FS_Api.Test + "Test01";
FS_Api.Test_Test02 = FS_Api.Test + "Test02";
