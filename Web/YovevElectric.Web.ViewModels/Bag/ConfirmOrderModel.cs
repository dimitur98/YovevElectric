using System;
using System.Collections.Generic;
using System.Text;

namespace YovevElectric.Web.ViewModels.Bag
{
    public class ConfirmOrderModel
    {
        public string City { get; set; }

        public string PostCode { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string MobileNumber { get; set; }

        public ICollection<ProductInBagViewModel> Products { get; set; }
    }
}
