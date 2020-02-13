/*
 * 내부 URL.
 * 주로 라우트 주소를 선언함
 */

var FS_Api = {};

FS_Api.Api = "/api/";

FS_Api.Sign = FS_Api.Api + "Sign/";
FS_Api.Sign_SignIn = FS_Api.Sign + "SignIn/";
FS_Api.Sign_SignOut = FS_Api.Sign + "SignOut/";
FS_Api.Sign_RefreshToAccess = FS_Api.Sign + "RefreshToAccess/";
FS_Api.Sign_AccessToUserInfo = FS_Api.Sign + "AccessToUserInfo/";

FS_Api.MyPage = FS_Api.Api + "MyPage/";
FS_Api.MyPage_MyPageInfo = FS_Api.MyPage + "MyPageInfo/";

FS_Api.Test = FS_Api.Api + "Test/";
FS_Api.Test_Test01 = FS_Api.Test + "Test01";
FS_Api.Test_Test02 = FS_Api.Test + "Test02";
