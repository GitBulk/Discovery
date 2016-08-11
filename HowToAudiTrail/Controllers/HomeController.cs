using HowToAudiTrail.Filters;
using HowToAudiTrail.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HowToAudiTrail.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [Audit]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            var context = new SampleEntities();
            var audits = context.Audits.ToList();
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}