using System;
using System.Collections.Generic;
using System.Text;

namespace YovevElectric.Web.ViewModels.Bag
{
    public class BagModel
    {
        public ICollection<ProductInBagViewModel> Products { get; set; }

        public decimal Price { get; set; }

        public decimal TotalPrice { get; set; }

        public MakeOrderInputModel Order { get; set; }
    }
}
