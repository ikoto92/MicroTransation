using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MicroTransation.Data;
using MicroTransation.Middleware;

namespace MicroTransation.Middleware
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var db = context.RequestServices.GetService<AppDbContext>();

            string token = context.Request.Headers["Authorization"].FirstOrDefault();

            if(token == null)
            {
                context.Response.StatusCode=StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized");
                return;
            }
            else
            {
                 await _next(context);
            }
        }
    }
}
public static class AuthMiddleWareExtenstions
{
    public static IApplicationBuilder UseAuth(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<AuthMiddleware>();
    }
}
