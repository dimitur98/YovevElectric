using System;
using System.Collections.Generic;
using System.Text;

namespace YovevElectric.Web.ViewModels.Discounts
{
    public class ApplyDiscountModel
    {
        public decimal PriceWithDiscount { get; set; }

        public double Percent { get; set; }

        public decimal OverPrice { get; set; }
    }
}
