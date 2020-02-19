using RedisMongoApp.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisMongoApp.Interfaces
{
    public interface IRedisOperations
    {
        void add(Menu menu);
        Menu get(string id);
        void delete(string id);
        Menu updateVisitor(Menu menu, string id);
        IEnumerable<Menu> getAll();
    }
}
