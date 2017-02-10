using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Tam.Core.Utilities;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Caching.Distributed;

namespace AzureCoreOne.Controllers
{
    public class HomeController : Controller
    {
        private readonly Tam.Core.Cache.MemoryCache cache;
        private readonly IDistributedCache distributedCache;
        private readonly SystemSettings systemSettings;
        public HomeController(SystemSettings settings, IMemoryCache cache, IDistributedCache distributedCache)
        {
            this.systemSettings = settings;
            this.cache = new Tam.Core.Cache.MemoryCache(cache);
            this.distributedCache = distributedCache;
        }

        public IActionResult Index()
        {
            var request = HttpContext.Request;
            bool isLocal = HttpContext.Request.IsLocal();

            //var cookie = request.Cookies["MyCookie"];
            //if (string.IsNullOrWhiteSpace(cookie))
            //{
            //    HttpContext.Response.Cookies.Append(key: "MyCookie", value: Uri.EscapeDataString("Hello world"),
            //        options: new CookieOptions
            //        {
            //            Path = "/",
            //            HttpOnly = false,
            //            Secure = false
            //        });
            //}
            var person = new Person
            {
                Age = 30,
                Name = "Toan"
            };
            cache.Set<Person>("toan_cache", person, null);
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            Person toanPerson = cache.Get<Person>("toan_cache");
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";
            string cacheKey = "myname";
            string myName = this.distributedCache.GetString(cacheKey);
            if (string.IsNullOrEmpty(myName))
            {
                myName = "lasthulk";
                distributedCache.SetString(cacheKey, myName);
            }
            ViewBag.MyName = myName;

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

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
