namespace YovevElectric.Web.ViewModels.Product
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using YovevElectric.Data.Models;

    public class EditProductInputModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Няма име.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Няма описание.")]
        public string Description { get; set; }

        public string Category { get; set; }

        public string SubCategory { get; set; }

        [Required(ErrorMessage = "Няма цена.")]
        public decimal Price { get; set; }

    }
}
