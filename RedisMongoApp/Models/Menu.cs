using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisMongoApp.Models
{
    public class Menu
    {
        public string id { get; set; }

        public string item { get; set; }
        public int numVisit { get; set; }
        public string dbName { get; set; }
    }
}
