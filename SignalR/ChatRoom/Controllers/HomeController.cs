using ChatRoom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChatRoom.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Login model)
        {
            if (ModelState.IsValid)
            {
                ViewBag.Title = "Home Page";
                ViewBag.Username = model.UserName;
                Session["username"] = model.UserName;
                return RedirectToAction("Chat", "Home");
            }
            return View("Index");
        }

        public ActionResult Chat()
        {
            string name = Session["username"] as string;
            if (string.IsNullOrEmpty(name))
            {
                return RedirectToAction("Index");
            }
            ViewBag.UserName = name;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}