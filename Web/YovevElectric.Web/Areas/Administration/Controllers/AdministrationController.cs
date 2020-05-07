namespace YovevElectric.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using YovevElectric.Common;
    using YovevElectric.Data.Models;
    using YovevElectric.Services.Data;
    using YovevElectric.Web.Controllers;
    using YovevElectric.Web.ViewModels.Category;
    using YovevElectric.Web.ViewModels.Home;
    using YovevElectric.Web.ViewModels.Img;
    using YovevElectric.Web.ViewModels.Product;

    //[Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
        private readonly IProductsService productsService;
        private readonly IImgService imgService;
        private readonly ICategoryService categoryService;

        public AdministrationController(IProductsService productsService, IImgService imgService, ICategoryService categoryService)
        {
            this.productsService = productsService;
            this.imgService = imgService;
            this.categoryService = categoryService;
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
            var output = new EditProductModel
            {
                EditProductInputModel = new EditProductInputModel
                {
                    Id = product.Id,
                    Category = product.Category,
                    Description = product.Description,
                    Price = product.Price,
                    Title = product.Title,
                },
                ImgEditModel = new ImgEditModel
                {
                    ImgEditViewModel = new ImgEditViewModel
                    {
                        ImgPath = product.ImgPath,
                        ProductId = product.Id,
                    },
                },
            };

            return this.View(output);
        }

        [HttpPost]
        public async Task<IActionResult> EditProduct(string id, EditProductModel input)
        {
            await this.productsService.EditProductAsync(id, input.EditProductInputModel);
            return this.Redirect($"/Administration/Administration/EditProduct?id={id}");
        }

        public IActionResult CreateCategory()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryInputModel input)
        {
            await this.categoryService.CreateCategoryAsync(input.Name);

            return this.Redirect("/MyAccount/MyAccount");
        }

        public IActionResult CreateSubCategory()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubCategory(SubCategoryInputModel input)
        {
            await this.categoryService.CreateSubCategoryAsync(input.SubCategoryName, input.CategoryName);

            return this.Redirect("/MyAccount/MyAccount");
        }

        public async Task<IActionResult> AllCategoriesAndSubCategories()
        {
            var categories = await this.categoryService.GetAllCategoriesWithDeletedAsync();
            var output = new AllCategoriesAndSubCategoriesViewModell
            {
                Categories = categories.Select(x => new CategoryViewModel
                {
                    Name = x.Name,
                    IsDeleted = x.IsDeleted,
                }).ToList(),
            };

            return this.View(output);
        }

        public async Task<IActionResult> DeleteUndeleteCategory(string name)
        {
            await this.categoryService.DeleteUnDeleteCategoryByNameAsync(name);

            return this.RedirectToAction("AllCategoriesAndSubCategories");
        }

        public async Task<IActionResult> DeleteUndeleteSubCategory(string name)
        {
            await this.categoryService.DeleteUnDeleteSubCategoryByNameAsync(name);

            return this.RedirectToAction("AllCategoriesAndSubCategories");
        }
    }
}
