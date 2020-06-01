using BookShop.Domain.Entities;
using BookShop.Services.Responses.Book;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.Services.Mappers
{
    public class AuthorMapper : IAuthorMapper
    {
        public AuthorResponse Map(Author author)
        {
            if (author == null) return null;
            return new AuthorResponse
            {
                AuthorId = author.AuthorId,
                AuthorName = author.AuthorName
            };
        }
    }
}
