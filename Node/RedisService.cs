using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
 public class RedisService
  {
    // redis config
    private static ConfigurationOptions configurationOptions = null;
    //the lock for singleton
    private static readonly object Locker = new object();

    //singleton
    private static ConnectionMultiplexer redisConn;
    public RedisService(string connectString){
      configurationOptions = ConfigurationOptions.Parse(connectString);
      configurationOptions = ConfigurationOptions.Parse("127.0.0.1:6379,password=xxx,connectTimeout=2000");
    }
  

    //singleton
    public static ConnectionMultiplexer GetRedisConn()
    {

      if (redisConn == null)
      {
        lock (Locker)
        {
          if (redisConn == null || !redisConn.IsConnected)
          {
            redisConn = ConnectionMultiplexer.Connect(configurationOptions);
          }
        }
      }
      return redisConn;
    }

    public static void CloseRedisConn()
    {
      redisConn.Close();
    }

  }
}

