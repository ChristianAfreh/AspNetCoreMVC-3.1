using BookStore.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookStore.Repository
{
    public interface ICategoryRepository
    {
        Task<List<CategoryModel>> GetAllCategories();
    }
}