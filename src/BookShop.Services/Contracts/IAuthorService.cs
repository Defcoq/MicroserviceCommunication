using BookShop.Services.Requests.Authors;
using BookShop.Services.Responses.Book;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Services.Contracts
{
    public interface IAuthorService
    {
        Task<IEnumerable<AuthorResponse>> GetAuthorsAsync();
        Task<AuthorResponse> GetAuthorAsync(GetAuthorRequest request);
        Task<IEnumerable<BookResponse>>
        GetBookByAuthorIdAsync(GetAuthorRequest request);
        Task<AuthorResponse> AddAuthorAsync(AddAuthorRequest request);
    }
}
