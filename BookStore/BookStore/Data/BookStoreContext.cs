using BookStore.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data
{
    public class BookStoreContext : IdentityDbContext<ApplicationUser>
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
