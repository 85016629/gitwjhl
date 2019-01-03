using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace fx.Infra.MemoryCache.Redis
{
    public class RedisWriteHelper
    {
        private static IDatabase cache = RedisWriteConn.GetFactionConn_Write.GetDatabase();

        public void TestBatchSent(Dictionary<string,string> hash)
        {
            var conn = cache;
            conn.KeyDeleteAsync("batch");
            conn.StringSetAsync("batch", "batch-sent");
            var tasks = new List<Task>();
            var batch = conn.CreateBatch();
            
            batch.Execute();
        }

        #region 操作hash数据类型
        /// <summary>
        /// 添加或更新hash数据的多个字段值(HashSet)
        /// </summary>
        /// <param name="hash">key</param>
        /// <param name="hashFields">需更新的字段和值</param>
        public static void HashSetEntry(string hash, Dictionary<string,object> hashFields)
        {
            var usKv = hashFields.Where(x => x.Value != null).Select(x => new HashEntry(x.Key, x.Value.ToString())).ToArray();
            cache.HashSet(hash, usKv);
        }

        /// <summary>
        /// 实体类，保存到redis
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <param name="inObj"></param>
        /// <param name="saveItems"></param>
        /// <returns></returns>
        public static bool SetObject<T>(string name, T inObj, List<string> saveItems = null) where T : new()
        {
            var allProperties = inObj.GetType().GetProperties();
            var allField = allProperties.Select(x => (RedisValue)x.Name).ToArray();
            RedisValue[] values = cache.HashGet(name, allField);
            List<HashEntry> allKv = new List<HashEntry>();
            for (int i = 0; i < allProperties.Count(); i++)
            {
                var item = allProperties[i];
                object value = item.GetValue(inObj);
                if (value != null && value != DBNull.Value)
                {
                    allKv.Add(new HashEntry(item.Name, value.ToString()));
                }
            }
            cache.HashSet(name, allKv.ToArray());
            return true;
        }

        /// <summary>
        /// 添加或更新hash数据的单个字段值
        /// </summary>
        /// <param name="hash"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void HashSetKey(string hash, string key, string value)
        {
            cache.HashSet(hash, key, value);
        }
        /// <summary>
        /// 设置Hash过期时间
        /// </summary>
        /// <param name="hash"></param>
        /// <param name="span"></param>
        /// <returns></returns>
        public static bool HashExpire(string hash,TimeSpan span)
        {
           return cache.KeyExpire(hash, span);
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除根据Key删除
        /// </summary>
        /// <param name="hash">keyName</param>
        public static void KeyDelete(string keyName)
        {
            cache.KeyDelete(keyName);
        }
        #endregion

        #region 操作List数据类型
        /// <summary>
        /// 从列表的头部插入顺序排序
        /// </summary>
        /// <param name="listName">列表名称</param>
        /// <param name="values">值</param>
        public static void ListLeftPush(string listName, string[] values)
        {
            if (values.Length > 0)
            {
                var rv = new RedisValue[values.Length];
                for (int i = 0; i < values.Length; i++)
                {
                    rv[i] = values[i];
                }
                cache.ListLeftPush(listName, rv);
            }
        }
        /// <summary>
        /// 从列表的头部插入顺序排序
        /// </summary>
        /// <param name="listName">列表名称</param>
        /// <param name="values">值</param>
        public static void ListLeftPush(string listName, string value)
        {
                cache.ListLeftPush(listName, value);
        }
        /// <summary>
        /// 从列表中移除与值相等的元素。
        /// </summary>
        /// <param name="keyName">list的Key名称</param>
        /// <param name="value">删除list中的值</param>
        /// <param name="count">大于0删除与从头部到尾部移动的值相等的元素; 小于0删除与从尾部到头部的值相等的元素;等于0删除所有与值相等的元素</param>
        public static void ListRemove(string keyName, string value, int count)
        {
            cache.ListRemove(keyName, value, count);
        }
#endregion

        #region 操作Sorted set
        /// <summary>
        /// 添加SortedSet数据类型
        /// </summary>
        /// <param name="sortedKey">SortedSet数据类型key</param>
        /// <param name="sortedKeyFields">SortedSet数据类型 value和score</param>
        public static void SortedSetAdd(string sortedKey, Dictionary<string, int> sortedKeyFields)
        {
            int count = sortedKeyFields.Count;
            SortedSetEntry[] hashEntry = new SortedSetEntry[count];
            int i = 0;
            foreach (var u in sortedKeyFields)
            {
                hashEntry[i] = new SortedSetEntry(u.Key, u.Value);
                i++;
            }
            cache.SortedSetAdd(sortedKey, hashEntry);
        }
        public static void SortedSetDel(string sortedKey, string value)
        {
            cache.SortedSetRemove(sortedKey, value);
        }
        #endregion

        #region 更新
        /// <summary>
        /// 更新zSet
        /// </summary>
        /// <param name="hash"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static void StringZSetKey(string hash, string key, int value)
        {
            var values = cache.SortedSetAdd(hash, key, value);
        }
        
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="hash"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static void StringHashSetKey(string hash, string key, string value)
        {
            var values = cache.HashSet(hash, key, value);
        }

        public static void DelZSetKey(string hash, string key)
        {
            var values = cache.SortedSetRemove(hash, key);
        }


        #endregion

        #region 读取数据
        public static string[] StringHashGetKeyArry(string hash, string[] hashFields)
        {
            RedisValue[] rv = new RedisValue[hashFields.Length];
            for (int i = 0; i < hashFields.Length; i++)
            {
                rv[i] = hashFields[i];
            }
            RedisValue[] values = cache.HashGet(hash, rv);  //可直接调用
            string[] arry = new string[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                arry[i] = values[i];
            }
            return arry;
        }
        #endregion

        /// <summary>
        /// 删除Key
        /// </summary>
        /// <param name="hash"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string StringHashDelKey(string hash, string key)
        {
            var values = cache.HashDelete(hash, key);
            return values.ToString();
        }

        #region redis锁
        public static string LockRedis(string key, TimeSpan timeSpan, string interfaceType)
        {
            RedisValue token = "name-" + Environment.MachineName;

            if (cache.LockTake("lock_key", token, TimeSpan.FromSeconds(3)))   //key表示的是redis数据库中该锁的名称，不可重复。 Token用来标识谁拥有该锁并用来释放锁。TimeSpan表示该锁的有效时间。
            {
                try
                {
                    string value = cache.StringGet(key);
                    if (!string.IsNullOrEmpty(value) && value != "0")//已经使用过
                    {
                        return "-1";//该key的红包已被领取
                    }
                    else
                    {
                        cache.StringSet(key, 1, timeSpan);
                        return "1";
                    }
                }
                catch (Exception e)
                {
                    return "-2";//该key异常
                }
                finally
                {
                    cache.LockRelease("lock_key", token);
                }
            }
            else
            {
                return "0";//该Key锁失败
            }
        }
        #endregion

        #region 操作String数据类型
        /// <summary>
        /// set String类型
        /// </summary>
        /// <param name="key">string类型key值</param>
        /// <param name="value">值</param>
        /// <param name="timeSpan">当前key过期时间</param>
        public static void SetStringKey(string key, string value, TimeSpan timeSpan)
        {
            cache.StringSet(key, value, timeSpan);//新建字段返回值1;修改字段返回值0
        }
        #endregion
    }
}
