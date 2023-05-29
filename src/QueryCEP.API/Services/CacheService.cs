using System;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace QueryCEP.API.Services
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase _cacheDb;
        private readonly IConfiguration _config;

        public CacheService(IConfiguration config)
        {
            _config = config;
            var redis = ConnectionMultiplexer.Connect(_config["RedisSettings:ConnectionString"]);
            _cacheDb = redis.GetDatabase();
        }

        public T GetData<T>(string key)
        {
            var value = _cacheDb.StringGet(key);
            if (!string.IsNullOrEmpty(value)) return JsonSerializer.Deserialize<T>(value);

            return default;
        }

        public object RemoveData(string key)
        {
            var exist = _cacheDb.KeyExists(key);
            if (exist) return _cacheDb.KeyDelete(key);

            return false;
        }

        public bool SetData<T>(string key, T value, DateTimeOffset expiration)
        {
            var expiryTime = expiration.DateTime.Subtract(DateTime.Now);
            return _cacheDb.StringSet(key, JsonSerializer.Serialize(value), expiryTime);
        }
    }
}