namespace YovevElectric.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using YovevElectric.Data.Models;

    public class ProductViewModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public decimal Price { get; set; }

        public string ImgPath { get; set; }
    }
}
