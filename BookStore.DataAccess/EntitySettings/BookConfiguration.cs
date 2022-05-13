using BookStore.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.EntitySettings
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable(nameof(Book)).HasKey(x => x.Id);

            builder.HasMany(p => p.Publishers)
                .WithMany(p => p.Books);

            builder.HasMany(g=>g.Genres)
                .WithMany(g => g.Books);

            builder.HasOne(a => a.Author)
                .WithMany(b=>b.Books);
        }
    }
}
