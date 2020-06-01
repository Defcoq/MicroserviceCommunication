using BookShop.Services.Contracts;
using BookShop.Services.Mappers;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BookShop.Services.Extensions
{
    public static class DependenciesRegistration
    {
        public static IServiceCollection AddMappers(this IServiceCollection
        services)
        {
            services
            .AddSingleton<IAuthorMapper, AuthorMapper>()
            .AddSingleton<IGenreMapper, GenreMapper>()
            .AddSingleton<IBookMapper, BookMapper>();
            return services;
        }
        public static IServiceCollection AddServices(this
        IServiceCollection services)
        {
            services
            .AddScoped<IBookService, BookService>()
            .AddScoped<IAuthorService, AuthorService>()
            .AddScoped<IGenreService, GenreService>();
            return services;
        }
        public static IMvcBuilder AddValidation(this IMvcBuilder builder)
        {
            builder
            .AddFluentValidation(configuration =>
            configuration.RegisterValidatorsFromAssembly
            (Assembly.GetExecutingAssembly()));
            return builder;
        }
    }
}
