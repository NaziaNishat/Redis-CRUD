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


        public HomeController(IOperations iOperations,IRedisOperations iRedisOperations)
        {
            operations = iOperations;
            redisOperations = iRedisOperations;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Menu>> GetAll()
        {
            return Ok(operations.getAll());
        }


        // GET [controller]/5
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

        // POST [controller]
        [HttpPost]
        public object Post([FromBody] Menu menu)
        {
            
            //redisOperations.add(menu);
            operations.add(menu);
            return Ok(menu);
        }

        // DELETE [controller]/5
        [HttpDelete("{key}")]
        public ActionResult Delete(string key)
        {
            redisOperations.delete(key);
            operations.delete(key);
            return Ok("Menu Deleted");
        }
    }
}
