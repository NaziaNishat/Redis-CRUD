using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisMongoApp.Models
{
    public class Counter
    {
        private Counter counter;
        private int count = 0;

        private Counter() {}

        public Counter getInstance()
        {
            if (counter == null)
            {
                counter = new Counter();
            }

            return counter;
        }

        public int numVisitor   
        {
            get { return count; }   
            set { count = value; }  
        }
    }
}
