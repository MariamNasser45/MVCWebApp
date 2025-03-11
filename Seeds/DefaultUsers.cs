using Microsoft.AspNetCore.Identity;
using MVCWebApp.Models;
using MVCWebApp.Models.Enums;

namespace MVCWebApp.Seeds
{
    public class DefaultUsers
    {
        public async static Task SeedAdmin(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            var adminUser = new User
            {
                FirstName = "Admin",
                LastName = "Admin",
                UserName = "Admin",
                Email = "Admin@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "01112425154",
                PhoneNumberConfirmed = true,
            };

            var checkAdmin = await userManager.FindByEmailAsync(adminUser.Email);

            if (checkAdmin == null)
            {
                var result = await userManager.CreateAsync(adminUser , "Admin@123");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, RolesEnum.Admin.ToString());
                }
            }
        }

        public async static Task SeedUser(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            var adminUser = new User
            {
                FirstName = "User",
                LastName = "User",
                UserName = "User",
                Email = "User@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = "01112425154",
                PhoneNumberConfirmed = true,
            };

            var checkAdmin = await userManager.FindByEmailAsync(adminUser.Email);

            if (checkAdmin == null)
            {
                var result = await userManager.CreateAsync(adminUser, "User@123");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, RolesEnum.User.ToString());
                }
            }
        }
    }
}
