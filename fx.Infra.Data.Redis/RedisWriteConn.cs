using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace fx.Infra.MemoryCache.Redis
{
    public class RedisWriteConn
    {
        private static ConnectionMultiplexer _connection;
        private static readonly object SyncObject = new object();
        
        private static string _redisIp = ""; //ConfigurationManager.ConnectionStrings["writeRedisstr"].ConnectionString;
        public static ConnectionMultiplexer GetFactionConn_Write
        {
            get
            {
                if (_connection == null)
                {
                    lock (SyncObject)
                    {
                        _connection = GetManager();
                    }
                }
                return _connection;
            }
        }

        private static ConnectionMultiplexer GetManager(string connectionString = null)
        {
            connectionString = connectionString ?? _redisIp;
            var connect = ConnectionMultiplexer.Connect(connectionString);

            return connect;
        }
    }
}
