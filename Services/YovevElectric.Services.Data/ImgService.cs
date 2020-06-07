namespace YovevElectric.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using YovevElectric.Common;
    using YovevElectric.Data.Common.Repositories;
    using YovevElectric.Data.Models;

    public class ImgService : IImgService
    {
        private readonly Cloudinary cloudinary;
        private readonly IDeletableEntityRepository<Product> productRepository;
        private readonly IDeletableEntityRepository<Category> categoryRepository;

        public ImgService(
            Cloudinary cloudinary,
            IDeletableEntityRepository<Product> productRepository,
            IDeletableEntityRepository<Category> categoryRepository)
        {
            this.cloudinary = cloudinary;
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
        }

        public async Task<string> UploadImgAsync(IFormFile file)
        {
            if (file == null)
            {
                return string.Empty;
            }

            var fileFileExtension = Path.GetExtension(file.FileName);
            var contentType = file.ContentType;
            var size = file.Length;

            if (!this.IsValidImg(fileFileExtension, contentType, size) || file == null)
            {
                return string.Empty;
            }

            byte[] destinationImage;
            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                destinationImage = memoryStream.ToArray();
            }

            var result = string.Empty;
            using (var destinationStream = new MemoryStream(destinationImage))
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, destinationStream),
                };
                var res = await this.cloudinary.UploadAsync(uploadParams);

                result = res.Uri.AbsoluteUri;
            }

            return result;
        }

        public async Task AddImgToCurrentProductAsync(string imgPath, string id)
        {
            var product = this.productRepository.All().FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                throw new NullReferenceException();
            }

            if (product.ImgPath == GlobalConstants.DefaultImgProduct)
            {
                product.ImgPath = imgPath;
            }
            else
            {
                product.ImgPath += "," + imgPath;
            }

            this.productRepository.Update(product);
            await this.productRepository.SaveChangesAsync();
        }

        public bool IsValidImg(string fileExtension, string contentType, long size)
        {

            if (!string.Equals(fileExtension, ".jpg", StringComparison.OrdinalIgnoreCase)
                && !string.Equals(fileExtension, ".png", StringComparison.OrdinalIgnoreCase)
                && !string.Equals(fileExtension, ".jpeg", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            if (!string.Equals(contentType, "image/jpg", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(contentType, "image/jpeg", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(contentType, "image/pjpeg", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(contentType, "image/x-png", StringComparison.OrdinalIgnoreCase) &&
                !string.Equals(contentType, "image/png", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            if (size >= GlobalConstants.ImgMaxLength)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteProductImgFromProductAsync(string id, int imgNumber)
        {
            if (id != null)
            {
                var product = await this.productRepository.All().FirstOrDefaultAsync(x => x.Id == id);
                var imgPaths = product.ImgPath.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList();
                var imgForDel = imgPaths[imgNumber - 1];
                imgPaths.RemoveAt(imgNumber - 1);
                if (imgPaths.Count == 0)
                {
                    imgPaths.Add(GlobalConstants.DefaultImgProduct);
                }

                product.ImgPath = string.Join(",", imgPaths);
                this.productRepository.Update(product);
                await this.productRepository.SaveChangesAsync();

                await this.DeleteImgFromCloudAsync(imgForDel);
            }

            return false;
        }

        public async Task DeleteCategoryImgByIdAsync(string id)
        {
            var category = await this.categoryRepository.All().FirstOrDefaultAsync(x => x.Id == id);

            await this.DeleteImgFromCloudAsync(category.ImgPath);

            category.ImgPath = GlobalConstants.DefaultImgProduct;
            this.categoryRepository.Update(category);
            await this.categoryRepository.SaveChangesAsync();
        }

        public async Task DeleteImgFromCloudAsync(string imgForDel)
        {
            if (imgForDel != GlobalConstants.DefaultImgProduct)
            {
                imgForDel = imgForDel.Split("/", StringSplitOptions.RemoveEmptyEntries).Last();
                imgForDel = imgForDel.Split(".", StringSplitOptions.RemoveEmptyEntries).First();
                DeletionParams deletionParams = new DeletionParams(imgForDel)
                {
                    PublicId = imgForDel,
                };
                await this.cloudinary.DestroyAsync(deletionParams);
            }
        }
    }
}
