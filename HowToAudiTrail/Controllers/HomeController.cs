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
            Person p1 = new Person();
            p1.Age = 42;
            p1.Name = "Sam";

            // Perform a shallow copy of p1 and assign it to p2.
            Person p2 = p1.ShallowCopy();
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

    public class Person
    {
        public int Age { get; set; }
        public string Name { get; set; }

        public Person ShallowCopy()
        {
            return (Person)this.MemberwiseClone();
        }
    }
}