using BookStore.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly BookRepository _bookRepository = null;
        private readonly LanguageRepository _languageRepository = null;

        public BookController(BookRepository bookRepository, LanguageRepository languageRepository)
        {
            _bookRepository = bookRepository;
            _languageRepository = languageRepository;
        }


        public async Task<ViewResult> GetAllBooks()
        {
            var data = await _bookRepository.GetAllBooks();
            return View(data);
        }

        public async Task<ViewResult> GetBook(int id) 
        {
            var data = await _bookRepository.GetBookById(id);
            return View(data);  
        }
        public List<BookModel> SearchBooks(string bookName, string authorName) 
        {
            return _bookRepository.SearchBook(bookName, authorName);
        }
        public async Task<ViewResult> AddNewBook(bool isSuccess = false, int bookId = 0)
        {
            ViewBag.Language = new SelectList(await _languageRepository.GetLanguages(),"Id","Name");


            var model = new BookModel()
            {
                LanguageId = 2
            };

            ViewBag.IsSuccess = isSuccess;
            ViewBag.BookId = bookId;
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddNewBook(BookModel bookModel)
        {
            ViewBag.Language = new SelectList(await _languageRepository.GetLanguages(), "Id", "Name");

            if (ModelState.IsValid)
            {
                int id = await _bookRepository.AddNewBook(bookModel);    
                if (id > 0)
                {
                    return RedirectToAction(nameof(AddNewBook), new { isSuccess = true, bookId = id });
                }
            }
           
            return View();

        }
        
    }
}

