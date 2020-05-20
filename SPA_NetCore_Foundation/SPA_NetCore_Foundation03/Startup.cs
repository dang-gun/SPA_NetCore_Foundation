using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using SPA_NetCore_Foundation.Global;
using Newtonsoft.Json.Serialization;

namespace SPA_NetCore_Foundation
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //API모델을 파스칼 케이스 유지하기
            services.AddControllers().AddNewtonsoftJson(options => { options.SerializerSettings.ContractResolver = new DefaultContractResolver(); });
        }

        // This method gets called by the runtime.
        //Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            //3.0 api 라우트
            app.UseRouting();

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

            
            //3.0 api 라우트 끝점
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
