using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YovevElectric.Web.ViewModels.Category
{
    public class CategoryInputModel
    {
        public IFormFile Img { get; set; }

        [Required(ErrorMessage = "Не е написано име на категория.")]
        public string Name { get; set; }
    }
}
