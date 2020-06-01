using BookShop.Domain.Entities;
using BookShop.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.EFRepository.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookShopContext _context;
        public IUnitOfWork UnitOfWork => _context;
        public BookRepository(BookShopContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<IEnumerable<Book>> GetAsync()
        {
            return await _context
            .Books
            .AsNoTracking()
            .ToListAsync();
        }

        public async Task<Book> GetAsync(Guid id)
        {
            var item = await _context.Books
            .AsNoTracking()
            .Where(x => x.Id == id)
            .Include(x => x.Genre).Include(x => x.Author).FirstOrDefaultAsync();
            return item;
        }
        public Book Add(Book order)
        {
            return _context.Books
            .Add(order).Entity;
        }
        public Book Update(Book item)
        {
            _context.Entry(item).State = EntityState.Modified;
            return item;
        }

        public Book Delete(Book item)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Book>> GetBookByAuthorIdAsync(Guid id)
        {
            var items = await _context
                                     .Books
                                     .Where(item => item.AuthorId == id)
                                     .Include(x => x.Genre)
                                     .Include(x => x.Author)
                                     .ToListAsync();
                                                return items;
        }

        public async Task<IEnumerable<Book>> GetBookByGenreIdAsync(Guid id)
        {
            var items = await _context.Books
                 .Where(item => item.GenreId == id)
                 .Include(x => x.Genre)
                 .Include(x => x.Author)
                 .ToListAsync();
            return items;
        }
    }
}
