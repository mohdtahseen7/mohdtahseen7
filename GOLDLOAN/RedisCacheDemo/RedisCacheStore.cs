using StackExchange.Redis;

namespace RedisCacheDemo
{
    public class RedisCacheStore
    {

        private string _Hostname = "localhost";
        private int _Port = 6379;
        public RedisCacheStore()
        {
            _Hostname = System.Configuration.ConfigurationManager.AppSettings["redis"];
            _Port = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["redisPort"]);
            Console.WriteLine(_Hostname);
        }
        public string Get(string KeyName, string value)
        {
            // string value;

            ConfigurationOptions options = new ConfigurationOptions()
            {
                EndPoints = { { _Hostname, _Port } },
                AllowAdmin = true,
                ConnectTimeout = 5 * 1000,
                //AbortOnConnectFail = false,
                //ResponseTimeout = 60000,

            };

            using (var redis = ConnectionMultiplexer.Connect(options))
            {
                IDatabase db = redis.GetDatabase();
                if (db.KeyExists(KeyName))
                {
                    value = db.StringGet(KeyName);
                }
                else
                {
                    value = null;
                }
            }
            return value;//JsonSerializer.Deserialize<CacheData>(value);
        }
        public bool DeleteKey(string key)
        {
            bool result;
            ConfigurationOptions options = new ConfigurationOptions()
            {
                EndPoints = { { _Hostname, _Port } },
                AllowAdmin = true,
                ConnectTimeout = 5 * 1000,
                //AbortOnConnectFail = false,
                //ResponseTimeout = 60000,

            };

            using (var redis = ConnectionMultiplexer.Connect(options))
            {
                IDatabase db = redis.GetDatabase();
                result = db.KeyDelete(key);
            }

            return result;
        }

        public bool Expire(string key)
        {
            bool result;
            ConfigurationOptions options = new ConfigurationOptions()
            {
                EndPoints = { { _Hostname, _Port } },
                AllowAdmin = true,
                ConnectTimeout = 5 * 1000,
                //AbortOnConnectFail = false,
                //ResponseTimeout = 60000,

            };

            using (var redis = ConnectionMultiplexer.Connect(options))
            {
                IDatabase db = redis.GetDatabase();
                if (db.KeyExists(key))
                {
                    result = db.KeyExpire(key, DateTime.UtcNow.AddSeconds(3));
                }
                else
                {
                    result = false;
                }

            }

            return result;
        }

        public bool Set(string KeyName, string Value)
        {

            bool result;

            RedisValue KeyValue = new RedisValue(Value);//JsonSerializer.Serialize<CacheData>(Value));

            ConfigurationOptions options = new ConfigurationOptions()
            {
                EndPoints = { { _Hostname, _Port } },
                AllowAdmin = true,
                ConnectTimeout = 5 * 1000,
                AbortOnConnectFail = false,
                //ResponseTimeout = 60000,

            };

            using (var redis = ConnectionMultiplexer.Connect(options))
            {
                IDatabase db = redis.GetDatabase();

                result = db.StringSet(KeyName, KeyValue);
            }

            return result;


        }
        public class RedisRun
        {
            //System.Configuration.ConfigurationManager.AppSettings["ut"];



            public static void Set(string key, string value)
            {

                RedisCacheStore session = new RedisCacheStore();
                //51.38.74.188
                session.Set(key, value);



            }
            public static string Get(String key, string value)
            {
                RedisCacheStore session = new RedisCacheStore();
                return session.Get(key, value);
            }
            public static void Delete(string key)
            {
                RedisCacheStore session = new RedisCacheStore();
                session.DeleteKey(key);
            }
            public static void KeyExpire(string key)
            {
                RedisCacheStore session = new RedisCacheStore();
                session.Expire(key);
            }
        }
    }

}

