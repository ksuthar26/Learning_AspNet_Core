using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learning_AspNet_Core.CustomMiddleware
{
    public static class SomeMiddlewareExtensions
    {
        public static IApplicationBuilder UseSimpleMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SimpleMiddleware>();
        }

        public static IApplicationBuilder UseSimpleMiddleware(this IApplicationBuilder builder, MiddlewareOptions middlewareOptions)
        {
            return builder.UseMiddleware<SimpleMiddleware>(new OptionsWrapper<MiddlewareOptions>(middlewareOptions));
        }
    }
}
