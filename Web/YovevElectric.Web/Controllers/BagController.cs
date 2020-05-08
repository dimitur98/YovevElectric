using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using YovevElectric.Data.Models;
using YovevElectric.Services.Data;
using YovevElectric.Web.ViewModels.Bag;
using YovevElectric.Web.ViewModels.Product;

namespace YovevElectric.Web.Controllers
{
    public class BagController : Controller
    {
        private readonly IBagService bagService;
        private readonly UserManager<ApplicationUser> userManager;

        public BagController(IBagService bagService, UserManager<ApplicationUser> userManager)
        {
            this.bagService = bagService;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> AddToBag(ProductDetailsModel input)
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            await this.bagService.AddProductToBagAsync(userId, input.AddToBagInputModel.Quantity, input.AddToBagInputModel.ProductId);

            return this.Redirect($"/Products/Details?id={input.AddToBagInputModel.ProductId}");
        }

        public async Task<IActionResult> Bag()
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var bagProducts = await this.bagService.GetProductsFromBagByIdAsync(user.BagId);

            var output = new BagModel
            {
                Products = bagProducts,
                Price = await this.bagService.TotalPriceOfBagAsync(user.BagId),
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

        
        public async Task<IActionResult> MakeOrder(BagModel input)
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var output = new ConfirmOrderModel
            {
                FirstName = input.Order.FirstName,
                LastName = input.Order.LastName,
                City = input.Order.City,
                MobileNumber = input.Order.MobileNumber,
                PostCode = input.Order.PostCode,
                Products = await this.bagService.GetProductsFromBagByIdAsync(user.BagId),
            };

            return this.View(output);
        }
    }
}
