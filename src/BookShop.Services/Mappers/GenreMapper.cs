using BookShop.Domain.Entities;
using BookShop.Services.Responses.Book;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.Services.Mappers
{
    public class GenreMapper : IGenreMapper
    {
        public GenreResponse Map(Genre genre)
        {
            if (genre == null) return null;
            return new GenreResponse
            {
                GenreId = genre.GenreId,
                GenreDescription = genre.GenreDescription
            };
        }
    }
}
