using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace YovevElectric.Web.ViewModels.Category
{
    public class CategoryInputModel
    {
        public IFormFile Img { get; set; }

        public string Name { get; set; }
    }
}
