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
        [Range(0, int.MaxValue, ErrorMessage = "Количеството не може да бъде отрицателно число.")]
        public int Quantity { get; set; }
    }
}
