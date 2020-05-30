using System;
using System.Collections.Generic;
using System.Text;

namespace YovevElectric.Web.ViewModels.Product
{
    public class ProductDetailsViewModel
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Category { get; set; }

        public string SubCategory { get; set; }

        public string FirstImg { get; set; }

        public IEnumerable<string> ImgList { get; set; }
    }
}
