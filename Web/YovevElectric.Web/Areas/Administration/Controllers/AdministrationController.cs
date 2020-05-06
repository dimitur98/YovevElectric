namespace YovevElectric.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using YovevElectric.Common;
    using YovevElectric.Data.Models;
    using YovevElectric.Services.Data;
    using YovevElectric.Web.Controllers;
    using YovevElectric.Web.ViewModels.Home;
    using YovevElectric.Web.ViewModels.Img;
    using YovevElectric.Web.ViewModels.Product;

    //[Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
        private readonly IProductsService productsService;
        private readonly IImgService imgService;

        public AdministrationController(IProductsService productsService, IImgService imgService)
        {
            this.productsService = productsService;
            this.imgService = imgService;
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
            var id = await this.productsService.CreateProductAsync(input);

            return this.Redirect($"/Administration/Img/ImgUpload?id={id}");
        }

        public async Task<IActionResult> EditProduct(string id)
        {
            var product = await this.productsService.GetProductByIdAsync(id);
            var output = new EditProductInputModel
            {
                Id = product.Id,
                Category = product.Category,
                Description = product.Description,
                Price = product.Price,
                Title = product.Title,
            };

            return this.View(output);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(string id, EditProductInputModel input)
        {
            await this.productsService.EditProductAsync(id, input);
            return this.Redirect("/Home/Products");
        }
    }
}
