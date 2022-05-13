
using BookStore.API.Data;
using DataAccess.EntitySettings;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataAccess.DataBase
{
    public class BookStoreDatabase : DbContext
    {
        public BookStoreDatabase()
        {
        }

        public BookStoreDatabase(DbContextOptions<BookStoreDatabase> options) : base(options)
        {
        }

        //DbSets [Entities]
        public virtual DbSet<Author> Authors { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Publisher> Publishers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=BookStoreDatabase;Integrated Security=True;Pooling=False");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            new AuthorConfiguration().Configure(builder.Entity<Author>());
            new BookConfiguration().Configure(builder.Entity<Book>());
            new GenreConfiguration().Configure(builder.Entity<Genre>());
            new PublisherConfiguration().Configure(builder.Entity<Publisher>());
            
        }
    }
}
