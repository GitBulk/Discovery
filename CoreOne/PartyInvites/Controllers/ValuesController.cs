using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace PartyInvites.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // private static Logger Logger = LogManager.GetCurrentClassLogger();
        private ILogger<ValuesController> logger;

        public ValuesController(ILogger<ValuesController> logger)
        {
            this.logger = logger;
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            this.logger.LogCritical("nlog is working from a controller");
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
