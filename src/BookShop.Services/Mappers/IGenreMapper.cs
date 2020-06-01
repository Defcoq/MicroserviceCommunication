﻿using BookShop.Domain.Entities;
using BookShop.Services.Responses.Book;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.Services.Mappers
{
    public interface IGenreMapper
    {
        GenreResponse Map(Genre genre);
    }
}
