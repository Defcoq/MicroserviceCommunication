using BookShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Domain.Repositories
{
    public interface IBookRepository  : IRepository
    {
        Task<IEnumerable<Book>> GetAsync();
        Task<Book> GetAsync(Guid id);
        Book Add(Book item);
        Book Update(Book item);
        Book Delete(Book item);

        Task<IEnumerable<Book>> GetBookByAuthorIdAsync(Guid id);
        Task<IEnumerable<Book>> GetBookByGenreIdAsync(Guid id);
    }
}
