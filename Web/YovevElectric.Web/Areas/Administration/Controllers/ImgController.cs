// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace YovevElectric.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using YovevElectric.Common;
    using YovevElectric.Services.Data;
    using YovevElectric.Web.ViewModels.Img;
    using YovevElectric.Web.ViewModels.Product;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class ImgController : Controller
    {
        private readonly Cloudinary cloudinary;
        private readonly IImgService imgService;
        private readonly IProductsService productsService;

        public ImgController(Cloudinary cloudinary, IImgService imgService, IProductsService productsService)
        {
            this.cloudinary = cloudinary;
            this.imgService = imgService;
            this.productsService = productsService;
        }

        public IActionResult ImgUpload(string id)
        {
            var output = new ImgUploadViewModel { ProductId = id };

            return this.View(output);
        }

        [HttpPost]
        public async Task<IActionResult> ImgUpload(ImgUploadInputModel input)
        {
            if (input.ProductId == null)
            {
                return this.View("Error");
            }

            var imgPath = string.Empty;
            for (int i = 1; i <= 4; i++)
            {
                var file = (IFormFile)input.GetType().GetProperty("ProductImg" + i).GetValue(input, null);
                imgPath += await this.imgService.UploadImgAsync(file) + ",";
            }

            await this.imgService.AddImgToCurrentProductAsync(imgPath, input.ProductId);
            return this.Redirect($"/Administration/Administration/EditProduct?id={input.ProductId}");
        }

        public async Task<IActionResult> DeleteProductImg(string id, string imgNumber)
        {
            await this.imgService.DeleteProductImgFromProductAsync(id, int.Parse(imgNumber));

            return this.Redirect($"/Administration/Administration/EditProduct?id={id}");
        }

        public async Task<IActionResult> DeleteCategoryImg(string id)
        {
            await this.imgService.DeleteCategoryImgByIdAsync(id);

            return this.Redirect($"/Administration/Administration/EditCategory?id={id}");
        }
    }
}
