using BookShop.Domain.Entities;
using BookShop.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.EFRepository.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly BookShopContext _catalogContext;
        public IUnitOfWork UnitOfWork => _catalogContext;
        public GenreRepository(BookShopContext catalogContext)
        {
            _catalogContext = catalogContext;
        }
        public async Task<IEnumerable<Genre>> GetAsync()
        {
            return await _catalogContext.Genres
            .AsNoTracking()
            .ToListAsync();
        }
        public async Task<Genre> GetAsync(Guid id)
        {
            var item = await _catalogContext.Genres
            .FindAsync(id);
            if (item == null) return null;
            _catalogContext.Entry(item).State = EntityState.Detached;
            return item;
        }
        public Genre Add(Genre item)
        {
            return _catalogContext.Genres.Add(item).Entity;
        }
    }
}
