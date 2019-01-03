using System;

namespace fx.Infra.MemoryCache.Redis
{

    public class MemoryCache : IMemoryCache
    {
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
                RedisWriteHelper.SetStringKey(_prefix, value, (TimeSpan) expiredTime);

            RedisWriteHelper.SetStringKey(_prefix, value, _defaultExpiredTime);
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
            throw new NotImplementedException();
        }

        public bool IsExistsKey(string key)
        {
            throw new NotImplementedException();
        }
    }
}
