using BookStore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class BookRepository
    {
        private readonly BookStoreContext _context = null;
        public BookRepository(BookStoreContext context)
        {
            _context = context;
        }

        public async Task<int> AddNewBook(BookModel model)
        {
            var newBook = new Book()
            {   
                Title = model.Title,
                Author = model.Author,
                Description = model.Description,
                TotalPages = model.TotalPages.HasValue ? model.TotalPages.Value : 0,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,    
            };
            
            await _context.Book.AddAsync(newBook);
            await _context.SaveChangesAsync();

            return newBook.Id;
            
        }
        public async Task<List<BookModel>> GetAllBooks() 
        {
            var books = new List<BookModel>();
            var allbooks = await _context.Book.ToListAsync();
            if(allbooks?.Any() == true)
            {
                foreach(var book in allbooks)
                {
                    books.Add(new BookModel()
                    {
                        Author = book.Author,
                        Category = book.Category,
                        Description = book.Description,
                        Id = book.Id,
                        Language = book.Language,
                        Title = book.Title,
                        TotalPages = book.TotalPages
                    });
                }
            }
            return books;
        }
        public async Task<BookModel> GetBookById(int id)
        {
            var book = await _context.Book.FindAsync(id);
            if(book != null)
            {
                var bookDetails = new BookModel()
                {
                    Author = book.Author,
                    Category = book.Category,
                    Description = book.Description,
                    Id = book.Id,
                    Language = book.Language,
                    Title = book.Title,
                    TotalPages = book.TotalPages
                };
                return bookDetails;
            }
            return null;
        }
        public List<BookModel> SearchBook(string title, string authorname)
        {
            return DataSource().Where(x => x.Title.Contains(title) && x.Author.Contains(authorname)).ToList();

        }
        private List<BookModel> DataSource()
        {
            return new List<BookModel>()
            {
                new BookModel(){Id = 1, Title = "After Many a Summer Dies the Swan", Author = "Aldous Huxley", Description="This is the description of the book.", Category="Action and Adventure", Language="English", TotalPages= 1000},
                new BookModel(){Id = 2, Title = "Ah, Wilderness!", Author = "Eugene O'Neill", Description="This is the description of the book.", Category="Classics", Language="English", TotalPages= 1500 },
                new BookModel(){Id = 3, Title = "Alien Corn", Author = "Sidney Howard", Description="This is the description of the book.", Category="Comic Book", Language="English", TotalPages= 900 },
                new BookModel(){Id = 4, Title = "The Alien Corn", Author = "W.Somerset Maugham", Description="This is the description of the book.", Category="Fantasy", Language="English", TotalPages= 1740 },
                new BookModel(){Id = 5, Title = "All Passion Spent", Author = "Vita Sackville-West", Description="This is the description of the book.", Category="Horror", Language="English", TotalPages= 1490 },
                new BookModel(){Id = 6, Title = "The Terrible, Horrible, Very Bad Good News", Author = "Meghna Pant", Description= "This is the description of the book.", Category="Literary Fiction", Language="English", TotalPages= 2000},
            };
        }


    }
}
