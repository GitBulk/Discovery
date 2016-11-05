using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BuildLinks.Controllers
{
    public class LandingController : Controller
    {
        // GET: Landing
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult Details(string id)
        {
            ViewBag.DistrictId = id;
            return View();
        }
    }
}