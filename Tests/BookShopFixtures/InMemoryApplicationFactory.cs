using BookShop.EFRepository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookShopFixtures
{
          public class InMemoryApplicationFactory<TStartup>
        : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder
            .UseEnvironment("Testing")
            .ConfigureTestServices(services =>
            {
                var options = new  DbContextOptionsBuilder<BookShopContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
                services.AddScoped<BookShopContext>(serviceProvider =>
                new TestBookShopContext(options));
                var sp = services.BuildServiceProvider();
                using var scope = sp.CreateScope();
                var scopedServices = scope.ServiceProvider;
                var db = scopedServices.GetRequiredService<BookShopContext>();
                db.Database.EnsureCreated();
            });
        }
    }
}
