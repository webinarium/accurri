using Accurri.Middlewares;

namespace Accurri.Extensions;

internal static class MiddlewareExtension
{
    public static IApplicationBuilder UseStopwatchMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<StopwatchMiddleware>();
    }

    public static IApplicationBuilder UseTransactionMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<TransactionMiddleware>();
    }
}
