using Microsoft.AspNetCore.Identity;

namespace Server.Data
{
    public class SeedAdministratorRoleAndUser
    {

        internal const string AdministratorRoleName = "Administrator";
        internal const string AdministratorUserName = "admin@year4.com";

        internal async static Task Seed(RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager) 
        {
            await SeedAdministratorRole(roleManager);
            await SeedUserRole(userManager);
        }

        private async static Task SeedAdministratorRole(RoleManager<IdentityRole> roleManager)
        {

            bool adminRoleExists = await roleManager.RoleExistsAsync(AdministratorRoleName);

            if(adminRoleExists == false)
            {

                var role = new IdentityRole { Name = AdministratorRoleName };

                await roleManager.CreateAsync(role);

            }

        }

        private async static Task SeedUserRole(UserManager<IdentityUser> userManager)
        {

            bool adminUserExists = await userManager.FindByEmailAsync(AdministratorUserName)!=null;

            if (adminUserExists == false)
            {

                var user = new IdentityUser { 
                    UserName = AdministratorUserName, 
                    Email = AdministratorUserName
                
                };

                IdentityResult identityResult = await userManager.CreateAsync(user,"Year4!");

                if (identityResult.Succeeded)
                {

                    await userManager.AddToRoleAsync(user, AdministratorRoleName);


                }

            }

        }


    }
}
