using BookShop.Services.Requests.Book;
using BookShop.Services.Responses.Book;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Services.Contracts
{
    public interface IBookService
    {
        Task<IEnumerable<BookResponse>> GetBooksAsync();
        Task<BookResponse> GetBookAsync(GetBookRequest request);
        Task<BookResponse> AddBookAsync(AddBookRequest request);
        Task<BookResponse> EditBookAsync(EditBookRequest request);
        Task<BookResponse> DeleteBookAsync(DeleteBookRequest request);
    }
}
