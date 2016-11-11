﻿using System.Collections.Generic;

namespace FirstWebApiCore.Models
{
    public interface IBookRepository
    {
        void Add(Book book);
        IEnumerable<Book> GetAll();
        Book Find(string id);
        Book Remove(string id);
        void Update(Book book);
    }
}