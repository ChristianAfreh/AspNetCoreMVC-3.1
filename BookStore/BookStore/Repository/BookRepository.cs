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
