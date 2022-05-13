﻿using BookStore.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace DataAccess.EntitySettings
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.ToTable(nameof(Author)).HasKey(i => i.Id);

            builder.HasMany(b => b.Books)
                .WithOne(b => b.Author);
        }
    }
}
