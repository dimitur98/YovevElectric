using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YovevElectric.Web.ViewModels.Bag
{
    public class AddToBagInputModel
    {
        public string ProductId { get; set; }

        [Required(ErrorMessage = "Попълни количество")]
        public int Quantity { get; set; }
    }
}
