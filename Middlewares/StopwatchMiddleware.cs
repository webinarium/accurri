using System.Diagnostics;

namespace Accurri.Middlewares;

internal sealed class StopwatchMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, ILogger<StopwatchMiddleware> logger)
    {
        var sw = new Stopwatch();

        sw.Start();
        await next(context);
        sw.Stop();

        logger.LogInformation("[{method} {path}] processing time {elapsed}",
            context.Request.Method,
            context.Request.Path,
            sw.Elapsed
        );
    }
}
