using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HowToDI.Controllers
{
    //[Authorize]
    public class ValuesController : ApiController
    {

        private readonly IProduct product;

        public ValuesController()
        {
            this.product = new VietnamProduct();
        }
        public ValuesController(IProduct product)
        {
            this.product = product;
        }

        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            //return "value";
            return this.product.Build();
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
