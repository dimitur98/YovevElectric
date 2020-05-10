namespace YovevElectric.Web.ViewModels.MyAccount
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class DeleteAccountInputModel
    {
        [Required(ErrorMessage = "Полето 'Имейл' не е попълнено.")]
        public string Email { get; set; }
    }
}
