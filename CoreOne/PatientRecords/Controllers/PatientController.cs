using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace PatientRecords.Controllers
{
    public class PatientController : Controller
    {
        public IActionResult Index(int id, string name = "unknown")
        {
            string output = "Patient info: " + HtmlEncoder.Default.Encode("Id: " + id + ", Name: " + name);
            ViewBag.Output = output;
            return View();
        }

        public IActionResult Details(int id, string name = "unknown")
        {
            return View();
        }
    }
}