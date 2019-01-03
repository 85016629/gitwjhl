using StackExchange.Redis;

namespace Common.Redis
{
    public class RedisReadConn
    {
        private static ConnectionMultiplexer _connection;
        private static readonly object SyncObject = new object();

        //private static string _redisIp = ConfigurationManager.AppSettings["readRedisstr"];
        private static string _redisIp = "";

        public static ConnectionMultiplexer GetFactionConn_Read
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
