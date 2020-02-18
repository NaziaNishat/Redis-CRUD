using Hangfire;
using Hangfire.MemoryStorage;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Hangfire.Logging;
using Hangfire.Logging.LogProviders;
using System.Threading.Tasks;
using System.Threading;
using RedisMongoApp.Models;

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

        static readonly int TRIES_TILL_SUCCESS = 3;
        static HttpClient client = new HttpClient();


        static void Main(string[] args)
        {
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

        public static void check() {
            List<Menu> menus = getMenu();
            for (int i = 0; i < menus.Count; i++)
            {
                var numMenu = menus[i];
                //Console.WriteLine(numMenu.numVisit);
                if (numMenu.numVisit > 1)
                {
                    Console.WriteLine("Save in Redis : "+numMenu.numVisit);
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