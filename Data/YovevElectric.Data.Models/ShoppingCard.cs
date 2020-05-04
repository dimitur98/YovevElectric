using System;
using System.Collections.Generic;
using System.Text;
using YovevElectric.Data.Common.Models;

namespace YovevElectric.Data.Models
{
    public class ShoppingCard : BaseDeletableModel<string>
    {
        public ShoppingCard()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public decimal TotalPrice { get; set; }

        public bool Sent { get; set; }

        public bool IsNew { get; set; }
    }
}
