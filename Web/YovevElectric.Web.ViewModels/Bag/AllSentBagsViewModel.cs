using System;
using System.Collections.Generic;
using System.Text;
using YovevElectric.Data.Models;
using YovevElectric.Services.Mapping;

namespace YovevElectric.Web.ViewModels.Bag
{
    public class AllSentBagsViewModel
    {
        public ICollection<AllSentBagViewModel> Bags { get; set; }
    }
}
