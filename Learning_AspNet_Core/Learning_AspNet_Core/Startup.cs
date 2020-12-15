using Learning_AspNet_Core.Extension;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Learning_AspNet_Core
{
    public class Startup
    {

        private IConfiguration _configartion;

        // Here we are using Dependency Injection to inject the Configuration object
        public Startup(IConfiguration config)
        {
            _configartion = config;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Way 1

            //services.Configure<PositionOptions>(_configartion.GetSection(PositionOptions.Position));
            //services.Configure<ColourOptions>(_configartion.GetSection(ColourOptions.Position));

            // Way 2

            services.AddConfig(_configartion);

            services.AddDirectoryBrowser();

            services.AddRazorPages().AddRazorPagesOptions(options =>
            {
                options.Conventions.AddPageRoute("/Configartion", "");
            }).WithRazorPagesAtContentRoot();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //DefaultFilesOptions defaultFilesOptions = new DefaultFilesOptions();
            //defaultFilesOptions.DefaultFileNames.Clear();
            //defaultFilesOptions.DefaultFileNames.Add("Custom.html");


            //app.UseDefaultFiles(defaultFilesOptions);

            //app.UseStaticFiles();

            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    FileProvider = new PhysicalFileProvider(
            //Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Css")),
            //    RequestPath = new PathString("/MyCss")
            //});

            //app.UseDirectoryBrowser();

            //app.UseDirectoryBrowser(new DirectoryBrowserOptions()
            //{
            //    FileProvider = new PhysicalFileProvider(
            //        Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\Css")),
            //    RequestPath = new PathString("/MyCss")
            //});

            //app.UseDirectoryBrowser(new DirectoryBrowserOptions()
            //{
            //    FileProvider = new PhysicalFileProvider(
            //        Path.Combine(Directory.GetCurrentDirectory(), @"MyDirectory")),
            //    RequestPath = new PathString("/MyDirectory")
            //});

            // app.UseFileServer(enableDirectoryBrowsing: true);

            // FileServerOptions fileServerOptions = new FileServerOptions();
            // fileServerOptions.DefaultFilesOptions.DefaultFileNames.Clear();
            // fileServerOptions.DefaultFilesOptions.DefaultFileNames.Add("Custom.html");

            // app.UseFileServer(fileServerOptions);

            // app.UseFileServer(new FileServerOptions()
            // {
            //     FileProvider = new PhysicalFileProvider(
            //Path.Combine(Directory.GetCurrentDirectory(), @"MyDirectory")),
            //     RequestPath = new PathString("/MyDirectory"),
            //     EnableDirectoryBrowsing = true
            // });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync(_configartion["MyCustomKey"]);
                //});
                endpoints.MapRazorPages();
            });


        }
    }
}
