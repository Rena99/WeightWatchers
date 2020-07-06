using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Subscriber.WebApi.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class MiddlewareWeightWatchers
    {
        private readonly RequestDelegate _next;

        public MiddlewareWeightWatchers(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
               await HandleExceptionAsync(httpContext, e); ;
            }
            //return _next(httpContext);
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError; // 500 if unexpected
            var result = JsonConvert.SerializeObject(new { error = ex.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class MiddlewareWeightWatchersExtensions
    {
        public static IApplicationBuilder UseMiddlewareWeightWatchers(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MiddlewareWeightWatchers>();
        }
    }
}
