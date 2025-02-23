using Microsoft.AspNetCore.Identity;

namespace MOGARealState.APIs.Extensions
{
    public static class InitializeRoles
    {
        public static async Task InitializeRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] roleNames = { "User", "Agent", "Admin" };

            foreach (var roleName in roleNames)
            {
                var roleExists = await roleManager.RoleExistsAsync(roleName);
                if (!roleExists)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }
    }
}
