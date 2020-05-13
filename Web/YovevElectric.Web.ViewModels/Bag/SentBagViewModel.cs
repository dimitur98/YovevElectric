using System;
using System.Collections.Generic;
using System.Text;
using YovevElectric.Data.Models;
using YovevElectric.Services.Mapping;

namespace YovevElectric.Web.ViewModels.Bag
{
    public class SentBagViewModel : IMapFrom<YovevElectric.Data.Models.Bag>
    {
        public string City { get; set; }

        public string PostCode { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Adress { get; set; }

        public string MobileNumber { get; set; }

        public string FirmName { get; set; }

        public string Bulstad { get; set; }

        public string MOL { get; set; }

        public string MoreInfo { get; set; }

        public decimal Price { get; set; }

        public decimal TotalPrice { get; set; }

        public ICollection<ProductInBagViewModel> Products { get; set; }


    }
}
