namespace YovevElectric.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using YovevElectric.Data.Common.Repositories;
    using YovevElectric.Data.Models;
    using YovevElectric.Services.Data;

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet("{name}")]
        public async Task<ICollection<string>> GetSubCategories(string name)
        {
            var subCategories = await this.categoryService.GetSubCategoriesByCategoryNameAsync(name);
            return subCategories.Select(x => x.Name).ToList();
        }

        [HttpGet]
        public async Task<ICollection<string>> GetCategories()
        {
            var categories = await this.categoryService.GetAllCategoriesAsync();
            return categories.Select(x => x.Name).ToList();
        }
    }
}