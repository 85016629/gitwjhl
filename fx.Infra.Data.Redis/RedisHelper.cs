namespace fx.Infra.Data.Redis
{
    using fx.Infra.MemoryCache;
    using System;
    using StackExchange.Redis;
    public class RedisDatabase : IMemoryCache
    {
        private RedisStream _redis;
        private StackExchange.Redis.IDatabase database;
        public RedisDatabase(IDatabase redisDb)
        {
            database = redisDb;
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
    }
}
