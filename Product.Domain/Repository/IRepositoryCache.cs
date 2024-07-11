using System.Linq.Expressions;

namespace Product.Domain.Repository;

public interface IRepositoryCache
{
    Task<string?> GetValueFromKey(string key, int database);
    Task SetValueKey(string key, byte[] value, int database);
}