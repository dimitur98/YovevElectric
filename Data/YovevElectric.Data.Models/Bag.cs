using System;
using System.Collections.Generic;
using System.Text;
using YovevElectric.Data.Common.Models;

namespace YovevElectric.Data.Models
{
    public class Bag : BaseDeletableModel<string>
    {
        public Bag()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public decimal TotalPrice { get; set; }

        public bool Sent { get; set; }

        public bool IsNew { get; set; }

        public DateTime DateOfSent { get; set; }

        public string OrderDataId { get; set; }

        public OrderData OrderData { get; set; }

        public string UserId { get; set; }

    }
}
