using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using StableManager.Models;
using Microsoft.AspNetCore.Identity;

namespace StableManager.Data
{
    //This class is required to initialize the context/db
    public static class DbInitializer
    {
        public static async Task InitializeAsync(ApplicationDbContext context, IServiceProvider serviceProvider)
        {
            context.Database.EnsureCreated();

            //Look for Stable, if not found, add stable init data
            if (context.StableDetails.Any() == false)
            {
                var StableDetail = new StableDetails { StableName = "Zedan Meadows", ContactName = "Stable Owner", StableDetailsID = Guid.NewGuid().ToString(), StableAddress = "123 Fake Street", StableCity = "Edmonton", StableProvState = "AB", StableCountry = "Canada", StablePostalCode = "T1A 2B3", StableEmail = "ZedanMeadows@Zedan.com", StablePhone = "780 123 4567", TaxNumber = "1111-11111", ModifiedOn = DateTime.Now, ModifierUserID = "SYSTEM_INIT" };
                context.StableDetails.Add(StableDetail);
                context.SaveChanges();
            }

            //Look for Admin user, if not found init it
            if (context.Users.Any() == false)
            {
                var AdminUser = new ApplicationUser { ActiveUser = true, Email = "Admin@Admin.com", UserName = "Admin@Admin.com", UserNumber = "SYS_ADMIN", ModifierUserID = "SYTEM_INIT", ModifiedOn = DateTime.Now, IsAdmin = true };
                var AdminID = await AddUser(serviceProvider, "Admin123!", AdminUser);
                
            }

            //Adds roles
            await InitRoles(serviceProvider);
            var Admin = context.Users.FirstOrDefault(u => u.UserName == "Admin@Admin.com");
            await InitRolesToUser(serviceProvider, Admin, "Administrator");

        }

        private static async Task<string> AddUser(IServiceProvider serviceProvider,
                                            string password, ApplicationUser applicationUser)
        {
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

            var user = await userManager.FindByNameAsync(applicationUser.UserName);
            if (user == null)
            {
                await userManager.CreateAsync(applicationUser, password);
            }


            return user.Id;
        }

        private static async Task<bool> InitRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            IdentityResult roleResult;

            string[] roles = { "Administrator", "Client" };

            foreach (var roleName in roles)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    //create the roles and seed them to the database: Question 1
                    roleResult = await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
            return true;
        }

        private static async Task<bool> InitRolesToUser(IServiceProvider serviceProvider, ApplicationUser applicationUser, string role)
        {
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

            if (await userManager.IsInRoleAsync(applicationUser, role) == false)
            {

                await userManager.AddToRoleAsync(applicationUser, role);
                return true;
            }

    
            return false;
        }
    }
}
