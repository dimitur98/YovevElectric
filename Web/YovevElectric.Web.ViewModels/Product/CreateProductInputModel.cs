namespace YovevElectric.Web.ViewModels.Product
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using YovevElectric.Data.Models;

    public class CreateProductInputModel
    {
        [Required(ErrorMessage = "Не е въведено името на продукта.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Не е написано описание за продукта")]
        public string Description { get; set; }

        public string Category { get; set; }

        public string SubCategory { get; set; }

        [Required(ErrorMessage = "Не е посочена цена.")]
        public decimal Price { get; set; }

    }
}
