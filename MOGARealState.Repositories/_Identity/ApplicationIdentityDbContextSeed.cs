using Microsoft.AspNetCore.Identity;
using MOGARealState.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MOGARealState.Repositories._Identity
{
    public class ApplicationIdentityDbContextSeed
    {
        public static async Task SeedUserAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Ensure Admin role exists
            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                await roleManager.CreateAsync(new IdentityRole("Admin"));
            }

            // Check specifically for the admin user
            var adminUser = await userManager.FindByEmailAsync("admin@admin.com");
            if (adminUser == null)
            {
                // Admin user doesn't exist, create it
                var user = new AppUser()
                {
                    Email = "admin@admin.com",
                    UserName = "admin123",
                    City = "Cairo"
                };

                var result = await userManager.CreateAsync(user, "P@ssw0rd");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
            else if (!(await userManager.IsInRoleAsync(adminUser, "Admin")))
            {
                // Admin user exists but might not have Admin role
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}
