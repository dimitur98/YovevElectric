using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YovevElectric.Web.ViewModels.Category
{
    public class SubCategoryInputModel
    {
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "Не е написано име на подкатегория.")]
        public string SubCategoryName { get; set; }
    }
}
