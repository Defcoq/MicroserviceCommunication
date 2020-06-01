using AutoMapper;
using BookShop.Domain.Entities;
using BookShop.Services.Requests.Book;
using BookShop.Services.Responses.Book;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.Services.Mappers
{
   public class CatalogProfile : Profile
    {
        public CatalogProfile()
        {
            CreateMap<BookResponse, Book>().ReverseMap();
            CreateMap<GenreResponse, Genre>().ReverseMap();
            CreateMap<AuthorResponse, Author>().ReverseMap();
            CreateMap<Price, PriceResponse>().ReverseMap();
            CreateMap<AddBookRequest, Book>().ReverseMap();
            CreateMap<EditBookRequest, Book>().ReverseMap();
        }
    }
}
