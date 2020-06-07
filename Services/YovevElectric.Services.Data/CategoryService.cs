using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YovevElectric.Common;
using YovevElectric.Data.Common.Repositories;
using YovevElectric.Data.Models;
using YovevElectric.Web.ViewModels.Category;

namespace YovevElectric.Services.Data
{
    public class CategoryService : ICategoryService
    {
        private readonly IDeletableEntityRepository<Category> categoryRepository;
        private readonly IDeletableEntityRepository<SubCategory> subCategoryRepository;
        private readonly IImgService imgService;

        public CategoryService(
            IDeletableEntityRepository<Category> categoryRepository,
            IDeletableEntityRepository<SubCategory> subCategoryRepository,
            IImgService imgService)
        {
            this.categoryRepository = categoryRepository;
            this.subCategoryRepository = subCategoryRepository;
            this.imgService = imgService;
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

        public async Task CreateCategoryAsync(string name, IFormFile img)
        {
            var imgPath = string.Empty;
            if (img != null)
            {
                imgPath = await this.imgService.UploadImgAsync(img);
            }
            else
            {
                imgPath = GlobalConstants.DefaultImgProduct;
            }

            var category = new Category
            {
                Name = name,
                ImgPath = imgPath,
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

        public async Task HardDeleteCategoryByIdAsync(string id)
        {
            var category = await this.categoryRepository.AllWithDeleted().FirstOrDefaultAsync(x => x.Id == id);
            var subCategories = await this.subCategoryRepository.AllWithDeleted().Where(x => x.CategoryId == id).ToListAsync();
            if (subCategories != null)
            {
                foreach (var subCategory in subCategories)
                {
                    this.subCategoryRepository.HardDelete(subCategory);
                    await this.subCategoryRepository.SaveChangesAsync();
                }
            }

            this.categoryRepository.HardDelete(category);
            await this.categoryRepository.SaveChangesAsync();
        }

        public async Task HardDeleteSubCategoryByIdAsync(string id)
        {
            var subCategory = await this.subCategoryRepository.AllWithDeleted().FirstOrDefaultAsync(x => x.Id == id);

            this.subCategoryRepository.HardDelete(subCategory);
            await this.subCategoryRepository.SaveChangesAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(string id)
            => await this.categoryRepository.All().FirstOrDefaultAsync(x => x.Id == id);

        public async Task EditCategoryByIdAsync(string id, CategoryInputModel input)
        {
            var category = await this.GetCategoryByIdAsync(id);

            category.ImgPath = await this.imgService.UploadImgAsync(input.Img);
            category.Name = input.Name;

            this.categoryRepository.Update(category);
            await this.categoryRepository.SaveChangesAsync();
        }
    }
}
