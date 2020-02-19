using Hangfire;
using Hangfire.MemoryStorage;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Hangfire.Logging;
using Hangfire.Logging.LogProviders;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RedisMongoApp.Interfaces;
using RedisMongoApp.Models;
using RedisMongoApp.Services;
using StackExchange.Redis.Extensions.Core.Abstractions;
using StackExchange.Redis.Extensions.Core.Configuration;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis.Extensions.Core.Implementations;


namespace RedisMongoConsole
{
    class Program
    {
        static Dictionary<int, int> _jobsToRetriesMap = new Dictionary<int, int>
        {
            {1,0 },
            {5,0 },
            {8,0 },
            {10,0 },
            {11,0 },
            {12,0 },
            {15,0 },
            {20,0 },
            {25,0 }
        };



        //public static IConfiguration Configuration { get; }

        static void Main(string[] args)
        {

            var services = ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();
            serviceProvider.GetService<ConsoleApplication>().Run();
        }

        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            IConfiguration Configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            var redisConfiguration = Configuration.GetSection("Redis").Get<RedisConfiguration>();
            services.AddSingleton(redisConfiguration);
            services.AddSingleton<IRedisCacheClient, RedisCacheClient>();
            services.AddSingleton<IRedisCacheConnectionPoolManager, RedisCacheConnectionPoolManager>();
            services.AddSingleton<IRedisDefaultCacheClient, RedisDefaultCacheClient>();
            services.AddSingleton<StackExchange.Redis.Extensions.Core.ISerializer, StackExchange.Redis.Extensions.MsgPack.MsgPackObjectSerializer>();

            services.AddTransient<IRedisOperations, RedisService>();

            services.AddTransient<ConsoleApplication>();
            return services;
        }



    }

}