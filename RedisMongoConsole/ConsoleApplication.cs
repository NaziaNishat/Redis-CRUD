using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Hangfire;
using Hangfire.Logging;
using Hangfire.Logging.LogProviders;
using Hangfire.MemoryStorage;
using RedisMongoApp.Interfaces;
using RedisMongoApp.Models;
using RedisMongoApp.Services;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis.Extensions.Core.Abstractions;


namespace RedisMongoConsole
{
    class ConsoleApplication
    {
        //private static IRedisOperations redisOperations;
        private readonly IRedisCacheClient redisCacheClient;

        private static IRedisOperations redisOperations;

        static HttpClient client = new HttpClient();

        private static RedisService redisService;

/*        private static IRedisOperations service;
*/
        public ConsoleApplication(IRedisOperations iRedisOperations)
        {
            redisOperations = iRedisOperations;
            //redisService = new RedisService(redisCacheClient);
        }

        public void Run()
        {
/*            var collection = new ServiceCollection();
            collection.AddScoped<IRedisOperations, RedisService>();
            var serviceProvider = collection.BuildServiceProvider();
            service = serviceProvider.GetService<IRedisOperations>();*/


            client.BaseAddress =
                new Uri("http://localhost:57276");

            // Specify headers
            var val = "application/json";
            var media =
                new MediaTypeWithQualityHeaderValue(val);

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(media);

            Console.WriteLine("List :");


            JobStorage storage = new MemoryStorage(new MemoryStorageOptions());
            LogProvider.SetCurrentLogProvider(new ColouredConsoleLogProvider());
            var serverOptions = new BackgroundJobServerOptions() { ShutdownTimeout = TimeSpan.FromSeconds(5) };

            using (var server = new BackgroundJobServer(serverOptions, storage))
            {
                //Console.WriteLine("Hangfire Server started. Press any key to exit...", LogLevel.Fatal);

                JobStorage.Current = storage;
                //BackgroundJob.Enqueue(() => Console.WriteLine("Hello Hangfire!", LogLevel.Error));

                //foreach (var job in _jobsToRetriesMap){

                /*                    BackgroundJob.Schedule(
                                        () => check(),
                                        TimeSpan.FromSeconds(1));*/

                RecurringJob.AddOrUpdate(() => check(), Cron.Minutely);


                //}
                System.Console.ReadKey();
                Console.WriteLine("Stopping server...", LogLevel.Fatal);
            }
        }

        public static void check()
        {
            List<Menu> menus = getMenu();
            for (int i = 0; i < menus.Count; i++)
            {
                var numMenu = menus[i];
                //Console.WriteLine(numMenu.numVisit);
                if (numMenu.numVisit > 3)
                {
                    Console.WriteLine("Hello!");
                    redisOperations.add(numMenu);

                }
            }
        }

        public static List<Menu> getMenu()
        {
            var action = "http://localhost:65200/home";
            var request =
                client.GetAsync(action);

            var response =
                request.Result.Content.
                    ReadAsAsync<List<Menu>>();

            return response.Result;
        }
    }
}
