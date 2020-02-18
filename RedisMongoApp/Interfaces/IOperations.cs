using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RedisMongoApp.Models;

namespace RedisMongoApp.Interfaces
{
    public interface IOperations
    {
        void add(Menu menu);
        Menu get(string id);
        Menu updateVisitor(Menu menu,string id);
        IEnumerable getAll();

    }
}
