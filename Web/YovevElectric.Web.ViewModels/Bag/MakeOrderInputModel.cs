using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YovevElectric.Web.ViewModels.Bag
{
    public class MakeOrderInputModel
    {
        [Required (ErrorMessage = "Моля, напишете град.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Моля, напишете пощенски код.")]
        public string PostCode { get; set; }

        [Required(ErrorMessage = "Моля, напишете си името.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Моля, напишете си фамилията.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Моля, напишете си адреса.")]
        public string Adress { get; set; }

        [Required(ErrorMessage = "Моля, напишете си телефонния номер.")]
        public string MobileNumber { get; set; }

        public string MoreInfo { get; set; }

        public string FirmName { get; set; }

        public string Bulstad { get; set; }

        public string MOL { get; set; }

        public ICollection<ProductInBagViewModel> Products { get; set; }

    }
}
