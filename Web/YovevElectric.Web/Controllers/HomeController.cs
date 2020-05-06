namespace YovevElectric.Web.Controllers
{
    using System.Diagnostics;

    using YovevElectric.Web.ViewModels;

    using Microsoft.AspNetCore.Mvc;
    using YovevElectric.Services.Data;
    using YovevElectric.Web.ViewModels.Home;
    using System.Linq;
    using System.Threading.Tasks;

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

        public async Task<IActionResult> Products()
        {
            var products = await this.productService.GetAllProductsAsync();
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
