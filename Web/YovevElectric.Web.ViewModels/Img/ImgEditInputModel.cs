using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace YovevElectric.Web.ViewModels.Img
{
    public class ImgEditInputModel
    {
        public string ProductId { get; set; }

        public IFormFile ProductImg { get; set; }
    }
}
