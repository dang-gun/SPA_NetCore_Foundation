using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using SPA_NetCore_Foundation.Global;

namespace SPA_NetCore_Foundation
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
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
                o.Authority = GlobalStatic.sAuthUrl;
                o.RequireHttpsMetadata = false;
                //인증서버에서 선언한 권한
                o.Audience = "dataEventRecords";
            });
        }

        // This method gets called by the runtime.
        //Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days.
                //You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            //09. OAuth2 미들웨어(IdentityServer) CROS 접근 권한 문제
            //app.UseCors(options =>
            //{
            //    //전체 허용
            //    options.AllowAnyOrigin();
            //});

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

            app.UseMvc();
        }
    }
}
