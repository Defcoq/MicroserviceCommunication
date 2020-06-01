using Microsoft.EntityFrameworkCore;
using System;
using Xunit;
using System.Linq;
using Shouldly;
using BookShop.EFRepository.Repositories;
using System.Threading.Tasks;
using BookShop.Domain.Entities;
using BookShopFixtures;

namespace BookShop.EFRepository.Tests
{
    public class BookShopEFRepositoryTests : IClassFixture<BookShopContextFactory>
    {

        private readonly BookRepository _sut;
        private readonly TestBookShopContext _context;

        public BookShopEFRepositoryTests(BookShopContextFactory bookShopContextFactory)
        {
            _context = bookShopContextFactory.ContextInstance;
            _sut = new BookRepository(_context);
        }
        [Fact]
        public async void should_get_data()
        {
            var options = new DbContextOptionsBuilder<BookShopContext>().UseInMemoryDatabase(databaseName: "should_get_data").Options;
            await using var context = new TestBookShopContext(options);
            context.Database.EnsureCreated();
            var sut = new BookRepository(context);
            var result = await sut.GetAsync();
            result.ShouldNotBeNull();
        }

        [Fact]
        public async Task should_returns_null_with_id_not_present()
        {
            var options = new DbContextOptionsBuilder<BookShopContext>()
            .UseInMemoryDatabase(databaseName:
            "should_returns_null_with_id_not_present")
            .Options;
            await using var context = new TestBookShopContext(options);
            context.Database.EnsureCreated();
            var sut = new BookRepository(context);
            var result = await sut.GetAsync(Guid.NewGuid());
            result.ShouldBeNull();
        }

        [Theory]
        [InlineData("b5b05534-9263-448c-a69e-0bbd8b3eb90e")]
        public async Task should_return_record_by_id(string guid)
        {
            var options = new DbContextOptionsBuilder<BookShopContext>()
            .UseInMemoryDatabase(databaseName: "should_return_record_by_id").Options;
            await using var context = new TestBookShopContext(options);
            context.Database.EnsureCreated();
            var sut = new BookRepository(context);
            var result = await sut.GetAsync(new Guid(guid));
            result.Id.ShouldBe(new Guid(guid));
        }

        [Fact]
        public async Task should_add_new_item()
        {
            var testItem = new Book
            {
                Name = "Test album",
                Description = "Description",
                LabelName = "Label name",
                Price = new Price { Amount = 13, Currency = "EUR" },
                PictureUri = "https://mycdn.com/pictures/32423423",
                ReleaseDate = DateTimeOffset.Now,
                AvailableStock = 6,
                GenreId = new Guid("c04f05c0-f6ad-44d1-a400-3375bfb5dfd6"),
                AuthorId = new Guid("f08a333d-30db-4dd1-b8ba-3b0473c7cdab")
            };
            _sut.Add(testItem);
            await _sut.UnitOfWork.SaveEntitiesAsync();
            _context.Books
            .FirstOrDefault(_ => _.Id == testItem.Id).ShouldNotBeNull();
        }
        [Fact]
        public async Task should_update_item()
        {
            var testItem = new Book
            {
                Id = new Guid("b5b05534-9263-448c-a69e-0bbd8b3eb90e"),
                Name = "Test album",
                Description = "Description updated",
                LabelName = "Label name",
                Price = new Price { Amount = 50, Currency = "EUR" },
                PictureUri = "https://mycdn.com/pictures/32423423",
                ReleaseDate = DateTimeOffset.Now,
                AvailableStock = 6,
                GenreId = new Guid("c04f05c0-f6ad-44d1-a400-3375bfb5dfd6"),
                AuthorId = new Guid("f08a333d-30db-4dd1-b8ba-3b0473c7cdab")
            };
            var options = new DbContextOptionsBuilder<BookShopContext>()
            .UseInMemoryDatabase("should_update_item")
            .Options;
            await using var context = new TestBookShopContext(options);
            context.Database.EnsureCreated();
            var sut = new BookRepository(context);
            sut.Update(testItem); await sut.UnitOfWork.SaveEntitiesAsync();
            context.Books
            .FirstOrDefault(x => x.Id == testItem.Id)
            ?.Description.ShouldBe("Description updated");
        }

    }
}
