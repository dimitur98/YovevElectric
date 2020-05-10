namespace YovevElectric.Web.ViewModels.Category
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CategoryViewModel
    {
        public string Name { get; set; }

        public string ImgPath { get; set; }

        public bool IsDeleted { get; set; }

        public string Statuse => this.IsDeleted ? "Неактивно" : "Активно";
    }
}
