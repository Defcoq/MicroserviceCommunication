using BookShop.Domain.Entities;
using BookShop.Services.Requests.Book;
using BookShop.Services.Responses.Book;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.Services.Mappers
{
    public interface IBookMapper
    {
        Book Map(AddBookRequest request);
        Book Map(EditBookRequest request);
        BookResponse Map(Book Book);
    }
}
