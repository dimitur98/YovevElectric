using System;
using System.Collections.Generic;
using System.Text;

namespace YovevElectric.Web.ViewModels.Category
{
    public class CategoryViewModel
    {
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public string Statuse => this.IsDeleted ? "Неактивно" : "Активно";
    }
}
