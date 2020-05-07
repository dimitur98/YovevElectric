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
    using YovevElectric.Web.ViewModels.Product;

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
            return this.Redirect($"/Administration/Administration/EditProduct?id={input.ProductId}");

        }

        
        

        public async Task<IActionResult> EditImg(string id)
        {
            var product = await this.productsService.GetProductWithDeletedByIdAsync(id);

            var output = new EditProductModel
            {
                ImgEditModel = new ImgEditModel
                {
                    ImgEditViewModel = new ImgEditViewModel
                    {
                        ProductId = product.Id,
                        ImgPath = product.ImgPath,
                    },
                },
            };

            return this.RedirectToAction("EditProduct", output);
        }


        
        public async Task<IActionResult> DeleteImg(string id)
        {
            var isDeleted = await this.imgService.DeleteProductImg(id);
            if (isDeleted)
            {
                this.ViewBag["deleteImg"] = true;
            }


            return this.Redirect($"/Administration/Administration/EditProduct?id={id}");
        }

        [HttpPost]
        public bool ImgValidation(ImgValidationInputModel input)
        {
            return true;
        }
    }
}
