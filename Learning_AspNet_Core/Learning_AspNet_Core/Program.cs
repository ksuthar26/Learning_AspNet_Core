using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Learning_AspNet_Core
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                // .UseContentRoot(Path.Combine(Directory.GetCurrentDirectory(), "MyDirectory"))
                .ConfigureHostConfiguration(configHost =>
                {
                    //configHost.Sources.Clear();

                    configHost.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "MyHost"));
                    configHost.AddJsonFile("hostsettings.json", optional: true);
                    //configHost.AddEnvironmentVariables(prefix: "PREFIX_");
                    //configHost.AddCommandLine(args);
                })
                .UseEnvironment("Development 123")
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "MyHost"));
                    config.AddJsonFile("MySettings.json", optional: true, reloadOnChange: true);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {

                    var config = new ConfigurationBuilder()
                                     .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "MyHost"))
                                     .AddJsonFile("hostsettings.json", optional: true)
                                     .AddCommandLine(args)
                                     .Build();

                    //webBuilder.CaptureStartupErrors(true);                    
                    webBuilder.UseSetting("APPLICATIONNAME", "CustomApplicationName");
                    // webBuilder.UseContentRoot(Path.Combine(Directory.GetCurrentDirectory(), "MyContent"));
                    // webBuilder.UseWebRoot(Path.Combine(Directory.GetCurrentDirectory(), "MyWeb"));
                    webBuilder.UseEnvironment("gfdfgf");   
                    //webBuilder.UseConfiguration(config);

                    webBuilder.UseStartup<Startup>();
                });
        }

    }
}
