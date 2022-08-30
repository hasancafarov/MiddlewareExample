using System.Diagnostics;

namespace MiddlewareExample.CustomMiddleware
{
    public class StatsLoggerMiddleware
    {
        private readonly RequestDelegate NextMiddleware;

        public StatsLoggerMiddleware(RequestDelegate nextMiddleware)
        {
            NextMiddleware = nextMiddleware;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //1 - Inspect the request
            if (context.Request.Headers.ContainsKey("Debug"))
            {
                Console.WriteLine($"Got request. Method={context.Request.Method} Path={context.Request.Path}");

                var sw = Stopwatch.StartNew();

                //2 - Call the next middleware
                await NextMiddleware(context);

                //3 - Inspect the response
                sw.Stop();
                Console.WriteLine($"Request finished. Method={context.Request.Method} Path={context.Request.Path} StatusCode={context.Response.StatusCode} ElapsedMilliseconds={sw.ElapsedMilliseconds}");
            }
        }
    }

}
