namespace YovevElectric.Web.ViewModels.Category
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using Microsoft.AspNetCore.Http;

    public class CategoryInputModel
    {
        public IFormFile Img { get; set; }

        [Required(ErrorMessage = "Не е написано име на категория.")]
        public string Name { get; set; }
    }
}
