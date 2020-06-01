using BookShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Domain.Repositories
{
   public interface IGenreRepository : IRepository
    {
        Task<IEnumerable<Genre>> GetAsync();
        Task<Genre> GetAsync(Guid id);
        Genre Add(Genre item);
    }
}
