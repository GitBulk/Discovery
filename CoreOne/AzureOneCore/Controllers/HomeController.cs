using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;

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
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult WriteCookie(string key, string value, bool isPersistent = false)
        {
            if (isPersistent)
            {
                CookieOptions options = new CookieOptions();
                options.Expires = DateTime.Now.AddDays(1);
                Response.Cookies.Append(key, value, options);
            }
            else
            {
                Response.Cookies.Append(key, value);
            }
            ViewBag.Message = "Cookie written successfully!";
            return View("Index");
        }

        public IActionResult ReadCookies(string key)
        {
            ViewBag.CookieValue = Request.Cookies[key];
            return View();
        }
    }
}
