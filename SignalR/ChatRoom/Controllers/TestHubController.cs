using ChatRoom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ChatRoom.Controllers
{
    public class TestHubController : Controller
    {
        // GET: TestHub
        public ActionResult Index()
        {
            var result = ProductManager.Products.Where(s => s.Active).ToList();
            return View(result);
        }

        // GET: TestHub/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TestHub/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TestHub/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TestHub/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TestHub/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TestHub/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TestHub/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
