using Microsoft.AspNetCore.Identity;
using ProductCatalog.Models;
using ProductCatalog.Models.Enums;

namespace ProductCatalog.Seeds
{
    public class DefaultUsers
    {
        public async static Task SeedAdmin(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var adminUser = new User
            {
                FirstName = "Admin",
                LastName = "Admin",
                UserName = "Admin@123",
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

        public async static Task SeedUser(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var adminUser = new User
            {
                FirstName = "User",
                LastName = "User",
                UserName = "User@123",
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
