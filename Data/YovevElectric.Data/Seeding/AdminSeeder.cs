namespace YovevElectric.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using YovevElectric.Common;
    using YovevElectric.Data.Common.Repositories;
    using YovevElectric.Data.Models;
    using YovevElectric.Data.Repositories;

    public class AdminSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userRepository = serviceProvider.GetService<IDeletableEntityRepository<ApplicationUser>>();
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetService<RoleManager<ApplicationRole>>();
            var user = await userRepository.AllWithDeleted().FirstOrDefaultAsync(x => x.Email == GlobalConstants.AdminUser);
            if (user != null)
            {
                return;
            }

            await userManager.CreateAsync(
                new ApplicationUser
                {
                    UserName = "Admin@admin.bg",
                    Email = "Admin@admin.bg",
                    EmailConfirmed = true,
                    PhoneNumber = "0888888888",
                }, "123456");

            user = await userManager.FindByNameAsync("Admin@admin.bg");
            var role = await roleManager.FindByNameAsync("Administrator");
            var exist = dbContext.UserRoles.Any(x => x.UserId == user.Id && x.RoleId == role.Id);
            if (exist)
            {
                return;
            }

            await dbContext.UserRoles.AddAsync(new IdentityUserRole<string>
            {
                RoleId = role.Id,
                UserId = user.Id,
            });
        }
    }
}
