using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AzureOneCore.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AzureOneCore.Controllers
{
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Product Get(string id)
        {
            int resultId = 0;
            if (!int.TryParse(id, out resultId))
            {
                throw new HttpStatusCodeException(System.Net.HttpStatusCode.BadRequest);
            }
            return new Product()
            {
                Category = "Smartphone",
                Description = "Description",
                Name = "iPhone 8",
                Price = 1000,
                ProductId = resultId
            };
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
