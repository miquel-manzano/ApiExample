using Microsoft.AspNetCore.Identity;

namespace T1_APIREST.Tools
{
    public static class RoleTools
    {
        public static async Task CrearRolsInicials(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            string[] rols = { "Admin", "Editor" };

            foreach (var rol in rols)
            {
                if (!await roleManager.RoleExistsAsync(rol))
                {
                    await roleManager.CreateAsync(new IdentityRole(rol));
                }
            }
        }
    }
}
