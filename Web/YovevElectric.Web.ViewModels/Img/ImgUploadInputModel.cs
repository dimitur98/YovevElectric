namespace YovevElectric.Web.ViewModels.Img
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.AspNetCore.Http;

    public class ImgUploadInputModel
    {
        public string ProductId { get; set; }

        public IFormFile ProductImg { get; set; }
    }
}
