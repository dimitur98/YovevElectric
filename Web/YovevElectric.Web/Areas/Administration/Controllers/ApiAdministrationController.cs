using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YovevElectric.Services.Data;
using YovevElectric.Web.ViewModels.Administration.Product;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace YovevElectric.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = "Administrator")]
    [ApiController]
    [Route("api/administration/[controller]/[action]")]
    public class ApiAdministrationController : Controller
    {
        private readonly IProductsService productsService;

        public ApiAdministrationController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        [HttpPost]
        public async Task DeleteProduct(ApiProductInputModel input)
        {
            await this.productsService.DeleteProductAsync(input.Id);
        }

        [HttpPost]
        public async Task UnDeleteProduct(ApiProductInputModel input)
        {
            await this.productsService.UnDeleteProductAsync(input.Id);
        }
    }
}
