using BookStore.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.DataAccess.EntitySettings;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.ToTable(nameof(Book)).HasKey(x => x.Id);

        builder.HasMany(p => p.Publishers)
            .WithMany(p => p.Books);

        builder.HasMany(g => g.Genres)
            .WithMany(g => g.Books);

        builder.HasOne(a => a.Author)
            .WithMany(b => b.Books);
    }
}