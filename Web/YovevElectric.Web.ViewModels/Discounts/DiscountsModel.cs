using System;
using System.Collections.Generic;
using System.Text;

namespace YovevElectric.Web.ViewModels.Discounts
{
    public class DiscountsModel
    {
        public DiscountInputModel DiscountInputModel { get; set; }

        public IEnumerable<DiscountsViewModel> Discounts { get; set; }
    }
}
