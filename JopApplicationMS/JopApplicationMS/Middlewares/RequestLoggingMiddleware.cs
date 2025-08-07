using Serilog;
using System.Diagnostics;

namespace JopApplicationMS.API.Middlewares
{
    public class RequestLoggingMiddleware
    {


        private readonly RequestDelegate _next;

        public RequestLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var stopwatch = Stopwatch.StartNew();
            var request = context.Request;

            // Log before request
            Log.Information("➡️ HTTP {Method} {Path} started", request.Method, request.Path);

            await _next(context);

            stopwatch.Stop();
            var response = context.Response;

            // Log after request
            Log.Information("✅ HTTP {Method} {Path} {Body} finished with {StatusCode} in {Elapsed} ms",
                request.Method,
                request.Path,
                response.StatusCode,
                stopwatch.ElapsedMilliseconds);
        }

    }
}
