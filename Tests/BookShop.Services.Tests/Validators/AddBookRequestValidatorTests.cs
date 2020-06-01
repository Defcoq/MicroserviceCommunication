using BookShop.Domain.Entities;
using BookShop.Services.Contracts;
using BookShop.Services.Requests.Authors;
using BookShop.Services.Requests.Book;
using BookShop.Services.Requests.Book.Validators;
using BookShop.Services.Requests.Genre;
using FluentValidation.TestHelper;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace BookShop.Services.Tests.Validators
{
    public class AddBookRequestValidatorTests
    {
        //private readonly AddBookRequestValidator _validator;

        public AddBookRequestValidatorTests()
        {

            _artistServiceMock = new Mock<IAuthorService>();
            _artistServiceMock
                .Setup(x => x.GetAuthorAsync(It.IsAny<GetAuthorRequest>()))
                .ReturnsAsync(() => null);

            _genreServiceMock = new Mock<IGenreService>();
            _genreServiceMock
                .Setup(x => x.GetGenreAsync(It.IsAny<GetGenreRequest>()))
                .ReturnsAsync(() => null);

            _validator = new AddBookRequestValidator(_artistServiceMock.Object, _genreServiceMock.Object);
        }

        private readonly Mock<IAuthorService> _artistServiceMock;
        private readonly Mock<IGenreService> _genreServiceMock;
        private readonly AddBookRequestValidator _validator;

        [Fact]
        public void should_have_error_when_AuthorId_doesnt_exist()
        {
            _artistServiceMock
                .Setup(x => x.GetAuthorAsync(It.IsAny<GetAuthorRequest>()))
                .ReturnsAsync(() => null);

            var addBookRequest = new AddBookRequest { Price = new Price(), AuthorId = Guid.NewGuid() };
            _validator.ShouldHaveValidationErrorFor(x => x.AuthorId, addBookRequest);
        }


        [Fact]
        public void should_have_error_when_AuthorId_is_null()
        {
            var addBookRequest = new AddBookRequest { Price = new Price() };
            _validator.ShouldHaveValidationErrorFor(x => x.AuthorId, addBookRequest);
        }

        [Fact]
        public void should_have_error_when_GenreId_doesnt_exist()
        {
            _genreServiceMock
                .Setup(x => x.GetGenreAsync(It.IsAny<GetGenreRequest>()))
                .ReturnsAsync(() => null);

            var addBookRequest = new AddBookRequest { Price = new Price(), GenreId = Guid.NewGuid() };
            _validator.ShouldHaveValidationErrorFor(x => x.GenreId, addBookRequest);
        }

        [Fact]
        public void should_have_error_when_GenreId_is_null()
        {
            var addBookRequest = new AddBookRequest { Price = new Price() };
            _validator.ShouldHaveValidationErrorFor(x => x.GenreId, addBookRequest);
        }
    }

}
