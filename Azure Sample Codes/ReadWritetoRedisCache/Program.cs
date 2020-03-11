using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace ReadWritetoRedisCache
{
    class Program
    {
        private static string CacheConnection = "azmmwnewredis.redis.cache.windows.net:6380,password=2FwUpwDdiaEvPc82kDeQRQLnAKtkDgFjcvROX7J1ccU=,ssl=True,abortConnect=False";
        static void Main(string[] args)
        {
            IDatabase cache = lazyConnection.Value.GetDatabase();
            Console.WriteLine("Reading Cache :" + cache.StringGet("Session333").ToString());
            Console.WriteLine("Writing Cache :" + cache.StringSet("Session333", "Writing something to redis "+ DateTime.Now.ToShortTimeString()).ToString());
            Console.WriteLine("Reading Cache :" + cache.StringGet("Session333").ToString());

            lazyConnection.Value.Dispose();

            Console.WriteLine("Press any key...");
            Console.ReadLine();
        }

        private static Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            return ConnectionMultiplexer.Connect(CacheConnection);
        });
        public static ConnectionMultiplexer Connection
        {
            get
            {
                return lazyConnection.Value;
            }
        }
    }
}
