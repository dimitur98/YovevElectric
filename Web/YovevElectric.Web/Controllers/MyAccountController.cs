namespace YovevElectric.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using YovevElectric.Common;
    using YovevElectric.Data.Models;
    using YovevElectric.Web.ViewModels.MyAccount;

    public class MyAccountController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public MyAccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult MyAccount()
        {
            return this.View();
        }

        public IActionResult ChangePassword()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            await this.userManager.ChangePasswordAsync(user, input.OldPassword, input.NewPassword);
            return this.Redirect("/");
        }

        public IActionResult DeleteAccount()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> DeleteAccount(DeleteAccountInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }
            var user = await this.userManager.GetUserAsync(this.User);

            if (user.Email == input.Email)
            {
                await this.signInManager.SignOutAsync();
                await this.userManager.DeleteAsync(user);
                return this.Redirect("/");
            }
            else
            {
                return this.View(input);
            }
        }
    }
}
