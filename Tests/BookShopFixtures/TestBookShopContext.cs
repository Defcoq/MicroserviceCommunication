using BookShop.Domain.Entities;
using BookShop.EFRepository;
using BookShopFixtures.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookShopFixtures
{
    public class TestBookShopContext : BookShopContext
    {
        public TestBookShopContext(DbContextOptions<BookShopContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed<Author>("./Data/author.json");
            modelBuilder.Seed<Genre>("./Data/genre.json");
            modelBuilder.Seed<Book>("./Data/book.json");
        }
    }
}
