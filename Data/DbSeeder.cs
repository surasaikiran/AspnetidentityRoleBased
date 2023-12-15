using AspnetidentityRoleBased.Constants;
using Microsoft.AspNetCore.Identity;

namespace AspnetidentityRoleBased.Data
{
    public static class DbSeeder
    {
        public static async Task SeedRolesandAdminAsync(IServiceProvider service)
        {
            //Seed Roles
            var userManager = service.GetService<UserManager<ApplicationUser>>();
            var roleManager = service.GetService<RoleManager<IdentityRole>>();
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.User.ToString()));

            //Creating Admin
            var user = new ApplicationUser
            {
                UserName = "admin@gmail.com",
                Email = "admin@gmail.com",
                Name = "Admin",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };
            var userInDb = await userManager.FindByIdAsync(user.Email);
            if(userInDb == null)
            {
                await userManager.CreateAsync(user, "Admin@123");
                await userManager.AddToRoleAsync(user, Roles.Admin.ToString());
            }

            
        }
    }
}
