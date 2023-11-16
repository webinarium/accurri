// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using Accurri.Middlewares;

namespace Accurri.Extensions;

internal static class MiddlewareExtension
{
    public static IApplicationBuilder UseStopwatchMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<StopwatchMiddleware>();
    }
}
