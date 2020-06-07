using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YovevElectric.Data.Models;
using YovevElectric.Web.ViewModels.Category;

namespace YovevElectric.Services.Data
{
    public interface ICategoryService
    {
        Task CreateSubCategoryAsync(string subCategoryName, string categoryName);

        Task CreateCategoryAsync(string name, IFormFile img);

        Task<string> GetCategoryIdByNameAsync(string name);

        Task<ICollection<SubCategory>> GetSubCategoriesByCategoryNameAsync(string id);

        Task<ICollection<SubCategory>> GetSubCategoriesWithDeletedByCategoryNameAsync(string name);

        Task<ICollection<Category>> GetAllCategoriesAsync();


        Task<ICollection<Category>> GetAllCategoriesWithDeletedAsync();

        Task HardDeleteCategoryByIdAsync(string id);

        Task HardDeleteSubCategoryByIdAsync(string id);

        Task<Category> GetCategoryByIdAsync(string id);

        Task EditCategoryByIdAsync(string id, CategoryInputModel input);
    }
}
