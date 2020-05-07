namespace YovevElectric.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using YovevElectric.Data.Common.Models;

    public class Category : BaseDeletableModel<string>
    {
        public Category()
        {
            this.Id = Guid.NewGuid().ToString();
            this.SubCategories = new HashSet<SubCategory>();
        }

        public string Name { get; set; }

        public ICollection<SubCategory> SubCategories { get; set; }
    }
}
