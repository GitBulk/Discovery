using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AzureOneCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly SystemSettings settings;
        public HomeController(IOptions<SystemSettings> settings)
        {
            this.settings = settings.Value;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {

            return View();
        }
    }
}
