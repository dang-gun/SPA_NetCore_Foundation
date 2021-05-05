using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Microsoft.OpenApi.Models;

using Newtonsoft.Json.Serialization;

using IdentityServer4_Custom.IdentityServer4;
using IdentityServer4_Custom.IdentityServer4.AuthRequest;

using ModelDB;
using SPA_NetCore_Foundation.Global;
using SwaggerAssist;
using EnumToClass;

namespace SPA_NetCore_Foundation
{
    /// <summary>
    /// 
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 인증서버 주소
        /// </summary>
        private string AuthUrl = "";

        /// <summary>
        /// 프로젝트 Xml 파일 경로.
        /// 프로젝트 세팅에 xml 파일 출력경로와 일치시켜준다.
        /// </summary>
        private string ProjectXmlDir
            = string.Format(@"{0}\SPA_NetCore_Foundation.xml"
                            , System.AppDomain.CurrentDomain.BaseDirectory);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="env"></param>
        public Startup(IConfiguration configuration, IHostEnvironment env)
        {
            Configuration = configuration;

            //로컬 경로 저장
            GlobalStatic.Dir_LocalRoot = env.ContentRootPath;

            //DB 커낵션 스트링 받아오기
            string sConnectStringSelect = "SpaNetCoreFoundation_mssql";
            //string sConnectStringSelect = "SpaNetCoreFoundation_sqlite";
            GlobalStatic.DBType = Configuration[sConnectStringSelect + ":DBType"];
            GlobalStatic.DBString = Configuration[sConnectStringSelect + ":ConnectionString"];

            //인증 정보
            this.AuthUrl = Configuration["AuthServer:Url"];
            GlobalStatic.TokenProc = new TokenProcess(this.AuthUrl);


            //세팅 로드
            GlobalStatic.Setting_DataProc.Setting_Load();


            //프로젝트 고유 출력 파일*************
            EnumXml exml = new EnumXml(this.ProjectXmlDir);
            string sTemp = string.Empty;
            EnumToModel etmBP_Temp = null;
        }

        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            //7. OAuth2 미들웨어(IdentityServer) 설정
            //AddCustomUserStore : 앞에서 만든 확장메소드를 추가
            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                //.AddSigningCredential()
                .AddExtensionGrantValidator<MyExtensionGrantValidator>()
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients())
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddCustomUserStore();

            //API모델을 파스칼 케이스 유지하기
            services.AddControllers().AddNewtonsoftJson(options => { options.SerializerSettings.ContractResolver = new DefaultContractResolver(); });

            //클라이언트 인증 요청 정보
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(o =>
            {
                o.Audience = "apiApp";

                //인증서버의 주소
                o.Authority = AuthUrl;
                o.RequireHttpsMetadata = false;
                //인증서버에서 선언한 권한
                o.Audience = "dataEventRecords";
            });


            //스웨거 문서정보를 생성 한다.
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "SPA NetCore Foundation API",
                        Description = "[ASP.NET Core] .NET Core로 구현한 SPA(Single Page Applications)(5) - 스웨거(Swagger) 설정 <br /> https://blog.danggun.net/https://blog.danggun.net/7690",
                        Version = "v1",
                        Contact = new OpenApiContact
                        {
                            Name = "Dang-Gun Roleeyas",
                            Email = string.Empty,
                            Url = new Uri("https://blog.danggun.net/")
                        },
                        License = new OpenApiLicense
                        {
                            Name = "MIT",
                            Url = new Uri("https://opensource.org/licenses/MIT")
                        }
                    });

                //인증UI **************************************
                c.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "로그인 후 전달받은 '엑세스 토큰(access token)'을 헤더의'Authorization'에 'Bearer access token' 형태로 담아 전달해야 합니다.",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
                //인증 필터
                c.OperationFilter<AuthenticationRequirementsOperationFilter>();
                //주석 표시기능
                c.IncludeXmlComments(string.Format(@"{0}\SPA_NetCore_Foundation08.xml", System.AppDomain.CurrentDomain.BaseDirectory));
            });

            
            services.AddDbContext<SpaNetCoreFoundationContext>();

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //3.0 api 라우트
            app.UseRouting();

            //09. OAuth2 미들웨어(IdentityServer) CROS 접근 권한 문제
            //app.UseCors(options =>
            //{
            //    //전체 허용
            //    options.AllowAnyOrigin();
            //});
            //OAuth2 미들웨어(IdentityServer) 설정
            app.UseIdentityServer();
            //인증서버 사용 설정
            app.UseAuthorization();

            //8. 프로젝트 미들웨어 기능 설정
            //웹사이트 기본파일 읽기 설정
            app.UseDefaultFiles();
            //wwwroot 파일읽기
            app.UseStaticFiles();
            //http요청을 https로 리디렉션합니다.
            //https를 허용하지 않았다면 제거 합니다.
            //https://docs.microsoft.com/ko-kr/aspnet/core/security/enforcing-ssl?view=aspnetcore-3.0&tabs=visual-studio
            app.UseHttpsRedirection();

            //인증 요청
            app.UseAuthentication();
            //에러가 났을때 Http 상태코드를 전달하기위한 설정
            app.UseStatusCodePages();


            //스웨거 미들웨어 설정
            app.UseSwagger();

            //스웨거 UI 활성화
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                //c.RoutePrefix = string.Empty;
            });


            //3.0 api 라우트 끝점
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
