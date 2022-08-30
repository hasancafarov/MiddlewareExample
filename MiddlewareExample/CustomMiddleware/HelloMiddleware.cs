using System.Diagnostics;

namespace MiddlewareExample.CustomMiddleware
{
    public class HelloMiddleware
    {
        readonly RequestDelegate _next;
        public HelloMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            Debug.WriteLine("Hello 1");
            await _next.Invoke(context);
            Debug.WriteLine("Hello 2");
        }
    }
    static public class HelloMiddlewareExtension
    {
        public static IApplicationBuilder UseHello(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HelloMiddleware>();
        }
    }
}
