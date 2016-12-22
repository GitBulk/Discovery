using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace AzureCoreOne.Controllers
{
    public class HomeController : Controller
    {
        private readonly SystemSettings systemSettings;
        public HomeController(SystemSettings settings)
        {
            this.systemSettings = settings;
        }

        public IActionResult Index()
        {
            var request = HttpContext.Request;
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        [Route("/Error/{errorCode}")]
        public ActionResult Error(int errorCode)
        {
            if (errorCode == 500 || errorCode == 404)
            {
                return View($"~/Views/Home/Error/{errorCode}.cshtml");
            }
            return View(errorCode);
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

        public ActionResult InjectToView()
        {
            return View();
        }

        public ViewResult Books()
        {
            return View();
        }
    }
}
