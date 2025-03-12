using Microsoft.AspNetCore.Identity;
using ProductCatalog.Data;

namespace ProductCatalog.Seeds
{
    public static class DefaultRoles
    {
        public async static Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            var getAllRoles = roleManager.Roles.ToList();

            if (!getAllRoles.Any())
            {
               await roleManager.CreateAsync(new IdentityRole("Admin"));

                await roleManager.CreateAsync(new IdentityRole("User"));
            }
        }
    }
}
