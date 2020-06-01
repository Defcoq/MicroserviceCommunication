using BookShop.Domain.Entities;
using BookShop.EFRepository.Repositories;
using BookShop.Services.Contracts;
using BookShop.Services.Mappers;
using BookShop.Services.Requests.Book;
using BookShopFixtures;
using Shouldly;
using System;
using System.Threading.Tasks;
using Xunit;

namespace BookShop.Services.Tests
{
    public class BookServiceTests : IClassFixture<BookShopContextFactory>
    {
        private readonly BookRepository _bookRepository;
        private readonly IBookMapper _mapper;
        public BookServiceTests(BookShopContextFactory BookShopContextFactory)
        {
            _bookRepository = new
            BookRepository(BookShopContextFactory.ContextInstance);
            _mapper = BookShopContextFactory.BookMapper;
        }
        [Fact]
        public async Task getBooks_should_return_right_data()
        {
            BookService sut = new BookService(_bookRepository, _mapper);
            var result = await sut.GetBooksAsync();
            result.ShouldNotBeNull();
        }
        [Theory]
        [InlineData("b5b05534-9263-448c-a69e-0bbd8b3eb90e")]
        public async Task getBook_should_return_right_data(string guid)
        {
            BookService sut = new BookService(_bookRepository, _mapper);
            var result = await sut.GetBookAsync(new GetBookRequest
            {
                Id =
            new Guid(guid)
            });
            result.Id.ShouldBe(new Guid(guid));
        }

        [Fact]
        public void getBook_should_thrown_exception_with_null_id()
        {
            BookService sut = new BookService(_bookRepository, _mapper);
            sut.GetBookAsync(null).ShouldThrow<ArgumentNullException>();
        }


        [Fact]
        public async Task addBook_should_add_right_entity()
        {
            var testBook = new AddBookRequest
            {
                Name = "Test album",
                GenreId = new Guid("c04f05c0-f6ad-44d1-a400-3375bfb5dfd6"),
                AuthorId = new Guid("f08a333d-30db-4dd1-b8ba-3b0473c7cdab"),
            Price = new Price { Amount = 13, Currency = "EUR" }
            };
            IBookService sut = new BookService(_bookRepository, _mapper);

            var result = await sut.AddBookAsync(testBook);
            result.Name.ShouldBe(testBook.Name);
            result.Description.ShouldBe(testBook.Description);
            result.GenreId.ShouldBe(testBook.GenreId);
            result.AuthorId.ShouldBe(testBook.AuthorId);
            result.Price.Amount.ShouldBe(testBook.Price.Amount);
            result.Price.Currency.ShouldBe(testBook.Price.Currency);
        }

        [Fact]
        public async Task editBook_should_add_right_entity()
        {
            var testBook = new EditBookRequest
            {
                Id = new Guid("b5b05534-9263-448c-a69e-0bbd8b3eb90e"),
                Name = "Test album",
                GenreId = new Guid("c04f05c0-f6ad-44d1-a400-3375bfb5dfd6"),
                AuthorId = new Guid("f08a333d-30db-4dd1-b8ba-3b0473c7cdab"),
            Price = new Price { Amount = 13, Currency = "EUR" }
        };
            BookService sut = new BookService(_bookRepository, _mapper);
            var result = await sut.EditBookAsync(testBook);
            result.Name.ShouldBe(testBook.Name);
            result.Description.ShouldBe(testBook.Description);
            result.GenreId.ShouldBe(testBook.GenreId);
            result.AuthorId.ShouldBe(testBook.AuthorId);
            result.Price.Amount.ShouldBe(testBook.Price.Amount);
            result.Price.Currency.ShouldBe(testBook.Price.Currency);
        }
    }
}
