using AzureCoreOne.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzureCoreOne.ViewComponents
{
    public class BookListViewComponent : ViewComponent
    {
        private readonly IBookService bookService;

        public BookListViewComponent(IBookService bookService)
        {
            this.bookService = bookService;
        }

        public IViewComponentResult Invoke(int numberOfItems)
        {
            var books = this.bookService.GetNewBooks(numberOfItems);
            return View(books);
        }
    }
}
