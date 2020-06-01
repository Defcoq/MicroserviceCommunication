using BookShop.Domain.Entities;
using BookShop.Services.Requests.Book;
using BookShop.Services.Responses.Book;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.Services.Mappers
{
    public class BookMapper : IBookMapper
    {
        private readonly IAuthorMapper _authorMapper;
        private readonly IGenreMapper _genreMapper;

        public BookMapper(IAuthorMapper authorMapper, IGenreMapper genreMapper)
        {
            _authorMapper = authorMapper;
            _genreMapper = genreMapper;
        }
        public Book Map(AddBookRequest request)
        {
            if (request == null) return null;
            var item = new Book
{
                Name = request.Name,
                Description = request.Description,
                LabelName = request.LabelName,
                PictureUri = request.PictureUri,
                ReleaseDate = request.ReleaseDate,
                Format = request.Format,
                AvailableStock = request.AvailableStock,
                GenreId = request.GenreId,
                AuthorId = request.AuthorId,
            };
            if (request.Price != null)
            {
                item.Price = new Price
                {
                    Currency = request.Price.Currency,
                    Amount = request.Price.Amount
                };
            }
            return item;
          
        }

        public Book Map(EditBookRequest request)
        {
            if (request == null) return null;

            var item = new Book
            {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description,
                LabelName = request.LabelName,
                PictureUri = request.PictureUri,
                ReleaseDate = request.ReleaseDate,
                Format = request.Format,
                AvailableStock = request.AvailableStock,
                GenreId = request.GenreId,
                AuthorId = request.AuthorId,
            };

            if (request.Price != null)
            {
                item.Price = new Price { Currency = request.Price.Currency, Amount = request.Price.Amount };
            }

            return item;
        }

        public BookResponse Map(Book request)
        {
            if (request == null) return null;

            var response = new BookResponse
            {
                Id = request.Id,
                Name = request.Name,
                Description = request.Description,
                LabelName = request.LabelName,
                PictureUri = request.PictureUri,
                ReleaseDate = request.ReleaseDate,
                Format = request.Format,
                AvailableStock = request.AvailableStock,
                GenreId = request.GenreId,
                Genre = _genreMapper.Map(request.Genre),
                AuthorId = request.AuthorId,
                Author = _authorMapper.Map(request.Author),
            };

            if (request.Price != null)
            {
                response.Price = new PriceResponse { Currency = request.Price.Currency, Amount = request.Price.Amount };
            }

            return response;
        }
    }
}
