// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using System.Diagnostics;

namespace Accurri.Middlewares;

internal sealed class StopwatchMiddleware
{
    private readonly RequestDelegate _next;

    public StopwatchMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ILogger<StopwatchMiddleware> logger)
    {
        var sw = new Stopwatch();

        sw.Start();
        await _next(context);
        sw.Stop();

        logger.LogInformation("[{method} {path}] processing time {elapsed}",
            context.Request.Method,
            context.Request.Path,
            sw.Elapsed
        );
    }
}
