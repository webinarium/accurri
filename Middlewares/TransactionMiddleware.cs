using Accurri.Dal;

namespace Accurri.Middlewares;

internal sealed class TransactionMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, ILogger<TransactionMiddleware> logger, AccurriDbContext dbContext)
    {
        try
        {
            await dbContext.Database.BeginTransactionAsync();
            await next(context);
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
