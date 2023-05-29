using System;

namespace QueryCEP.API.Services
{
    public interface ICacheService
    {
        T GetData<T>(string key);
        bool SetData<T>(string key, T value, DateTimeOffset expiration);
        object RemoveData(string key);
    }
}