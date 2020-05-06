// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace YovevElectric.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Mvc;
    using YovevElectric.Common;
    using YovevElectric.Services.Data;
    using YovevElectric.Web.ViewModels.Img;

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

            var imgPath = await this.imgService.UploadImgAsync(input.ProductImg);

            await this.imgService.AddImgToCurrentProductAsync(imgPath, input.ProductId);
            return this.Redirect("/MyAccount/MyAccount");

        }

        [HttpPost]
        public async Task<IActionResult> DeleteProductImg(ImgDeleteInputModel input)
        {

            await this.imgService.AddImgToCurrentProductAsync(GlobalConstants.DefaultImgProduct, input.ProductId);

            DeletionParams deletionParams = new DeletionParams(input.ImgPath)
            {
                PublicId = input.ImgPath,
            };
            await this.cloudinary.DestroyAsync(deletionParams);
            return this.View("EditProduct");
        }

        [HttpPost]
        public bool ImgValidation(ImgValidationInputModel input)
        {
            return true;
        }
    }
}
