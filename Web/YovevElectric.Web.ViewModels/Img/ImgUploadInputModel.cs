namespace YovevElectric.Web.ViewModels.Img
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Microsoft.AspNetCore.Http;

    public class ImgUploadInputModel
    {
        public string ProductId { get; set; }

        public IFormFile ProductImg1 { get; set; }

        public IFormFile ProductImg2 { get; set; }

        public IFormFile ProductImg3 { get; set; }

        public IFormFile ProductImg4 { get; set; }

    }
}
