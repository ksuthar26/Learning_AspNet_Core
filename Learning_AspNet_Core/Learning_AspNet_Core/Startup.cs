using Learning_AspNet_Core.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Add(new ServiceDescriptor(typeof(IStudentRepository), new StudentRepository())); // by default singleton
            services.Add(new ServiceDescriptor(typeof(IStudentRepository), typeof(StudentRepository), ServiceLifetime.Scoped));    // Scoped
            services.Add(new ServiceDescriptor(typeof(IStudentRepository), typeof(StudentRepository), ServiceLifetime.Singleton)); // singleton
            services.Add(new ServiceDescriptor(typeof(IStudentRepository), typeof(StudentRepository), ServiceLifetime.Transient)); // Transient

            //Application Service
            services.AddSingleton<IStudentRepository, StudentRepository>();
            services.AddSingleton(typeof(IStudentRepository), typeof(StudentRepository));
            services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient(typeof(IStudentRepository), typeof(StudentRepository));
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped(typeof(IStudentRepository), typeof(StudentRepository));

            services.AddMvc(option => option.EnableEndpointRouting = false);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }            

            app.UseRouting();

            app.UseMvcWithDefaultRoute();

            //app.UseMvc((routes) =>
            //{
            //    //routes.MapRoute("default", "{controller=Home}/{action=index}");
            //    //routes.MapRoute("default", "{Admin}/{controller=Home}/{action=index}");
            //    //routes.MapRoute("default", "admin/{controller=Home}/{action=index}");
            //    //routes.MapRoute("default", "{controller=Home}/{action=index}/{id?}");

            //    //routes.MapRoute("secure",
            //    //      "secure",
            //    //      new { Controller = "Home", Action = "Index" });

            //    //routes.MapRoute("default",
            //    //                 "{controller}/{action}/{id?}",
            //    //                 new { controller = "Student", action = "Index" }
            //    //               );

            //    routes.MapRoute("default1", "{controller=Home}/{action=index}/{id:int?}");

            //    //routes.MapRoute("default",
            //    //        "post/{id:int}",
            //    //        new { controller = "Home", action = "PostsByID" });

            //    //routes.MapRoute("anotherRoute",
            //    //                "post/{id:alpha}",
            //    //                new { controller = "Home", action = "PostsByPostName" });
            //});

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Failed to Find Route");
            });
        }
    }
}
