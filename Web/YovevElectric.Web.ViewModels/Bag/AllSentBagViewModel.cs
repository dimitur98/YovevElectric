using System;
using System.Collections.Generic;
using System.Text;
using YovevElectric.Data.Models;
using YovevElectric.Services.Mapping;

namespace YovevElectric.Web.ViewModels.Bag
{
    public class AllSentBagViewModel : IMapFrom<YovevElectric.Data.Models.Bag>
    {
        public string Id { get; set; }

        public string OrderDataFirstName { get; set; }

        public string OrderDataLastName { get; set; }

        public string OrderDataMobileNumber { get; set; }

        public bool IsNew { get; set; }

        public DateTime DateOfSent { get; set; }
    }
}
