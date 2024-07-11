using System.Data.Common;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Product.Domain.Repository;
using StackExchange.Redis;

namespace Product.Data;

public class RedisContext(IConfiguration configuration) : IRepositoryCache
{
    private readonly ConnectionMultiplexer _connection =
        ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")!);

    public async Task<string?> GetValueFromKey(string key, int database)
    {
        var redis = _connection.GetDatabase(database);
        return await redis.StringGetAsync(key);
    }

    public async Task SetValueKey(string key, byte[] value, int database)
    {
        var redis = _connection.GetDatabase(database);
        await redis.StringSetAsync(key, value);
    }
}