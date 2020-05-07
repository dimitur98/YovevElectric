using System;
using System.Collections.Generic;
using System.Text;
using YovevElectric.Data.Common.Models;

namespace YovevElectric.Data.Models
{
    public class SubCategory : BaseDeletableModel<string>
    {
        public SubCategory()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Name { get; set; }

        public string CategoryId { get; set; }

        public Category Category { get; set; }
    }
}
