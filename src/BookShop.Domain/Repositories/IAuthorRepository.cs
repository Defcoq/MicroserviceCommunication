using BookShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Domain.Repositories
{
    public interface IAuthorRepository : IRepository
    {
        Task<IEnumerable<Author>> GetAsync();
        Task<Author> GetAsync(Guid id);
        Author Add(Author item);
    }
}
