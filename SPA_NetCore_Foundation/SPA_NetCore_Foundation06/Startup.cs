using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4_Custom.IdentityServer4;
using IdentityServer4_Custom.IdentityServer4.AuthRequest;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SPA_NetCore_Foundation.Global;
using ModelDB;
using Swashbuckle.AspNetCore.Swagger;

namespace SPA_NetCore_Foundation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            //DB 커낵션 스트링 받아오기
            GlobalStatic.DBString = Configuration["ConnectionString:MSSQL_SpaNetCoreFoundation"];
        }

        public IConfiguration Configuration { get; }

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

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //클라이언트 인증 요청 정보
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(o =>
            {
                o.Audience = "apiApp";

                //인증서버의 주소
                o.Authority = GlobalStatic.AuthUrl;
                o.RequireHttpsMetadata = false;
                //인증서버에서 선언한 권한
                o.Audience = "dataEventRecords";
            });


            //스웨거 문서정보를 생성 한다.
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1"
                    , new Info 
                    { 
                        Title = "SPA NetCore Foundation API"
                        , Description = "[ASP.NET Core] .NET Core로 구현한 SPA(Single Page Applications)(5) - 스웨거(Swagger) 설정 <br /> https://blog.danggun.net/7689"
                        , Version = "v1"
                        , Contact = new Contact
                        {
                            Name = "Dang-Gun Roleeyas",
                            Email = string.Empty,
                            Url = "https://blog.danggun.net/"
                        }
                        , License = new License
                        {
                            Name = "MIT",
                            Url = "https://opensource.org/licenses/MIT"
                        }
                    });

                //인증UI **************************************
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    In = "header",
                    Description = "로그인 후 전달받은 'ExternalKey'를 헤더의'MgrExternalKey' 담아 전달해야 합니다.",
                    Name = "Authorization",
                    Type = "apiKey"
                });

                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                {
                    { "Bearer", new string[] { } }
                });
            });

            services.AddDbContext<SpaNetCoreFoundationContext>(opts =>
                opts.UseSqlServer(GlobalStatic.DBString));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            //09. OAuth2 미들웨어(IdentityServer) CROS 접근 권한 문제
            //app.UseCors(options =>
            //{
            //    //전체 허용
            //    options.AllowAnyOrigin();
            //});
            //OAuth2 미들웨어(IdentityServer) 설정
            app.UseIdentityServer();

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

            app.UseMvc();
        }
    }
}
