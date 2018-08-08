using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DaniPrototypeId4Api.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private static readonly IDictionary<int, string> _values = new Dictionary<int, string>();

        private static object _valuesCriticalZone = new object();

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return _values.Select(x => $@"{x.Key}, {x.Value}");
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            var value = _values.Where(x => x.Key == id).Select(x => $@"{x.Key}, {x.Value}");

            return value.Count() != 0 ? value.First() : null;
        }

        // POST api/values
        [HttpPost]
        public int? Post([FromBody]string value)
        {
            if (_values.Count < 100)
            {
                var randomizer = new Random();
                var duplicatedId = true;
                var randomId = 0;

                lock (_valuesCriticalZone)
                {
                    while (!duplicatedId)
                    {
                        randomId = randomizer.Next();
                        duplicatedId = _values.ContainsKey(randomId);
                    }
                    _values.Add(randomId, value);
                }

                return randomId;
            }
            return null;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
            lock (_valuesCriticalZone)
            {
                if (_values.ContainsKey(id))
                {
                    _values[id] = value;
                }
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            lock (_valuesCriticalZone)
            {
                if (_values.ContainsKey(id))
                {
                    _values.Remove(id);
                }
            }
        }
    }
}
