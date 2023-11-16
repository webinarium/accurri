// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using Accurri.Dal;

namespace Accurri.Middlewares;

internal sealed class TransactionMiddleware
{
    private readonly RequestDelegate _next;

    public TransactionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(
        HttpContext context,
        ILogger<TransactionMiddleware> logger,
        AccurriDbContext dbContext
    )
    {
        try
        {
            await dbContext.Database.BeginTransactionAsync();
            await _next(context);
            logger.LogInformation("Commit transaction");
            await dbContext.Database.CommitTransactionAsync();
        }
        catch (Exception)
        {
            logger.LogError("Rollback transaction");
            await dbContext.Database.RollbackTransactionAsync();
            throw;
        }
    }
}
