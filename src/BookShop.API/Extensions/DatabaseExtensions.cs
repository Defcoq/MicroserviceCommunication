using BookShop.EFRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.API.Extensions
{
    public static class DatabaseExtensions
    {
        public static IServiceCollection AddBookShopContext(this IServiceCollection services, string connectionString)
        {
            return services.AddEntityFrameworkSqlServer()
                    .AddDbContext<BookShopContext>(contextOptions =>
                    {
                        contextOptions.UseSqlServer(connectionString,
                    serverOptions =>
                    {
                        serverOptions.MigrationsAssembly(typeof(Startup).Assembly.FullName);
                    });
                    });
        }
}
}
