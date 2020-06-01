using BookShop.EFRepository;
using BookShop.Services.Mappers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookShopFixtures
{
    public class BookShopContextFactory
    {
        public readonly TestBookShopContext ContextInstance;
        public readonly IGenreMapper GenreMapper;
        public readonly IAuthorMapper ArtistMapper;
        public readonly IBookMapper BookMapper;

        public BookShopContextFactory()
        {
            var contextOptions = new
            DbContextOptionsBuilder<BookShopContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .EnableSensitiveDataLogging()
            .Options;
            EnsureCreation(contextOptions);
            ContextInstance = new TestBookShopContext(contextOptions);
            GenreMapper = new GenreMapper(); ArtistMapper = new AuthorMapper();
            BookMapper = new BookMapper(ArtistMapper, GenreMapper);
        }
        private void EnsureCreation(DbContextOptions<BookShopContext>
        contextOptions)
        {
            using var context = new TestBookShopContext(contextOptions);
            context.Database.EnsureCreated();
        }
    }
}
