
using BookStore.API.Data;
using BookStore.Domain.Models;
using DataAccess.EntitySettings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DataAccess.DataBase
{
    public class BookStoreDatabase : IdentityDbContext<ApiUser>
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
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Name="User",
                    NormalizedName="USER",
                    Id= "edd63545-eb9f-46f0-bb37-e2e000cb4193"
                },
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    Id = "e270b1f3-5780-40f1-817f-0852cca1b394"
                });

            var hasher = new PasswordHasher<ApiUser>();
            builder.Entity<ApiUser>().HasData(
                new ApiUser
                {
                    Id= "818c63bc-e500-4815-97fc-ad7fa5a5ab35",
                    Email = "admin@bookstore.com",
                    NormalizedEmail="ADMIN@BOOKSTORE.COM",
                    UserName= "admin@bookstore.com",
                    NormalizedUserName = "ADMIN@BOOKSTORE.COM",
                    FirstName="System",
                    LastName="Admin",
                    PasswordHash = hasher.HashPassword(null,"P@ssword1")
                },
                new ApiUser
                { 
                    Id= "fd4b49ef-60ce-4091-a314-29a571c1bc99",
                    Email = "user@bookstore.com",
                    NormalizedEmail = "USER@BOOKSTORE.COM",
                    UserName = "user@bookstore.com",
                    NormalizedUserName = "USER@BOOKSTORE.COM",
                    FirstName = "System",
                    LastName = "User",
                    PasswordHash = hasher.HashPassword(null, "P@ssword2")
                });

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId= "edd63545-eb9f-46f0-bb37-e2e000cb4193",
                    UserId= "fd4b49ef-60ce-4091-a314-29a571c1bc99"
                },
                new IdentityUserRole<string>
                {
                    RoleId= "e270b1f3-5780-40f1-817f-0852cca1b394",
                    UserId= "818c63bc-e500-4815-97fc-ad7fa5a5ab35"
                });


            new AuthorConfiguration().Configure(builder.Entity<Author>());
            new BookConfiguration().Configure(builder.Entity<Book>());
            new GenreConfiguration().Configure(builder.Entity<Genre>());
            new PublisherConfiguration().Configure(builder.Entity<Publisher>());
            
        }
    }
}
