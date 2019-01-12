namespace fx.Infra.Data.Redis
{
    using fx.Infra.MemoryCache;
    using System;
    using StackExchange.Redis;
    using System.Collections.Generic;

    public class RedisDatabase : IMemoryCache
    {
        private RedisStream _redis;
        private IDatabase database;
        public RedisDatabase(IDatabase redisDb)
        {
            database = redisDb;
        }

        public ICollection<T> GetAll<T>(string hashKey) where T : new()
        {
            throw new NotImplementedException();
        }

        public bool IsExistsKey(string key)
        {
            return database.KeyExists(key);
        }

        public string ReadFromCache(string key)
        {
            return "";
        }

        public void Remove(string key)
        {
            database.HashDelete(key,"");
        }

        public void RenameKey(string key, string newKey)
        {
            throw new NotImplementedException();
        }

        public IDictionary<string, T> Search<T>(IEnumerable<string> keys)
        {
            throw new NotImplementedException();
        }

        public void WriteInCache(string key, string content)
        {
            database.StringSet(key, content);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="content"></param>
        /// <param name="expiredTime"></param>
        public void WriteInCache(string key, string content, int expiredTime)
        {
            database.StringSet(key, content, new TimeSpan(1000 * expiredTime));
        }

        public void WriteInCache<T>(string key, T entiry, int expiredTime = 0) where T : new()
        {
            throw new NotImplementedException();
        }
    }
}
