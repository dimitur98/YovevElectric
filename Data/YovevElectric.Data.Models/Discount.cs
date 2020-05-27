using System;
using System.Collections.Generic;
using System.Text;
using YovevElectric.Data.Common.Models;

namespace YovevElectric.Data.Models
{
    public class Discount : BaseDeletableModel<int>
    {
        public double Percents { get; set; }

        public decimal OverPrice { get; set; }
    }
}
