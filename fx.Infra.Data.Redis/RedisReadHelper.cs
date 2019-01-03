using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Redis
{
    public class RedisReadHelper
    {
        private static IDatabase cache = RedisReadConn.GetFactionConn_Read.GetDatabase();

        public static Dictionary<string, string> HashGetAll(string hash)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            try
            {
                HashEntry[] values = cache.HashGetAll(hash);
                foreach (HashEntry h in values)
                {
                    string key = h.Name.ToString();
                    string value = h.Value.ToString();
                    dic.Add(key, value);
                }
            }
            catch (Exception ex)
            {
                return null;
            }

            return dic;
        }


        /// <summary>
        /// 根据名称获取对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name"></param>
        /// <returns></returns>
        //public static T GetObject<T>(ref DtoResult result, string name, List<string> keyArry = null) where T : new()
        //{
        //    T reEnt = new T();
        //    var allProperties = reEnt.GetType().GetProperties();
        //    var allField = allProperties.Where(x => (keyArry == null || keyArry.Count() == 0 || keyArry.Contains(x.Name))).Select(x => (RedisValue)x.Name).ToArray();
        //    RedisValue[] values = cache.HashGet(name, allField);
        //    int errNum=values.Count(x => !x.HasValue);
        //    if (errNum > 0)
        //    {
        //        result.IsSucess = false;
        //        result.RetMsg = string.Format("取的数，有{0}个错误", errNum);
        //        return reEnt;
        //    }
        //    for (int i = 0; i < allField.Count(); i++)
        //    {
        //        var item = allProperties.SingleOrDefault(x => x.Name == allField[i]);
        //        if (item == null) continue;
        //        try
        //        {
        //            var piType = item.PropertyType;
        //            if (!piType.IsGenericType)
        //            {
        //                item.SetValue(reEnt, Convert.ChangeType(values[i], piType), null);
        //            }
        //            else
        //            {
        //                Type genericTypeDefinition = piType.GetGenericTypeDefinition();
        //                item.SetValue(reEnt, Convert.ChangeType(values[i], Nullable.GetUnderlyingType(piType)));
        //            }
        //        }
        //        catch (Exception e)
        //        {
        //            result.IsSucess = false;
        //            result.RetMsg = string.Format("对象[{0}]的性[{1}]值[{2}]转换无效:", reEnt.GetType().Name, item.Name, values[i], e.Message);
        //        }
        //    }
        //    result.IsSucess = true;
        //    return reEnt;
        //}



        /// <summary>
        /// 根据名称获取，对象列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="names"></param>
        /// <returns></returns>
        //public static List<T> GetObjects<T>(List<string> names, List<string> keyArry = null) where T : new()
        //{
        //    List<T> reEnts = new List<T>();
        //    foreach (var name in names)
        //    {
        //        DtoResult result = new DtoResult();
        //        reEnts.Add(GetObject<T>(ref result, name, keyArry));
        //    }
        //    return reEnts;
        //}



        /// <summary>
        /// 根据值数据获取字字典
        /// </summary>
        /// <param name="name"></param>
        /// <param name="allKey"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetDictionary(string name, List<string> allKey)
        {
            var allrv = allKey.Select(x => (RedisValue)x).ToArray();
            RedisValue[] values = cache.HashGet(name, allrv);  //可直接调用
            StringBuilder sb = new StringBuilder();
            Dictionary<string, string> allKv = new Dictionary<string, string>();
            for (int i = 0; i < allrv.Length; i++)
            {
                allKv.Add(allrv[i], values[i]);
            }

            return allKv;
        }


        /// <summary>
        /// 传入Key值 查询Redis表数据 没有返回NUll
        /// </summary>
        /// <param name="hash"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string StringHashGetKey(string hash, string key)
        {
            RedisValue values;
            try
            {
                values = cache.HashGet(hash, key);
            }
            catch
            {
                //LogHelper.WriteLog(typeof(RedisReadHelper), ex);
                return "";
            }
            return values.ToString();
        }

        //public static string[] StringHashGetKeyArry(ref DtoResult result, string hash, string[] hashFields)
        //{

        //    RedisValue[] rv = new RedisValue[hashFields.Length];
        //    for (int i = 0; i < hashFields.Length; i++)
        //    {
        //        rv[i] = hashFields[i];
        //    }
        //    RedisValue[] values = cache.HashGet(hash, rv);  //可直接调用
        //    string[] arry = new string[values.Length];
        //    for (int i = 0; i < values.Length; i++)
        //    {
        //        arry[i] = values[i];
        //    }

        //    int errNum = values.Count(x => !x.HasValue);
        //    if (errNum > 0)
        //    {
        //        result.IsSucess = false;
        //        result.RetMsg = string.Format("取的数，有{0}个错误", errNum);
        //    }
        //    else
        //    {
        //        result.IsSucess = true;
        //    }
        //    return arry;
        //}

        /// <summary>
        /// 执行lua脚本程序
        /// </summary>
        /// <param name="lua"></param>
        /// <param name="parms"></param>
        /// <returns></returns>
        public static string ScriptEvaluate(string lua, string[] parms)
        {
            RedisKey[] param = new RedisKey[parms.Length];
            for (int i = 0; i < parms.Length; i++)
            {
                param[i] = parms[i];
            }
            RedisResult obj = cache.ScriptEvaluate(lua, param);

            return obj == null ? "" : obj.ToString();
        }
        /// <summary>
        /// 查询Redis时间戳
        /// </summary>
        /// <returns></returns>
        public static string UpTime()
        {
            string la = "return redis.call('time')";
            string[] a = (string[])cache.ScriptEvaluate(la);
            string ss = a[0];

            return ss;
        }

        public static string StringGet(string key)
        {
            return cache.StringGet(key).ToString();
        }

        public static string[] StringGetArry(string[] strArry)
        {
            RedisKey[] keys = new RedisKey[strArry.Length];
            for (int i = 0; i < strArry.Length; i++)
            {
                keys[i] = strArry[i];
            }
            RedisValue[] values = cache.StringGet(keys);  //可直接调用
            string[] arry = new string[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                arry[i] = values[i];
            }
            return arry;
        }

        /// <summary>
        /// Read Zset 
        /// </summary>
        /// <param name="hash"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string StringZGetKey(string hash)
        {
            SortedSetEntry[] values = cache.SortedSetRangeByScoreWithScores(hash);
            int length = values.Length;
            string str = values[length - 1].ToString();
            return str;
        }

        public static string StringZGetKeyAll(string hash)
        {
            SortedSetEntry[] values = cache.SortedSetRangeByScoreWithScores(hash);
            string str = "";
            foreach (var a in values)
            {
                str += a + ",";
            }
            return str;
        }

        /// <summary>
        /// 根据hash，key读取lua再执行
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetHashLua(string hash, string key, string[] parms)
        {
            string lua = cache.HashGet(hash, key);//读取Lua脚本(转化为Hash)

            //RedisKey[] param = { "userlist", key, value };
            RedisKey[] param = new RedisKey[parms.Length];
            for (int i = 0; i < parms.Length; i++)
            {
                param[i] = parms[i];
            }
            RedisResult obj = cache.ScriptEvaluate(lua, param);
            string[] objArry = (string[])obj;
            string message = "[";
            foreach (string n in objArry)
            {
                message += n + ",";
            }
            message = message.Substring(0, message.Length - 1) + "]";
            return objArry.Length == 0 ? "" : message;
        }
        public static string GetHashLuaStr(string lua, string key, string[] parms)
        {
            //string lua = cache.HashGet(hash, key);//读取Lua脚本(转化为Hash)

            //RedisKey[] param = { "userlist", key, value };

            RedisKey[] param = new RedisKey[parms.Length];
            for (int i = 0; i < parms.Length; i++)
            {
                param[i] = parms[i];
            }
            RedisResult obj = cache.ScriptEvaluate(lua, param);
            //string[] objArry = (string[])obj;
            //string message = "[";
            //foreach (string n in objArry)
            //{
            //    message += n + ",";
            //}
            //message = message.Substring(0, message.Length - 1) + "]";
            //return objArry.Length == 0 ? "" : message;
            return obj == null ? "" : obj.ToString();
        }

        /// <summary>
        /// 批量读取Hash数据
        /// </summary>
        /// <param name="hashArry"></param>
        /// <param name="hashFields"></param>
        /// <returns></returns>
        public static string Batch(string[] hashArry, string[] hashFields)
        {
            string result = "";
            List<Task<StackExchange.Redis.HashEntry[]>> valueList = new List<Task<StackExchange.Redis.HashEntry[]>>();
            var batch = cache.CreateBatch();
            foreach (string hash in hashArry)
            {
                string key = hash;
                Task<StackExchange.Redis.HashEntry[]> tres = batch.HashGetAllAsync(key);
                valueList.Add(tres);
            }
            batch.Execute();

            if (valueList != null && valueList.Count > 0)
            {
                foreach (var hashEntry in valueList)
                {
                    var rtnValue = hashEntry.Result.ToDictionary(k => k.Name, v => v.Value);
                    if (rtnValue != null && rtnValue.Count > 0)
                    {
                        result += "{";
                        foreach (var field in hashFields)
                        {
                            if (rtnValue.Keys.Contains(field.ToString()))
                            {
                                result += "\"" + field + "\":\"" + rtnValue[field] + "\",";
                            }
                        }
                        result = result.Substring(0, result.Length - 1);
                        result += "},";
                    }
                }
                result = string.IsNullOrEmpty(result) ? "" : result.Substring(0, result.Length - 1);
                result = "[" + result + "]";
                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
