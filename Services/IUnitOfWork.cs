namespace Accurri.Services;

#pragma warning disable CS1591

public interface IUnitOfWork
{
    IQueryable<TEntity> From<TEntity>() where TEntity : class;

    TEntity? Find<TKey, TEntity>(TKey id) where TEntity : class;
    Task<TEntity?> FindAsync<TKey, TEntity>(TKey id, CancellationToken token = default) where TEntity : class;

    void Add<TEntity>(TEntity entity) where TEntity : class;
    void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

    void Update<TEntity>(TEntity entity) where TEntity : class;
    void UpdateRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

    void Remove<TEntity>(TEntity entity) where TEntity : class;
    void RemoveRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class;

    void Save();
    Task SaveAsync(CancellationToken token = default);
}
