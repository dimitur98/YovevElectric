namespace YovevElectric.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using YovevElectric.Common;
    using YovevElectric.Data.Models;
    using YovevElectric.Services.Data;
    using YovevElectric.Services.Mapping;
    using YovevElectric.Web.Controllers;
    using YovevElectric.Web.ViewModels.Bag;
    using YovevElectric.Web.ViewModels.Category;
    using YovevElectric.Web.ViewModels.Home;
    using YovevElectric.Web.ViewModels.Img;
    using YovevElectric.Web.ViewModels.Product;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
        private readonly IProductsService productsService;
        private readonly IImgService imgService;
        private readonly ICategoryService categoryService;
        private readonly IBagService bagService;
        private readonly IOrderDataService orderDataService;

        public AdministrationController(
            IProductsService productsService,
            IImgService imgService,
            ICategoryService categoryService,
            IBagService bagService,
            IOrderDataService orderDataService)
        {
            this.productsService = productsService;
            this.imgService = imgService;
            this.categoryService = categoryService;
            this.bagService = bagService;
            this.orderDataService = orderDataService;
        }

        public IActionResult CreateProduct()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CreateProductInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }
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
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

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
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.categoryService.CreateCategoryAsync(input.Name, input.Img);

            return this.Redirect("/");
        }

        public IActionResult CreateSubCategory()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateSubCategory(SubCategoryInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.categoryService.CreateSubCategoryAsync(input.SubCategoryName, input.CategoryName);

            return this.Redirect("/");
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

        public async Task<IActionResult> Orders()
        {
            var output = new AllSentBagsViewModel
            {
                Bags = await this.bagService.GetAllSentBags(),
            };

            return this.View(output);
        }

        public async Task<IActionResult> OrderDetails(string id)
        {
            var bag = await this.bagService.GetSentBagByIdAsync(id);
            var orderData = await this.orderDataService.GetOrderDataByIdAsync(bag.OrderDataId);
            var model = new SentBagViewModel
            {
                Adress = orderData.Adress,
                Bulstad = orderData.Bulstad,
                City = orderData.City,
                FirmName = orderData.FirmName,
                FirstName = orderData.FirstName,
                LastName = orderData.LastName,
                MobileNumber = orderData.MobileNumber,
                MOL = orderData.MOL,
                PostCode = orderData.PostCode,
                MoreInfo = orderData.MoreInfo,
                Products = await this.bagService.GetProductsFromBagByIdAsync(id),
                Price = await this.bagService.TotalPriceOfBagAsync(id),
            };

            return this.View(model);
        }
    }
}
