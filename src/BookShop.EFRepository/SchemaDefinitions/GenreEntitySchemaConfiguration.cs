using BookShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.EFRepository.SchemaDefinitions
{
    public class GenreEntitySchemaConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.ToTable("Genres", BookShopContext.DEFAULT_SCHEMA);
            builder.HasKey(k => k.GenreId);
            builder.Property(p => p.GenreId);
            builder.Property(p => p.GenreDescription)
            .IsRequired()
            .HasMaxLength(1000);
        }
    }
}
