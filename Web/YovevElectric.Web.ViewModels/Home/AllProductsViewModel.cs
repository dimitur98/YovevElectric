namespace YovevElectric.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using YovevElectric.Data.Models;

    public class AllProductsViewModel
    {
        public ICollection<ProductViewModel> AllProducts { get; set; }

        public int CurrentPage { get; set; }

        public int PagesCount { get; set; }

        public ICollection<Category> Categories { get; set; }
    }
}
