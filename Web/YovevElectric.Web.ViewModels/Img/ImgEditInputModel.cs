using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace YovevElectric.Web.ViewModels.Img
{
    public class ImgEditInputModel
    {
        public string ProductId { get; set; }

        public IFormFile ProductImg1 { get; set; }

        public IFormFile ProductImg2 { get; set; }

        public IFormFile ProductImg3 { get; set; }

        public IFormFile ProductImg4 { get; set; }
    }
}
