using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AzureCoreOne.AppContexts;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AzureCoreOne.Controllers
{
    [Route("api/[controller]")]
    public class RootController : Controller
    {
        AzureCoreOneDbContext context;
        public RootController(AzureCoreOneDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        [Route("Import")]
        public string Import(string key)
        {
            if (key == "1qaZ2wsX")
            {
                context.Database.Migrate();
                return "Migrated";
            }
            return "NO";
        }

        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

    }
}
