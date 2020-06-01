using BookShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace BookShop.EFRepository.SchemaDefinitions
{
    public class BookEntitySchemaDefinition : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books", BookShopContext.DEFAULT_SCHEMA);
            builder.HasKey(k => k.Id);

            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(1000);
            builder
            .HasOne(e => e.Genre)
            .WithMany(c => c.Books)
            .HasForeignKey(k => k.GenreId);
            builder
            .HasOne(e => e.Author)
            .WithMany(c => c.Books)
            .HasForeignKey(k => k.AuthorId);
            builder.Property(p => p.Price).HasConversion(
            p => $"{p.Amount}:{p.Currency}",
            p => new Price
            {
                Amount = Convert.ToDecimal(
            p.Split(':', StringSplitOptions.None)[0]),
                Currency = p.Split(':', StringSplitOptions.None)[1]
            });
        }
    }
}
