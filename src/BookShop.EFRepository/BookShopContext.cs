using BookShop.Domain.Entities;
using BookShop.Domain.Repositories;
using BookShop.EFRepository.SchemaDefinitions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BookShop.EFRepository
{
   public  class BookShopContext : DbContext, IUnitOfWork
    {
        public const string DEFAULT_SCHEMA = "bookshop";
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public BookShopContext(DbContextOptions<BookShopContext> options) :
        base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookEntitySchemaDefinition());
            modelBuilder.ApplyConfiguration(new GenreEntitySchemaConfiguration());
            modelBuilder.ApplyConfiguration(new AuthorEntitySchemaConfiguration());
            base.OnModelCreating(modelBuilder);
        }
        public async Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            await SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
