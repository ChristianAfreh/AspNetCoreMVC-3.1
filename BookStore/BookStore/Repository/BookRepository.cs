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
                CategoryId = model.CategoryId,
                LanguageId = model.LanguageId,
                TotalPages = model.TotalPages.HasValue ? model.TotalPages.Value : 0,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
                CoverPhotoUrl = model.CoverPhotoUrl,
            };

            newBook.bookGallery = new List<BookGallery>();
            
            foreach(var file in model.Gallery)
            {
                newBook.bookGallery.Add(new BookGallery()
                {
                    Name = file.Name,
                    URL = file.URL,
                });
            }
            
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
                        CategoryId = book.CategoryId,
                        Description = book.Description,
                        Id = book.Id,
                        LanguageId = book.LanguageId,
                        Title = book.Title,
                        TotalPages = book.TotalPages,
                        CoverPhotoUrl = book.CoverPhotoUrl,
                    });
                }
            }
            return books;
        }
        public async Task<BookModel> GetBookById(int id)
        {
            var book = await _context.Book.Where(x => x.Id == id).Select(book => new BookModel()
          

                {
                    Author = book.Author,
                    CategoryId = book.CategoryId,
                    Category = book.Category.Name,
                    Description = book.Description,
                    Id = book.Id,
                    LanguageId = book.LanguageId,
                    Language = book.Language.Name,
                    Title = book.Title,
                    TotalPages = book.TotalPages,
                    CoverPhotoUrl = book.CoverPhotoUrl,
                    Gallery = book.bookGallery.Select(g => new GalleryModel()
                    {
                        Id = g.Id,
                        Name = g.Name,
                        URL = g.URL
                    }).ToList()
                }).FirstOrDefaultAsync();

            return book;
        }
        public List<BookModel> SearchBook(string title, string authorname)
        {
            return null;

        }


    }
}
