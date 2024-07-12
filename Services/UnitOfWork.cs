using Accurri.Dal;

namespace Accurri.Services;

internal sealed class UnitOfWork(ILogger<UnitOfWork> logger, AccurriDbContext dbContext) : IUnitOfWork
{
    public IQueryable<TEntity> From<TEntity>() where TEntity : class
    {
        return dbContext.Set<TEntity>();
    }

    public TEntity? Find<TKey, TEntity>(TKey id) where TEntity : class
    {
        return dbContext.Find<TEntity>(id);
    }

    public async Task<TEntity?> FindAsync<TKey, TEntity>(TKey id, CancellationToken token = default)
        where TEntity : class
    {
        return await dbContext.FindAsync<TEntity>(id, token);
    }

    public void Add<TEntity>(TEntity entity) where TEntity : class
    {
        dbContext.Add(entity);
    }

    public void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
    {
        dbContext.AddRange(entities);
    }

    public void Update<TEntity>(TEntity entity) where TEntity : class
    {
        dbContext.Update(entity);
    }

    public void UpdateRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
    {
        dbContext.UpdateRange(entities);
    }

    public void Remove<TEntity>(TEntity entity) where TEntity : class
    {
        dbContext.Remove(entity);
    }

    public void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
    {
        dbContext.RemoveRange(entities);
    }

    public void Save()
    {
        var count = dbContext.SaveChanges();
        logger.LogInformation("Saved {count} changes in the database", count);
    }

    public async Task SaveAsync(CancellationToken token = default)
    {
        var count = await dbContext.SaveChangesAsync(token);
        logger.LogInformation("Saved {count} changes in the database (async)", count);
    }
}
