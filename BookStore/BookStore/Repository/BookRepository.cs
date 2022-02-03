using BookStore.Models;
using System.Collections.Generic;
using System.Linq;

namespace BookStore.Repository
{
    public class BookRepository
    {
        public List<BookModel> GetAllBooks() 
        {
            return DataSource();
        }
        public BookModel GetBookById(int id)
        {
            return DataSource().Where(x => x.Id == id).FirstOrDefault();
        }
        public List<BookModel> SearchBook(string title, string authorname)
        {
            return DataSource().Where(x => x.Title.Contains(title) && x.Author.Contains(authorname)).ToList();

        }
        private List<BookModel> DataSource()
        {
            return new List<BookModel>()
            {
                new BookModel(){Id = 1, Title = "After Many a Summer Dies the Swan", Author = "Aldous Huxley" },
                new BookModel(){Id = 2, Title = "Ah, Wilderness!", Author = "Eugene O'Neill" },
                new BookModel(){Id = 3, Title = "Alien Corn", Author = "Sidney Howard" },
                new BookModel(){Id = 4, Title = "The Alien Corn", Author = "W.Somerset Maugham" },
                new BookModel(){Id = 5, Title = "All Passion Spent", Author = "Vita Sackville-West" },
            };
        }


    }
}
