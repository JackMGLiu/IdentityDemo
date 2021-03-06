﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityTest.Middleware;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityTest
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/Login";
                    options.AccessDeniedPath = "/Account/Denied";
                    //options.AccessDeniedPath = new PathString("/Account/Denied");
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.Map("/task", t =>
            //{
            //    t.Run(async (context) =>
            //    {
            //        await context.Response.WriteAsync("this is a task！");
            //    });
            //});

            app.UseAuthentication();

            app.UseStaticFiles();

            ////添加权限中间件, 一定要放在app.UseAuthentication后
            app.UsePermission(new PermissionMiddlewareOption()
            {
                LoginAction = @"/login",
                NoPermissionAction = @"/denied",
                //这个集合从数据库中查出所有用户的全部权限
                UserPerssions = new List<UserPermission>()
                {
                    new UserPermission {Url = "/", UserName = "gsw"},
                    new UserPermission {Url = "/home/contact", UserName = "gsw"},
                    new UserPermission {Url = "/home/about", UserName = "aaa"},
                    new UserPermission {Url = "/", UserName = "aaa"}
                }
            });

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("1：ext first！");
            //    await next.Invoke();
            //});

            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("2：ext second！");
            //    await next.Invoke();
            //});

            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("3：ext third！");
            //});
        }
    }
}
