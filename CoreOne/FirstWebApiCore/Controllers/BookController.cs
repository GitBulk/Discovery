using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FirstWebApiCore.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace FirstWebApiCore.Controllers
{
    [Route("api/[controller]")]
    public class BookController : Controller
    {
        private readonly IBookRepository bookRepo;
        public BookController(IBookRepository bookRepo)
        {
            this.bookRepo = bookRepo;
        }

        [HttpGet]
        public IEnumerable<Book> GetAll()
        {
            return this.bookRepo.GetAll();
        }

        [HttpGet("{id}", Name = "GetBook")]
        public IActionResult GetById(string id)
        {
            var item = this.bookRepo.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item); // return 200;
            //return new ObjectResult(item);
        }

        public IActionResult Create([FromBody]Book book)
        {
            if (book == null)
            {
                return BadRequest();
            }
            this.bookRepo.Add(book);
            // https://github.com/niksoper/aspnet5-books/blob/blog-dotnet-rc1/src/MvcLibrary/Controllers/BooksController.cs
            return CreatedAtRoute("GetBook", new { id = book.Id }, book);
        }
    }
}
