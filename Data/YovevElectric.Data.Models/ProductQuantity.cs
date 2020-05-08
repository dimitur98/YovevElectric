namespace YovevElectric.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using YovevElectric.Data.Common.Models;

    public class ProductQuantity : BaseDeletableModel<int>
    {
        public string ProductId { get; set; }

        public Product Product { get; set; }

        public int Quantity { get; set; }

        public string BagId { get; set; }

        public Bag Bag { get; set; }
    }
}
