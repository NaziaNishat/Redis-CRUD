using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RedisMongoApp.Interfaces;
using RedisMongoApp.Models;
using RedisMongoApp.Services;

namespace RedisMongoApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private IOperations operations;
        private IRedisOperations redisOperations;
        private Counter counter;


        public HomeController(IOperations iOperations,IRedisOperations iRedisOperations)
        {
            operations = iOperations;
            redisOperations = iRedisOperations;
            //counter = counter.getInstance();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Menu>> GetAll()
        {
            return Ok(operations.getAll());
        }

/*        [HttpGet("/Redis")]
        public ActionResult<IEnumerable<Menu>> GetAllRedis()
        {
            return Ok(redisOperations.getAll());
        }*/

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Menu> Get(string id)
        {

            Menu menu = redisOperations.get(id);

            if (menu == null)
            {
                menu = operations.get(id);
                if (menu != null)
                {
                    menu = operations.updateVisitor(menu, id);
                }
            }
            return Ok(menu);


        }

        // POST api/values
        [HttpPost]
        public object Post([FromBody] Menu menu)
        {
            
            //redisOperations.add(menu);
            operations.add(menu);
            return Ok(menu);
        }

        // DELETE api/redis/5
        [HttpDelete("{key}")]
        public ActionResult Delete(string key)
        {
            redisOperations.delete(key);
            operations.delete(key);
            return Ok("Menu Deleted");
        }
    }
}
