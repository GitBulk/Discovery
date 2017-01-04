using AzureCoreOne.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AzureCoreOne.Services
{
    public interface IBookService
    {
        void Add(Book book);
        IEnumerable<Book> GetAll();
        IEnumerable<Book> GetNewBooks(int numberOfItems);
        Book Find(string id);
        Book Remove(string id);
        void Update(Book book);
    }
}
