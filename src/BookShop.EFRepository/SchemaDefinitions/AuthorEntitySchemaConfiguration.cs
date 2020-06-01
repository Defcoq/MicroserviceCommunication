using BookShop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookShop.EFRepository.SchemaDefinitions
{
    public class AuthorEntitySchemaConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.ToTable("Authors", BookShopContext.DEFAULT_SCHEMA);
            builder.HasKey(k => k.AuthorId);
            builder.Property(p => p.AuthorId);
            builder.Property(p => p.AuthorName)
            .IsRequired()
            .HasMaxLength(200);
        }
    }
}
