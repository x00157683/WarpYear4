using Microsoft.AspNetCore.Identity;
using Shared.Models;

namespace Server.Data
{
    public class SeedAdministratorRoleAndUser
    {

        internal const string AdministratorRoleName = "Administrator";
        internal const string AdministratorUserName = "admin@year4.com";

        internal async static Task Seed(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager) 
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

        private async static Task SeedUserRole(UserManager<AppUser> userManager)
        {

            bool adminUserExists = await userManager.FindByEmailAsync(AdministratorUserName)!=null;

            if (adminUserExists == false)
            {

                var user = new AppUser { 
               
                    Name = AdministratorUserName,
                    UserName = AdministratorUserName, 
                    Email = AdministratorUserName,
                    PhoneNumber = "0869887410",
                    Password = "Year4!"

                
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
