using System;
using System.Collections.Generic;
using System.Text;
using YovevElectric.Data.Common.Models;

namespace YovevElectric.Data.Models
{
    public class OrderData : BaseDeletableModel<string>
    {
        public OrderData()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string City { get; set; }

        public string PostCode { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Adress { get; set; }

        public string MobileNumber { get; set; }

        public string FirmName { get; set; }

        public string Bulstad { get; set; }

        public string MOL { get; set; }

        public bool IsNew { get; set; }
    }
}
