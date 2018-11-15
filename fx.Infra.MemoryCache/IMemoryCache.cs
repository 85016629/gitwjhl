using System;

namespace fx.Infra.MemoryCache
{
    public interface IMemoryCache
    {
        void WriteInCache(string key, string content);
        void WriteInCache(string key, string content, int expiredTime);
        string ReadFromCache(string key);
        bool IsExistsKey(string key);
        void Remove(string key);
    }
}
