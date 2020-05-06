namespace YovevElectric.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    public class MyAccountController : Controller
    {
        public IActionResult MyAccount()
        {
            return this.View();
        }
    }
}
