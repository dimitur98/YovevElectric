using System;
using System.Collections.Generic;
using System.Text;
using YovevElectric.Data.Models;
using YovevElectric.Services.Mapping;

namespace YovevElectric.Web.ViewModels.Bag
{
    public class SentBagViewModel : IMapFrom<YovevElectric.Data.Models.Bag>
    {
        public string OrderDataCity { get; set; }

        public string OrderDataPostCode { get; set; }

        public string OrderDataFirstName { get; set; }

        public string OrderDataLastName { get; set; }

        public string OrderDataAdress { get; set; }

        public string OrderDataMobileNumber { get; set; }

        public string OrderDataFirmName { get; set; }

        public string OrderDataBulstad { get; set; }

        public string OrderDataMOL { get; set; }

        public decimal Price { get; set; }

        public decimal TotalPrice { get; set; }

        public ICollection<ProductInBagViewModel> Products { get; set; }


    }
}
