using StackExchange.Redis;
using System;
using System.Collections.Generic;

namespace fx.Infra.MemoryCache.Redis
{

    public class MemoryCache : IMemoryCache
    {
        private static readonly IDatabase Cache = RedisWriteConn.GetFactionConn_Write.GetDatabase();

        private TimeSpan _defaultExpiredTime = new TimeSpan(0, 12, 0, 0);
        private string _prefix;

        public MemoryCache KeyPair(string prefixKey)
        {
            if (string.IsNullOrEmpty(_prefix))
            {
                _prefix = prefixKey;
                return this;
            }
            _prefix = ":" + prefixKey;
            return this;
        }



        public void Value<T>(T value) where T : new() 
        {
            if (string.IsNullOrEmpty(_prefix))
                throw new Exception("The Key is Empty");

            RedisWriteHelper.SetObject(_prefix, value);            
        }

        public void Value(string value, TimeSpan? expiredTime = null)
        {
            if (string.IsNullOrEmpty(_prefix))
                throw new Exception("The Key is Empty");

            if (expiredTime != null)
                Cache.StringSet(_prefix, value, (TimeSpan) expiredTime);

            Cache.StringSet(_prefix, value, _defaultExpiredTime);
        }

        public void WriteInCache(string key, string content, int expiredTime = 0)
        {
            throw new NotImplementedException();
        }

        public string ReadFromCache(string key)
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            RedisWriteHelper.KeyDelete(key);
        }

        public bool IsExistsKey(string key)
        {
            return Cache.KeyExists(key);
        }

        public void WriteInCache<T>(string key, T entiry, int expiredTime = 0) where T : new()
        {
            
        }

        public void RenameKey(string key, string newKey)
        {
            Cache.KeyRename(key, newKey);
        }

        public ICollection<T> GetAll<T>(string hashKey) where T : new()
        {
            throw new NotImplementedException();
        }

        public IDictionary<string, T> Search<T>(IEnumerable<string> keys)
        {
            throw new NotImplementedException();
        }
    }
}
