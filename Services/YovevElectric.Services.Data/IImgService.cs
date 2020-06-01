namespace YovevElectric.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;

    public interface IImgService
    {
        Task<string> UploadImgAsync(IFormFile file);

        Task AddImgToCurrentProductAsync(string imgPath, string id);

        Task<bool> DeleteProductImgFromProductAsync(string id, int imgNumber);

        bool IsValidImg(string fileExtension, string contentType, long size);

        Task DeleteImgFromCloudAsync(string imgForDel);
    }
}
