using System;

namespace fx.Infra.MemoryCache
{
    public interface IMemoryCache
    {
        /// <summary>
        /// 将键键值对写入缓存中。
        /// </summary>
        /// <param name="key"></param>
        /// <param name="content"></param>
        /// <param name="expiredTime"></param>
        void WriteInCache(string key, string content, int expiredTime = 0);
        /// <summary>
        /// 根据Key值从Redis中读出数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string ReadFromCache(string key);
        /// <summary>
        /// 判断一个键是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool IsExistsKey(string key);
        /// <summary>
        /// 从缓存中删除数据。
        /// </summary>
        /// <param name="key"></param>
        void Remove(string key);
    }
}
