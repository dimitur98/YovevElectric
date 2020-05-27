using System;
using System.Collections.Generic;
using System.Text;

namespace YovevElectric.Web.ViewModels.Discounts
{
    public class DiscountsViewModel
    {
        public int Id { get; set; }

        public double Percents { get; set; }

        public decimal OverPrice { get; set; }
    }
}
