using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace AnagramSolver.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnagramApiController : ControllerBase
    {
        // GET: api/AnagramAPI
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/AnagramAPI/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/AnagramAPI
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/AnagramAPI/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
