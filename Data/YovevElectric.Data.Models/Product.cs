namespace YovevElectric.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using YovevElectric.Data.Common.Models;

    public class Product : BaseDeletableModel<string>
    {
        public Product()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Category { get; set; }

        public string SubCategory { get; set; }

        public decimal Price { get; set; }

        public string ImgPath { get; set; }

    }
}
