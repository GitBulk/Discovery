using AzureCoreOne.Models;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace AzureCoreOne.Services
{
    public class BookService : IBookService
    {
        private readonly ConcurrentDictionary<string, Book> books;
        private int nextId = 0;

        public BookService()
        {
            this.books = new ConcurrentDictionary<string, Book>();
            this.Add(new Book { Title = "RESTful API with MVC6", Author = "Nick Soper" });
            this.Add(new Book { Title = "Java 8", Author = "Vo Danh" });
            this.Add(new Book { Title = "Angular 3", Author = "Googing" });
        }

        public void Add(Book book)
        {
            if (book == null)
            {
                return;
            }

            this.nextId++;
            book.Id = nextId.ToString();

            this.books.TryAdd(book.Id, book);
        }

        public Book Find(string id)
        {
            Book book;
            this.books.TryGetValue(id, out book);
            return book;
        }

        public IEnumerable<Book> GetAll()
        {
            return this.books.Values.OrderBy(b => b.Id);
        }

        public IEnumerable<Book> GetNewBooks(int numberOfItems)
        {
            if (numberOfItems < 1)
            {
                return Enumerable.Empty<Book>();
            }
            var books = this.books.Values.OrderByDescending(b => b.Id).Take(numberOfItems);
            return books;
        }

        public Book Remove(string id)
        {
            Book book;
            this.books.TryRemove(id, out book);
            return book;
        }

        public void Update(Book book)
        {
            this.books[book.Id] = book;
        }
    }
}
