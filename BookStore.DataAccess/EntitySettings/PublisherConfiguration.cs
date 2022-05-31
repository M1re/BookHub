using BookStore.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookStore.DataAccess.EntitySettings;

public class PublisherConfiguration : IEntityTypeConfiguration<Publisher>
{
    public void Configure(EntityTypeBuilder<Publisher> builder)
    {
        builder.ToTable(nameof(Publisher)).HasKey(x => x.Id);

        builder.HasMany(b => b.Books)
            .WithMany(p => p.Publishers);
    }
}