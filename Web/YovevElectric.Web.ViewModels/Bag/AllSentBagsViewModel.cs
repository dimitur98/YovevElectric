namespace YovevElectric.Web.ViewModels.Bag
{
    using System.Collections.Generic;

    public class AllSentBagsViewModel
    {
        public OrderSearchInputModel OrderSearch { get; set; }

        public IEnumerable<AllSentBagViewModel> Bags { get; set; }
    }
}
