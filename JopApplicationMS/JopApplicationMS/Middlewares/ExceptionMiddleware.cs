//namespace JopApplicationMS.API.Middlewares
//{
//  public class ExceptionMiddleware
//{
//    private readonly RequestDelegate _next;
//    private readonly ILogger<ExceptionMiddleware> _logger;
//    private readonly IHostEnvironment _env;

//    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
//    {
//        _next = next;
//        _logger = logger;
//        _env = env;
//    }

//    public async Task Invoke(HttpContext context)
//    {
//        try
//        {
//            await _next(context); // Call next middleware
//        }
//        catch (Exception ex)
//        {
//            _logger.LogError(ex, "Unhandled exception occurred");

//            context.Response.StatusCode = 500;
//            context.Response.ContentType = "application/json";

//            var response = _env.IsDevelopment()
//                ? new { error = ex.Message, stackTrace = ex.StackTrace }
//                : new { error = "An unexpected error occurred" };

//            await context.Response.WriteAsJsonAsync(response);
//        }
//    }
//}

//}
