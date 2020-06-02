using System;
using Cart.Domain.Services;
using Cart.Infrastructure.Extensions.Policies;
using Cart.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using BookShop.API.Client;

namespace Cart.Infrastructure.Extensions
{
    public static class BookShopServiceExtensions
    {
        public static IServiceCollection AddBookShopService(this IServiceCollection services, Uri uri)
        {
            services.AddScoped<IBookShopService, BookShopService>();

            services.AddHttpClient<IBookShopClient, BookShopClient>(client => { client.BaseAddress = uri; })
                .SetHandlerLifetime(TimeSpan.FromMinutes(2)) //Set lifetime to 2 minutes
                .AddPolicyHandler(BookShopServicePolicies.RetryPolicy())
                .AddPolicyHandler(BookShopServicePolicies.CircuitBreakerPolicy());

            return services;
        }
    }
}