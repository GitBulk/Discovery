using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AzureCoreOne.Services;
using AzureCoreOne.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace AzureCoreOne.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService bookService;

        public BookController(IBookService bookService)
        {
            this.bookService = bookService;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            var book = this.bookService.Find(Convert.ToString(id));
            if (book == null)
            {
                // should use throw new HttpStatusCodeException(System.Net.HttpStatusCode.NotFound) for web api
                //throw new HttpStatusCodeException(System.Net.HttpStatusCode.NotFound);
                return NotFound();
            }
            return View(book);
        }
    }
}
