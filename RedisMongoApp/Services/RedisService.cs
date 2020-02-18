using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RedisMongoApp.Interfaces;
using RedisMongoApp.Models;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace RedisMongoApp.Services
{

    public class RedisService : IRedisOperations
    {
        private readonly IRedisCacheClient _redis;

        public RedisService(IRedisCacheClient redis)
        {
            _redis = redis;
        }

        public void add(Menu menu)
        {
            _redis.Db0.Add(menu.id, menu, DateTimeOffset.Now.AddMinutes(1000));
        }

        public Menu get(string id)
        { 
            var menu = _redis.Db0.Get<Menu>(id);
            Console.WriteLine("Redissssssssss");
            return menu;
        }

        public IEnumerable getAll()
        {
            throw new NotImplementedException();
        }

        public Menu updateVisitor(Menu menu, string id)
        {
            throw new NotImplementedException();
        }
    }
}
