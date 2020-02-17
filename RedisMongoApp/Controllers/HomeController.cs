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

        public HomeController(IOperations ioperations)
        {
            operations = ioperations;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Menu>> GetAll()
        {
            return Ok(operations.getAll());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<Menu> Get(int id)
        {
            return Ok();

        }

        // POST api/values
        [HttpPost]
        public object Post([FromBody] Menu menu)
        {
            
            operations.add(menu);
            return Ok(menu);
        }
    }
}
