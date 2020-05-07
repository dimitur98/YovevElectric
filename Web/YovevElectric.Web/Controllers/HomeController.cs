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

    public class HomeController : BaseController
    {
        private readonly IProductsService productService;

        public HomeController(IProductsService productService)
        {
            this.productService = productService;
        }

        public IActionResult Index()
        {
            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        public async Task<IActionResult> Products(int page = 1)
        {
            var count = await this.productService.GetProductsCount();
            var pagesCount = (int)Math.Ceiling((double)count / GlobalConstants.ItemsPerPage);
            var products = await this.productService.GetAllProductsAsync((page - 1) * GlobalConstants.ItemsPerPage);
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
            };

            return this.View(output);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
