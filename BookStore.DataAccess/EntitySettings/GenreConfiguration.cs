using BookStore.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.DataAccess.EntitySettings;

public class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder.ToTable(nameof(Genre)).HasKey(x => x.Id);

        builder.HasMany(b => b.Books)
            .WithMany(g => g.Genres);
    }
}