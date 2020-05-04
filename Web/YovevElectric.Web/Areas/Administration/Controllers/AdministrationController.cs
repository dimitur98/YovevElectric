namespace YovevElectric.Web.Areas.Administration.Controllers
{
    using YovevElectric.Common;
    using YovevElectric.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using YovevElectric.Services.Data;
    using System.Threading.Tasks;
    using YovevElectric.Web.ViewModels.Product;
    using YovevElectric.Data.Models;
    using YovevElectric.Web.ViewModels.Home;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
        private readonly IProductsService productsService;

        public AdministrationController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        public IActionResult Home()
        {
            return this.View();
        }

        public IActionResult CreateProduct()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductInputModel input)
        {
            await this.productsService.CreateProductAsync(input);

            return this.Redirect("Home");
        }

        public async Task<IActionResult> EditProduct(string id)
        {
            var product = await this.productsService.GetProductByIdAsync(id);
            var output = new ProductViewModel
            {
                Category = product.Category,
                Description = product.Description,
                ImgPath = product.ImgPath,
                Price = product.Price,
                Title = product.Title,
            };

            return this.View(output);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(EditProductInputModel input)
        {
            await this.productsService.EditProductAsync(input);
            return this.Redirect("Home");
        }
    }
}
