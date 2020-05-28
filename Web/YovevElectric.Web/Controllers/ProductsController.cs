using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YovevElectric.Common;
using YovevElectric.Data.Models;
using YovevElectric.Services.Data;
using YovevElectric.Web.ViewModels.Home;
using YovevElectric.Web.ViewModels.Product;

namespace YovevElectric.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductsService productsService;
        private readonly ICategoryService categoryService;
        private readonly IBagService bagService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDiscountsService discountsService;

        public ProductsController(
            IProductsService productsService,
            ICategoryService categoryService,
            IBagService bagService,
            UserManager<ApplicationUser> userManager,
            IDiscountsService discountsService)
        {
            this.productsService = productsService;
            this.categoryService = categoryService;
            this.bagService = bagService;
            this.userManager = userManager;
            this.discountsService = discountsService;
        }

        public async Task<IActionResult> Products(int page = 1, string category = null, string subCategory = null, string title = null, string orderBy = null)
        {
            var products = await this.productsService.GetAllProductsAsync((page - 1) * GlobalConstants.ItemsPerPage, category, subCategory, title, orderBy);
            var count = await this.productsService.GetProductsCount(category, subCategory, title);
            var pagesCount = (int)Math.Ceiling((double)count / GlobalConstants.ItemsPerPage);
            var user = await this.userManager.GetUserAsync(this.User);

            if (user == null)
            {
                user = new ApplicationUser();
                user.BagId = string.Empty;
            }

            var productsInBag = await this.bagService.GetProductsFromBagByIdAsync(user.BagId);
            var totalSum = await this.bagService.TotalPriceOfBagAsync(user.BagId);
            var discount = await this.discountsService.ApplyDiscountIfNeedAsync(totalSum);
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
                ProductsCount = await this.bagService.GetProductsCountInBagAsync(user.BagId),
                ProductsInBag = productsInBag.Take(4).ToList(),
                TotalSum = totalSum,
                OrderBy = orderBy == null ? string.Empty : orderBy,
                PriceWithDiscount = discount == null ? totalSum : discount.PriceWithDiscount,
            };
            if (category != null)
            {
                this.ViewData["category"] = category;
            }

            if (subCategory != null)
            {
                this.ViewData["subCategory"] = subCategory;
            }

            if (title != null)
            {
                this.ViewData["name"] = title;
            }

            if (orderBy != null)
            {
                this.ViewData["orderBy"] = orderBy;
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
