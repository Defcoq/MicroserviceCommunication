using BookShop.Domain.Entities;
using BookShop.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.EFRepository.Repositories
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly BookShopContext _bookShopContext;
        public IUnitOfWork UnitOfWork => _bookShopContext;

        public AuthorRepository(BookShopContext bookShopContext)
        {
            _bookShopContext = bookShopContext;
        }
        public async Task<IEnumerable<Author>> GetAsync()
        {
            return await _bookShopContext.Authors
            .AsNoTracking()
            .ToListAsync();
        }
        public async Task<Author> GetAsync(Guid id)
        {
            var artist = await _bookShopContext.Authors
            .FindAsync(id);
            if (artist == null) return null;
            _bookShopContext.Entry(artist).State = EntityState.Detached;
            return artist;
        }
        public Author Add(Author artist)
        {
            return _bookShopContext.Authors.Add(artist).Entity;
        }
    }
}
