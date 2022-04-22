using BookStore.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository = null;
        private readonly ILanguageRepository _languageRepository = null;
        private readonly ICategoryRepository _categoryRepository = null;
        private readonly IWebHostEnvironment _webHostEnvironment = null;

        public BookController(IBookRepository bookRepository, ILanguageRepository languageRepository, IWebHostEnvironment webHostEnvironment,ICategoryRepository categoryRepository)
        {
            _bookRepository = bookRepository;
            _languageRepository = languageRepository;
            _categoryRepository = categoryRepository;
            _webHostEnvironment = webHostEnvironment;
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

        [Authorize]
        public async Task<ViewResult> AddNewBook(bool isSuccess = false, int bookId = 0)
        {
            ViewBag.Language = new SelectList(await _languageRepository.GetLanguages(),"Id","Name");
            ViewBag.Category = new SelectList(await _categoryRepository.GetAllCategories(),"Id","Name");
            ViewBag.IsSuccess = isSuccess;
            ViewBag.BookId = bookId;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddNewBook(BookModel bookModel)
        {

            if (ModelState.IsValid) 
            {
                if (bookModel.CoverPhoto != null)
                {
                    string folder = "books/cover/";
                    bookModel.CoverPhotoUrl = await UploadImage(folder, bookModel.CoverPhoto);
                }
                if (bookModel.GalleryFiles != null)
                {
                    string folder = "books/gallery/";

                    bookModel.Gallery = new List<GalleryModel>();
                    

                    foreach(var file in bookModel.GalleryFiles)
                    {
                        var gallery = new GalleryModel()
                        {
                            Name = file.FileName,
                            URL = await UploadImage(folder, file),
                        };

                        bookModel.Gallery.Add(gallery);
                    }
                   
                }

                if (bookModel.BookPdf != null)
                {
                    string folder = "books/pdf/";
                    bookModel.BookPdfUrl = await UploadImage(folder, bookModel.BookPdf);
                }

                int id = await _bookRepository.AddNewBook(bookModel);    
                if (id > 0)
                {
                    return RedirectToAction(nameof(AddNewBook), new { isSuccess = true, bookId = id });
                }
            }

            ViewBag.Language = new SelectList(await _languageRepository.GetLanguages(), "Id", "Name");
            ViewBag.Category = new SelectList(await _categoryRepository.GetAllCategories(), "Id", "Name");

            return View();

        }

        private async Task<string> UploadImage(string folderPath,IFormFile file)
        { 
            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;

            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);

            await file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            
            return  "/" + folderPath;
        }
        
        [Authorize]
        public async Task<IActionResult> UpdateBook(int Id, bool isSuccess = false )
        {
            ViewBag.Language = new SelectList(await _languageRepository.GetLanguages(), "Id", "Name");
            ViewBag.Category = new SelectList(await _categoryRepository.GetAllCategories(), "Id", "Name");
            ViewBag.IsSuccess = isSuccess;
            ViewBag.BookId = Id;
            try
            {
                var result = await _bookRepository.GetBookById(Id);
                return View(result);
            }
            catch(Exception ex)
            {
                var errorViewModel = new ErrorViewModel()
                {
                    RequestId = ex.Message
                };

                return View("Error", errorViewModel);
            }
        }
        [HttpPost]
        public async Task<IActionResult> UpdateBook(BookModel model, int bookId)
        {
            bookId = model.Id;
            ViewBag.Language = new SelectList(await _languageRepository.GetLanguages(), "Id", "Name");
            ViewBag.Category = new SelectList(await _categoryRepository.GetAllCategories(), "Id", "Name");

            if (ModelState.IsValid)
            {
                if (model.CoverPhoto != null)
                {
                    string folder = "books/cover/";
                    model.CoverPhotoUrl = await UploadImage(folder, model.CoverPhoto);
                }
                if (model.GalleryFiles != null)
                {
                    string folder = "books/gallery/";

                    model.Gallery = new List<GalleryModel>();


                    foreach (var file in model.GalleryFiles)
                    {
                        var gallery = new GalleryModel()
                        {
                            Name = file.FileName,
                            URL = await UploadImage(folder, file),
                        };

                        model.Gallery.Add(gallery);
                    }

                }

                if (model.BookPdf != null)
                {
                    string folder = "books/pdf/";
                    model.BookPdfUrl = await UploadImage(folder, model.BookPdf);
                }

                var Id = await _bookRepository.UpdateBookAsync(model);
                if (Id > 0)
                {
                    return RedirectToAction(nameof(UpdateBook), new { isSuccess = true, Id = bookId });
                }
            }

            return View();
        }
        public IActionResult DeleteBook()
        {
           return View();
        }
    }
}

