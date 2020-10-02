[ASP.NET Core] .NET Core로 구현한 SPA(Single Page Applications)(7) - 주요 유틸 추가 및 셈플

프로젝트 구성 정보
Visual Studio 2019 Preview(16.1.0 Preview 3.0)
.NET Core 3.1
js router : Sammy.js (http://sammyjs.org/ , https://github.com/quirkey/sammy)

/***********************/
요약
EF(Entity Framework)와 코드 퍼스트(Code First)
EF를 설치하고 코드퍼스트 방식으로 DB를 생성한다.
셈플용 DB는 SQLite로 설정함.
로그인 정보를 DB로 옮김


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