using System;
using System.Collections.Generic;

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
        void RenameKey(string key, string newKey);

        #region Hash存对象
        /// <summary>
        /// 将一个对象保存的Redis中。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="entiry"></param>
        /// <param name="expiredTime"></param>
        void WriteInCache<T>(string key, T entiry, int expiredTime = 0) where T : new();

        ICollection<T> GetAll<T>(string hashKey) where T : new();

        IDictionary<string, T> Search<T>(IEnumerable<string> keys);
        
        #endregion
    }
}
