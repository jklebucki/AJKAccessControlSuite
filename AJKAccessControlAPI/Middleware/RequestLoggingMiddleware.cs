using System.Diagnostics;

namespace AJKAccessControlAPI.Middleware{
    public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public RequestLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Log request details
        Debug.WriteLine($"Request: {context.Request.Method} {context.Request.Path}");

        // Call the next middleware in the pipeline
        await _next(context);

        // Log response details
        Debug.WriteLine($"Response: {context.Response.StatusCode}");
    }
}
}