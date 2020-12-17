using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learning_AspNet_Core.CustomMiddleware
{
    public class SimpleMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _config;
        private readonly MiddlewareOptions middlewareOptions;
        public SimpleMiddleware(RequestDelegate next, IWebHostEnvironment env, IConfiguration config, IOptions<MiddlewareOptions> options)
        {
            _next = next;
            _env = env;
            _config = config;
            middlewareOptions = options.Value;
        }
        public async System.Threading.Tasks.Task Invoke(HttpContext context)
        {
            await context.Response.WriteAsync("<div> Hello from Simple Middleware </div>");
            if (_env.IsDevelopment())
            {
                await context.Response.WriteAsync($"<div> Values from configartion : {_config["SampleKey"]}</div>");
            }
            else
            {
                await _next(context);
            }
        }


    }
}
