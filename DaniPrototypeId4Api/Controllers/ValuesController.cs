using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace DaniPrototypeId4Api.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private static readonly IDictionary<int, string> Values = new Dictionary<int, string>();

        private static readonly object ValuesCriticalZone = new object();

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            lock (ValuesCriticalZone)
            {
                return Ok(Values);
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            lock (ValuesCriticalZone)
            {
                if (Values.ContainsKey(id))
                {
                    return Ok(Values[id]);
                }

                return NotFound();
            }
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] string value)
        {
            lock (ValuesCriticalZone)
            {
                if (Values.Count >= 100)
                {
                    return BadRequest(@"Too many string. :(");
                }
                var randomizer = new Random();
                var duplicatedId = true;
                var randomId = 0;

                while (duplicatedId)
                {
                    randomId = randomizer.Next();
                    duplicatedId = Values.ContainsKey(randomId);
                }
                Values.Add(randomId, value);

                return Ok(randomId);
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]string value)
        {
            lock (ValuesCriticalZone)
            {
                if (!Values.ContainsKey(id))
                {
                    return NotFound();
                }
                Values[id] = value;

                return Ok();
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            lock (ValuesCriticalZone)
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
}
