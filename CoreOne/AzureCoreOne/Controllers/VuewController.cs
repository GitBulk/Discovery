using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AzureCoreOne.Controllers
{
    public class VuewController : Controller
    {
        // GET: /<controller>/
        public IActionResult OrderForm()
        {
            return View();
        }

        public IActionResult NavigationMenu()
        {
            return View();
        }

        public IActionResult InlineEditor()
        {
            return View();
        }
    }
}
