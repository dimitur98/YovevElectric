﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using YovevElectric.Data.Models;
using YovevElectric.Services.Data;
using YovevElectric.Web.ViewModels.Bag;
using YovevElectric.Web.ViewModels.Product;

namespace YovevElectric.Web.Controllers
{
    [Authorize]
    public class BagController : Controller
    {
        private readonly IBagService bagService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IDiscountsService discountsService;

        public BagController(IBagService bagService, UserManager<ApplicationUser> userManager, IDiscountsService discountsService)
        {
            this.bagService = bagService;
            this.userManager = userManager;
            this.discountsService = discountsService;
        }

        [HttpPost]
        public async Task<IActionResult> AddToBag(ProductDetailsModel input)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await this.bagService.AddProductToBagAsync(userId, input.AddToBagInputModel.Quantity, input.AddToBagInputModel.ProductId);
            this.TempData["addToBag"] = true;
            return this.Redirect($"/Products/Products");
        }

        public async Task<IActionResult> Bag()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var bagProducts = await this.bagService.GetProductsFromBagByIdAsync(user.BagId);
            var price = await this.bagService.TotalPriceOfBagAsync(user.BagId);
            var discount = await this.discountsService.ApplyDiscountIfNeedAsync(price);
            var output = new BagModel
            {
                Products = bagProducts,
                Price = price,
                PriceWithDiscount = discount == null ? price : discount.PriceWithDiscount,
                Percent = discount == null ? 0 : discount.Percent,
                DiscountOverPrice = discount == null ? 0 : discount.OverPrice,
                Discounts = await this.discountsService.GetDiscountsAsync(),
            };
            return this.View(output);
        }

        public async Task<IActionResult> ClearBag()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            await this.bagService.ClearBagByIdAsync(user.BagId);
            return this.RedirectToAction("Bag");
        }

        public async Task<IActionResult> DeleteProductFromBag(int id)
        {
            await this.bagService.DeleteProductFromBagByIdAsync(id);
            return this.RedirectToAction("Bag");
        }

        [HttpPost]
        public async Task<IActionResult> MakeOrder(BagModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            await this.bagService.MakeOrderAsync(user.BagId, input.Order);

            user.BagId = null;
            await this.userManager.UpdateAsync(user);
            this.TempData["orderSent"] = true;
            return this.Redirect("/Products/Products");
        }

        
    }
}
