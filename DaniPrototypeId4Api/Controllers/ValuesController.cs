using System;
using System.Collections.Generic;
using System.Linq;
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
        public IEnumerable<string> Get()
        {
            lock (ValuesCriticalZone)
            {
                return Values.Select(x => $@"{x.Key}, {x.Value}");
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            string[] value;

            lock (ValuesCriticalZone)
            {
                value = Values.Where(x => x.Key == id).Select(x => $@"{x.Key}, {x.Value}").ToArray();
            }
            return value.Length != 0 ? value.First() : null;
        }

        // POST api/values
        [HttpPost]
        public int? Post([FromBody] string value)
        {
            lock (ValuesCriticalZone)
            {
                if (Values.Count >= 100)
                {
                    return null;
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

                return randomId;
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
            lock (ValuesCriticalZone)
            {
                if (Values.ContainsKey(id))
                {
                    Values[id] = value;
                }
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            lock (ValuesCriticalZone)
            {
                if (Values.ContainsKey(id))
                {
                    Values.Remove(id);
                }
            }
        }
    }
}
