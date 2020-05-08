using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YovevElectric.Data.Models;

namespace YovevElectric.Services.Data
{
    public interface ICategoryService
    {
        Task CreateSubCategoryAsync(string subCategoryName, string categoryName);

        Task CreateCategoryAsync(string name);

        Task<string> GetCategoryIdByNameAsync(string name);

        Task<ICollection<SubCategory>> GetSubCategoriesByCategoryNameAsync(string id);

        Task<ICollection<SubCategory>> GetSubCategoriesWithDeletedByCategoryNameAsync(string name);

        Task<ICollection<Category>> GetAllCategoriesAsync();

        Task DeleteUnDeleteCategoryByNameAsync(string name);

        Task DeleteUnDeleteSubCategoryByNameAsync(string name);

        Task<ICollection<Category>> GetAllCategoriesWithDeletedAsync();
    }
}
