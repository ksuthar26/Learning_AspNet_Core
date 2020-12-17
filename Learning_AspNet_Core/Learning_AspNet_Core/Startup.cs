using Learning_AspNet_Core.CustomMiddleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Learning_AspNet_Core
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        private readonly IConfiguration _configration;
        public Startup(IConfiguration configration)
        {
            _configration = configration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<MiddlewareOptions>(_configration.GetSection("MiddlewareOptions"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger, IConfiguration configration)
        {
            ////Sample 1 :
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("<div> Hello World from the middleware 1 </div>");
            //});

            //app.Run((context) =>
            //{
            //    return context.Response.WriteAsync("This is sample 2");
            //});

            // //Sample 2 :

            //app.Use((context, next) =>
            //{
            //    context.Response.WriteAsync("This is Sample 1");
            //    return next();
            //});

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("<div> Hello World from the middleware 2 </div>");
            //});

            // Sample 3 :

            // app.Use((context, next) =>
            //{
            //    context.Response.WriteAsync("<div> Hello World from the middleware 1 </div>");
            //    next.Invoke();
            //    return context.Response.WriteAsync("<div> Returning from the middleware 1 </div>");
            //});

            // app.Use(async (context, next) =>
            // {
            //     await context.Response.WriteAsync("<div> Hello World from the middleware 2 </div>");
            //     //await next.Invoke();
            //     await context.Response.WriteAsync("<div> Returning from the middleware 2 </div>");
            // });

            // app.Run(async (context) =>
            // {
            //     await context.Response.WriteAsync("<div> Hello World from the middleware 3 </div>");
            // });

            // // Sample 4 :

            //app.UseMiddleware<SimpleMiddleware>();

            // // Sample 5 :            

            app.Map("/Map1", (app) =>
            {
                app.Map("/MapLevel1", (app) =>
                 {
                     app.Run(async context =>
                     {
                         await context.Response.WriteAsync("Map Test 1.1");
                     });
                 });
                app.Map("/MapLevel2", (app) =>
                {
                    app.Run(async context =>
                    {
                        await context.Response.WriteAsync("Map Test 1.2");
                    });
                });
            });

            app.Map("/Map2", app =>
             {
                 app.Run(async context =>
                 {
                     await context.Response.WriteAsync("Map Test 2");
                 });
             });

            app.Map("/Map3", HandleMapTest3);

            app.MapWhen(context => context.Request.Query.ContainsKey("branch"), HandleMapTest3);

            app.UseWhen(context => context.Request.Query.ContainsKey("branch"), HandleBranch);

            app.UseWhen(context => context.Request.Query.ContainsKey("branch"),
                        appbuilder => HandleBranchAndRejoin(appbuilder, logger));

            var middlewareOptions = configration.GetSection("MiddlewareOptions").Get<MiddlewareOptions>();
            app.UseSimpleMiddleware(middlewareOptions);

        }

        private static void HandleMapTest3(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Map Test 3");
            });
        }

        private static void HandleBranch(IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                var branchVer = context.Request.Query["branch"];
                await next();
                await context.Response.WriteAsync($"Branch used responce = {branchVer}");
            });
        }

        private void HandleBranchAndRejoin(IApplicationBuilder app, ILogger<Startup> logger)
        {
            app.Use(async (context, next) =>
            {
                var branchVer = context.Request.Query["branch"];
                logger.LogInformation("Branch used = {branchVer}", branchVer);

                // Do work that doesn't write to the Response.
                await next();
                // Do other work that doesn't write to the Response.
                logger.LogInformation("Branch used xcvxcvxcv = {branchVer}", branchVer);
            });
        }
    }

    public class MiddlewareOptions
    {
        public string Param1 { get; set; }

        public string Param2 { get; set; }
    }
}
