
using System.Diagnostics;

namespace Revision_Part_1.Middlewares;

public class RequestTimeLogging(ILogger<RequestTimeLogging> _logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var stopWatch = Stopwatch.StartNew();
        await next.Invoke(context);
        stopWatch.Stop();

        if(stopWatch.ElapsedMilliseconds / 1000 > 4)
        {
            _logger.LogWarning("Request [{Verb}] at [{Path}] took {Time} ms", context.Request.Method, context.Request.Path, stopWatch.ElapsedMilliseconds);
        }
    }
}
