using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YovevElectric.Common;
using YovevElectric.Services.Data;
using YovevElectric.Web.ViewModels.Home;
using YovevElectric.Web.ViewModels.Product;

namespace YovevElectric.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsService productsService;
        private readonly ICategoryService categoryService;

        public ProductsController(IProductsService productsService, ICategoryService categoryService)
        {
            this.productsService = productsService;
            this.categoryService = categoryService;
        }

        public async Task<IActionResult> Products(int page = 1, string category = null, string subCategory = null)
        {
            var products = await this.productsService.GetAllProductsAsync((page - 1) * GlobalConstants.ItemsPerPage, category, subCategory);
            var count = await this.productsService.GetProductsCount(category, subCategory);
            var pagesCount = (int)Math.Ceiling((double)count / GlobalConstants.ItemsPerPage);

            var output = new AllProductsViewModel
            {
                AllProducts = products.Select(x => new ProductViewModel
                {
                    Id = x.Id,
                    Category = x.Category,
                    Description = x.Description,
                    ImgPath = x.ImgPath,
                    Price = x.Price,
                    Title = x.Title,
                }).ToList(),
                CurrentPage = page,
                PagesCount = pagesCount == 0 ? 1 : pagesCount,
                Categories = await this.categoryService.GetAllCategoriesAsync(),
            };
            if (category != null)
            {
                this.ViewData["category"] = category;
            }

            if (subCategory != null)
            {
                this.ViewData["subCategory"] = subCategory;
            }
            return this.View(output);
        }

        public async Task<IActionResult> Details(string id)
        {
            var product = await this.productsService.GetProductByIdAsync(id);

            var output = new ProductDetailsModel
            {
                ProductDetailsViewModel = new ProductDetailsViewModel
                {
                    Id = product.Id,
                    Title = product.Title,
                    Description = product.Description,
                    Category = product.Category,
                    SubCategory = product.SubCategory,
                    Price = product.Price,
                    ImgPath = product.ImgPath,
                },
            };
            return this.View(output);
        }
    }
}
