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
    }
}
