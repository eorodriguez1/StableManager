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

        public static async Task InitializeAsync2(IServiceProvider serviceProvider)
        {
            //gets current context
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            //we make sure that context exists
            context.Database.EnsureCreated();

            //If default information for stable is not available, create it
            if (!context.StableDetails.Any())
            {
                var StableDetail = new StableDetails { StableName = "Zedan Meadows", ContactName = "Stable Owner", StableDetailsID = Guid.NewGuid().ToString(), StableAddress = "123 Fake Street", StableCity = "Edmonton", StableProvState = "AB", StableCountry = "Canada", StablePostalCode = "T1A 2B3", StableEmail = "ZedanMeadows@Zedan.com", StablePhone = "780 123 4567", TaxNumber = "1111-11111", ModifiedOn = DateTime.Now, ModifierUserID = "SYSTEM_INIT" };
                context.StableDetails.Add(StableDetail);
            }

            //If there are no users, create an Admin user
            if (!context.Users.Any())
            {
                var AdminUser = new ApplicationUser { ActiveUser = true, Email = "Admin@Admin.com", UserName = "Admin@Admin.com", UserNumber = "SYS_ADMIN", ModifierUserID = "SYTEM_INIT", ModifiedOn = DateTime.Now, IsAdmin = true, Id = Guid.NewGuid().ToString()};
                var UserManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
                var User = await UserManager.CreateAsync(AdminUser, "Admin123!");
            }

            //If no roles are found, create the default roles
            if (!context.Roles.Any())
            {
                var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                string[] Roles = { "Administrator", "Client", "Trainer","Employee","ReadOnly"};

                foreach (var roleName in Roles)
                {
                    var Role = new IdentityRole(roleName);
                    var RoleResult = await RoleManager.CreateAsync(Role);
                }
            }

            //If there are no user roles defined, define default (Admin user to Admin role).
            if (!context.UserRoles.Any())
            {
                var AdminUser = context.Users.FirstOrDefault(u => u.UserName == "Admin@Admin.com");
                var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
                await userManager.AddToRoleAsync(AdminUser, "Administrator");
            }

            //Save all changes
            context.SaveChanges();

        }
    }
}
