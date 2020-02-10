# SPA_NetCore_Foundation01
SPA(Sigle Page Applications)를 구현하는 것은 기획한 사람 마음입니다.<br />
이 포스팅을 읽기 전에 참고할만한 내용이 있습니다.<br />
이 포스팅은 제가 SPA를 어떻게 구성했는지를 설명하고 공유하기 위한 것입니다.<br />
<br />
<br />
## 자세한 설명
[[ASP.NET Core] .NET Core로 구현한 SPA(Single Page Applications)(1) - 기초](https://blog.danggun.net/7042)
<br />
<br />

### 프로젝트 구성 정보
<br />
Visual Studio 2019 Preview(16.1.0 Preview 3.0)<br />
.NET Core 2.2<br />
js router : Sammy.js (http://sammyjs.org/ , https://github.com/quirkey/sammy)
<br />
<br />
### 프로젝트 구조
<br />
#### 실행구조
<br />
index.html<br />
↓<br />
Sammy 라우트 생성(app.js)<br />