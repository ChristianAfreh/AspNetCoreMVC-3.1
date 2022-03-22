﻿using BookStore.Models;
using BookStore.Repository;
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
        private readonly BookRepository _bookRepository = null;
        private readonly LanguageRepository _languageRepository = null;
        private readonly CategoryRepository _categoryRepository = null;
        private readonly IWebHostEnvironment _webHostEnvironment = null;

        public BookController(BookRepository bookRepository, LanguageRepository languageRepository, IWebHostEnvironment webHostEnvironment,CategoryRepository categoryRepository)
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
        public async Task<ViewResult> AddNewBook(bool isSuccess = false, int bookId = 0)
        {
            ViewBag.Language = new SelectList(await _languageRepository.GetLanguages(),"Id","Name");
            ViewBag.Category = new SelectList(await _categoryRepository.GetAllCategories(),"Id","Name");


            //var model = new BookModel()
            //{
            //    LanguageId = 2
            //};

            ViewBag.IsSuccess = isSuccess;
            ViewBag.BookId = bookId;
            return View();
            //model
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
    }
}

