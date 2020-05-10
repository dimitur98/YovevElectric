using System;
using System.Collections.Generic;
using System.Text;
using YovevElectric.Data.Models;
using YovevElectric.Services.Mapping;

namespace YovevElectric.Web.ViewModels.Bag
{
    public class ProductInBagViewModel : IMapFrom<ProductQuantity>
    {
        public string Id { get; set; }

        public string ProductId { get; set; }

        public string ProductTitle { get; set; }

        public decimal ProductPrice { get; set; }

        public int Quantity { get; set; }

        public string ProductImgPath { get; set; }

        public decimal TotalPrice => this.Quantity * this.ProductPrice;
    }
}
