using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookShop.API.Extensions;
using BookShop.Domain.Repositories;
using BookShop.EFRepository;
using BookShop.EFRepository.Repositories;
using BookShop.Services.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BookShop.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
        
            services.AddBookShopContext(Configuration.GetSection("DataSource:ConnectionString").Value)
            .AddScoped<IBookRepository, BookRepository>()
            .AddScoped<IAuthorRepository, AuthorRepository>()
            .AddScoped<IGenreRepository, GenreRepository>()
            .AddMappers()
            .AddServices()
            .AddControllers()
            .AddValidation();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
