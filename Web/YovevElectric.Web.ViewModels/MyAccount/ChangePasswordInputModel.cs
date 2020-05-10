using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YovevElectric.Web.ViewModels.MyAccount
{
    public class ChangePasswordInputModel
    {
        [Required(ErrorMessage = "Полето 'Стара парола' не е попълнено.")]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "Полето 'Нова парола' не е попълнено.")]
        public string NewPassword { get; set; }

        [Required (ErrorMessage = "Полето 'Потвърди нова парола' не е попълнено.")]
        [Compare("NewPassword", ErrorMessage = "'Нова парола' и 'Потвърди нова парола' не съвпадат.")]
        public string ConfirmNewPassword { get; set; }
    }
}
