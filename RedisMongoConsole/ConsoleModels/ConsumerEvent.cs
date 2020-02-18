using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace RedisMongoConsole.ConsoleModels
{
    class ConsumerEvent
    {
        public void GetData()   
        {
            using (var client = new WebClient())  
            {
                client.Headers.Add("Content-Type:application/json"); 
                client.Headers.Add("Accept:application/json");
                var result = client.DownloadString("http://localhost:5000/home"); 
                Console.WriteLine(Environment.NewLine + result);
            }
        }
    }
}
