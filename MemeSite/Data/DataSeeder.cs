using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using MemeSite.Model;

namespace MemeSite.Data
{
    public static class DataSeeder
    {
        public static void SeedData(UserManager<PageUser> userManager, RoleManager<PageRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        public static void SeedUsers(UserManager<PageUser> userManager)
        {
            if (userManager.FindByNameAsync("normaluser").Result == null)
            {
                PageUser user = new PageUser();
                user.UserName = "normaluser";
                user.Email = "us1@x.d";

                IdentityResult result = userManager.CreateAsync
                (user, "P@ssw0rd").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "NormalUser").Wait();
                }
            }

            if (userManager.FindByNameAsync("admin").Result == null)
            {
                PageUser user = new PageUser();
                user.UserName = "admin";
                user.Email = "xd@xd.xd";

                IdentityResult result = userManager.CreateAsync
                (user, "P@ssw0rd").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Administrator").Wait();
                }
            }
        }

        public static void SeedRoles(RoleManager<PageRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("NormalUser").Result)
            {
                PageRole role = new PageRole();
                role.Name = "NormalUser";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Administrator").Result)
            {
                PageRole role = new PageRole();
                role.Name = "Administrator";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }
    }
}
