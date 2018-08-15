using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExampleApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ValuesController : ControllerBase
    {
        private static readonly IDictionary<int, string> Values = new Dictionary<int, string>
        {
            {1, @"value1"},
            {2, @"value2"}
        };

        // GET api/values
        [HttpGet]
        public ActionResult<IDictionary<int, string>> Get()
        {
            return Ok(Values);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            if (!Values.ContainsKey(id))
            {
                return NotFound();
            }

            return Ok(Values[id]);
        }

        // POST api/values
        [HttpPost]
        public ActionResult Post([FromBody] string value)
        {
            var counter = 0;

            while (!Values.TryAdd(GetHashCode(), value) || counter++ < 100)
            {
            }

            return counter < 100 ? Ok() : throw new TimeoutException();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] string value)
        {
            if (!Values.ContainsKey(id))
            {
                return NotFound();
            }

            Values[id] = value;

            return Ok();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (!Values.ContainsKey(id))
            {
                return NotFound();
            }

            Values.Remove(id);

            return Ok();
        }
    }
}
