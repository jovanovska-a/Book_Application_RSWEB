using e_shop.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace e_shop.Data
{
    public class AppDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public AppDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to postgres with connection string from app settings
            object value = options.UseNpgsql(Configuration.GetConnectionString("ConnectionString"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<BookGenre>().HasKey(bg => new
            {
                bg.BookId,
                bg.GenreId
            });
            modelBuilder.Entity<BookGenre>().HasOne(b => b.Book).WithMany(bg => bg.Books_Genres).HasForeignKey(b => b.BookId);
            modelBuilder.Entity<BookGenre>().HasOne(b => b.Genre).WithMany(bg => bg.Books_Genres).HasForeignKey(b => b.GenreId);
            
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<UserBook> UserBooks { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<BookGenre> Books_Genres { get; set; }


    }
}

 