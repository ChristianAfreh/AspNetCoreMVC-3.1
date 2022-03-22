using Microsoft.EntityFrameworkCore;

namespace BookStore.Data
{
    public class BookStoreContext : DbContext
    {
        public BookStoreContext(DbContextOptions<BookStoreContext> options)
            : base(options) 
        {

        }

        public DbSet<Book> Book { get; set; }
        public DbSet<Language> Language { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<BookGallery> BookGallery { get; set; }

    }
}
