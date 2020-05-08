using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YovevElectric.Data.Common.Repositories;
using YovevElectric.Data.Models;

namespace YovevElectric.Services.Data
{
    public class CategoryService : ICategoryService
    {
        private readonly IDeletableEntityRepository<Category> categoryRepository;
        private readonly IDeletableEntityRepository<SubCategory> subCategoryRepository;

        public CategoryService(IDeletableEntityRepository<Category> categoryRepository, IDeletableEntityRepository<SubCategory> subCategoryRepository)
        {
            this.categoryRepository = categoryRepository;
            this.subCategoryRepository = subCategoryRepository;
        }

        public async Task<ICollection<Category>> GetAllCategoriesAsync() => await this.categoryRepository.All().ToListAsync();

        public async Task<ICollection<Category>> GetAllCategoriesWithDeletedAsync() => await this.categoryRepository.AllWithDeleted().ToListAsync();

        public async Task<ICollection<SubCategory>> GetSubCategoriesByCategoryNameAsync(string name)
            => await this.subCategoryRepository.All().Where(x => x.Category.Name == name).ToListAsync();

        public async Task<ICollection<SubCategory>> GetSubCategoriesWithDeletedByCategoryNameAsync(string name)
           => await this.subCategoryRepository.AllWithDeleted().Where(x => x.Category.Name == name).ToListAsync();

        public async Task<string> GetCategoryIdByNameAsync(string name)
        {
            var category = await this.categoryRepository.All().FirstOrDefaultAsync(x => x.Name == name);
            return category.Id;
        }

        public async Task CreateCategoryAsync(string name)
        {
            var category = new Category
            {
                Name = name,
            };

            await this.categoryRepository.AddAsync(category);
            await this.categoryRepository.SaveChangesAsync();
        }

        public async Task CreateSubCategoryAsync(string subCategoryName, string categoryName)
        {
            var subCategory = new SubCategory
            {
                Name = subCategoryName,
                CategoryId = await this.GetCategoryIdByNameAsync(categoryName),
            };

            await this.subCategoryRepository.AddAsync(subCategory);
            await this.subCategoryRepository.SaveChangesAsync();
        }

        public async Task DeleteUnDeleteCategoryByNameAsync(string name)
        {
            var category = await this.categoryRepository.AllWithDeleted().FirstOrDefaultAsync(x => x.Name == name);
            if (category.IsDeleted)
            {
                category.IsDeleted = false;
            }
            else
            {
                category.IsDeleted = true;
            }

            this.categoryRepository.Update(category);
            await this.categoryRepository.SaveChangesAsync();
        }

        public async Task DeleteUnDeleteSubCategoryByNameAsync(string name)
        {
            var subCategory = await this.subCategoryRepository.AllWithDeleted().FirstOrDefaultAsync(x => x.Name == name);
            if (subCategory.IsDeleted)
            {
                subCategory.IsDeleted = false;
            }
            else
            {
                subCategory.IsDeleted = true;
            }

            this.subCategoryRepository.Update(subCategory);
            await this.subCategoryRepository.SaveChangesAsync();
        }
    }
}
