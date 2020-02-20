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
            menu.dbName = "Redis";
            _redis.Db0.Add(menu.id, menu, DateTimeOffset.Now.AddMinutes(1000));
        }

        public Menu get(string id)
        { 
            var menu = _redis.Db0.Get<Menu>(id);
            if (menu != null)
            {
                updateVisitor(menu, id);

            }
            return menu;
        }

        public void delete(string key)
        {
            try
            {
                var result = _redis.Db0.Remove(key);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public IEnumerable<Menu> getAll()
        {
            var menus = _redis.Db0.GetAll<Menu>(new string[] { "*" });
            return menus.Values.ToList();
        }

        public Menu updateVisitor(Menu menu, string key)
        {
            var result = _redis.Db0.Get<Menu>(key);

            result.numVisit += 1;
            _redis.Db0.Add(key, result);

            return result;

        }
    }
}
