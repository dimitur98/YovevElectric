namespace YovevElectric.Web.Controllers
{
    using System.Diagnostics;

    using YovevElectric.Web.ViewModels;

    using Microsoft.AspNetCore.Mvc;
    using YovevElectric.Services.Data;
    using YovevElectric.Web.ViewModels.Home;
    using System.Linq;
    using System.Threading.Tasks;
    using YovevElectric.Common;
    using System;
    using YovevElectric.Web.ViewModels.Category;

    public class HomeController : BaseController
    {
        private readonly IProductsService productService;
        private readonly ICategoryService categoryService;

        public HomeController(IProductsService productService, ICategoryService categoryService)
        {
            this.productService = productService;
            this.categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await this.categoryService.GetAllCategoriesAsync();
            var output = new AllCategoriesAndSubCategoriesViewModell
            {
                Categories = categories.Select(x => new CategoryViewModel
                {
                    Name = x.Name,
                    ImgPath = x.ImgPath,
                }).ToList(),
            };
            return this.View(output);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        public IActionResult Test()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> SiteMap()
        {
            var categories = new SiteMapViewModel
            {
                Categories = await this.categoryService.GetAllCategoriesAsync(),
            };

            return this.View(categories);
        }

        public IActionResult AboutUs()
        {
            return this.View();
        }
    }
}
