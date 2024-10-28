using MicroTransation.Middlwere;

namespace MicroTransation.Middlwere
{
    public class AuthMiddlwere
    {
        private readonly RequestDelegate _next;

        public AuthMiddlwere (RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine("hello from middle");
            await _next(context);
        }
    }
  }
public static class AuthMiddleWareExtenstions
{
    public static IApplicationBuilder UseAuth(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<AuthMiddlwere>();
    }
}

