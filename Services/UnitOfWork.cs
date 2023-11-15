// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com

using Accurri.Dal;

namespace Accurri.Services;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly ILogger<UnitOfWork> _logger;
    private readonly AccurriDbContext _dbContext;

    public UnitOfWork(ILogger<UnitOfWork> logger, AccurriDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }

    public IQueryable<TEntity> From<TEntity>() where TEntity : class
    {
        return _dbContext.Set<TEntity>();
    }

    public TEntity? Find<TKey, TEntity>(TKey id) where TEntity : class
    {
        return _dbContext.Find<TEntity>(id);
    }

    public async Task<TEntity?> FindAsync<TKey, TEntity>(TKey id, CancellationToken token = default)
        where TEntity : class
    {
        return await _dbContext.FindAsync<TEntity>(id, token);
    }

    public void Add<TEntity>(TEntity entity) where TEntity : class
    {
        _dbContext.Add(entity);
    }

    public void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
    {
        _dbContext.AddRange(entities);
    }

    public void Update<TEntity>(TEntity entity) where TEntity : class
    {
        _dbContext.Update(entity);
    }

    public void UpdateRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
    {
        _dbContext.UpdateRange(entities);
    }

    public void Remove<TEntity>(TEntity entity) where TEntity : class
    {
        _dbContext.Remove(entity);
    }

    public void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class
    {
        _dbContext.RemoveRange(entities);
    }

    public void Save()
    {
        var count = _dbContext.SaveChanges();
        _logger.LogInformation("Saved {count} changes in the database", count);
    }

    public async Task SaveAsync(CancellationToken token = default)
    {
        var count = await _dbContext.SaveChangesAsync(token);
        _logger.LogInformation("Saved {count} changes in the database (async)", count);
    }
}
