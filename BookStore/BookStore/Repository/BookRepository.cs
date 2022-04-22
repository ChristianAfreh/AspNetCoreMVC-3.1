using BookStore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class BookRepository : IBookRepository
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
                BookPdfUrl = model.BookPdfUrl
            };

            newBook.bookGallery = new List<BookGallery>();

            foreach (var file in model.Gallery)
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
            return await _context.Book
            .Select(book => new BookModel()

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
            }).ToListAsync();
        }
        public async Task<List<BookModel>> GetTopBooksAsync(int count)
        {
            return await _context.Book
               .Select(book => new BookModel()

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
               }).Take(count).ToListAsync();
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
                BookPdfUrl = book.BookPdfUrl,
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

        public async Task<int> UpdateBookAsync(BookModel model)
        {
            var book = await _context.Book.FirstOrDefaultAsync(x => x.Id == model.Id);
            if(book != null)
            {
                book.Title = model.Title;
                book.Author = model.Author;
                book.Description = model.Description;
                book.LanguageId = model.LanguageId;
                book.CategoryId = model.CategoryId;
                book.TotalPages = model.TotalPages;
                book.CoverPhotoUrl = model.CoverPhotoUrl;
                book.BookPdfUrl = model.BookPdfUrl;
                book.bookGallery = new List<BookGallery>();
                foreach (var file in model.Gallery)
                {
                    book.bookGallery.Add(new BookGallery()
                    {
                        Name = file.Name,
                        URL = file.URL
                    });
                }
            };

           _context.Book.Update(book);
            _context.SaveChanges();

            return book.Id;
        }


    }
}
