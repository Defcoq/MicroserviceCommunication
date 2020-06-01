using BookShop.Services.Requests.Genre;
using BookShop.Services.Responses.Book;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Services.Contracts
{
    public interface IGenreService
    {
        Task<IEnumerable<GenreResponse>> GetGenreAsync();
        Task<GenreResponse> GetGenreAsync(GetGenreRequest request);
        Task<IEnumerable<BookResponse>>
        GetBookByGenreIdAsync(GetGenreRequest request);
        Task<GenreResponse> AddGenreAsync(AddGenreRequest request);
    }
}
