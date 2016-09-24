using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HowToDI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProduct product;

        public HomeController()
        {
            this.product = new VietnamProduct();
        }
        public HomeController(IProduct product)
        {
            this.product = product;
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }

        public string ViewDI()
        {
            return this.product.Build();
        }
    }
}
