[ASP.NET Core] .NET Core로 구현한 SPA(Sigle Page Applications)(7)- 부트스트랩, 무료 템플릿 추가
https://blog.danggun.net/7437

프로젝트 구성 정보
Visual Studio 2019 Preview(16.1.0 Preview 3.0)
.NET Core 2.2
js router : Sammy.js (http://sammyjs.org/ , https://github.com/quirkey/sammy)

/***********************/
요약

기능 추가용 프로젝트
이제부터 추가가능은 이 프로젝에 추가된다.

관리자 필수기능 추가
마이페이지 기능 추가
공통 유틸리티 추가
- 팝업 유틸
- 메시지 박스 유틸


/***********************/
GlobalStatic.js - GlobalStatic.SiteType = 0;

일반적인 웹처럼 사인인없이 사이트 탐색이 가능한 구조로 되어 있다.
사인인 시도시 사인인 페이지로 넘어가며 사인인이 완료되면 홈으로 다시 온다.


/***********************/
GlobalStatic.js - GlobalStatic.SiteType = 1;
/***********************/
로그인 없이는 탐색할 수 없는 구조로 되어 있다.
사인아웃을 하면 사인인 페이지로 넘어온다.

내부적인 구현은 사인인 토큰이 유효하지 않으면 해당페이지의 데이터를 주면 안된다.
이렇게 되면 강제로 해당페이지에 접속한다고 해도 어차피 데이터를 볼 수 없다.