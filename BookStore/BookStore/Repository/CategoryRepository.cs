using BookStore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public class CategoryRepository
    {
        private readonly BookStoreContext _context;
        public CategoryRepository(BookStoreContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryModel>> GetAllCategories()
        {
            var categories = await _context.Category.Select(x => new CategoryModel()
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,

            }).ToListAsync();

            return categories;
        }
    }
}
