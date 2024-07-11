using System.Linq.Expressions;

namespace Product.Domain.Repository;

public interface IRepository
{
    IQueryable<T> QueryAsNoTracking<T>(params Expression<Func<T, object>>[] includeProperties) where T : class;
    IQueryable<T> Query<T>(params Expression<Func<T, object>>[] includeProperties) where T : class;
    IQueryable<T> QueryIncludeStringProperties<T>(params string[] includeProperties) where T : class;
    Task AddAsync<T>(T entity, CancellationToken cancellationToken) where T : class;
    Task AddRange<T>(IEnumerable<T> items) where T : class;
    void Remove<T>(T item) where T : class;
    void RemoveRange<T>(IEnumerable<T> items) where T : class;
    Task BeginTransactionAsync();
    void RollbackTransaction();
    void CommitTransaction();
    Task<bool> CommitAsync(CancellationToken cancellationToken);
    void Update<T>(T entity) where T : class;
}