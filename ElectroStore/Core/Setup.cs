using ElectroStore.Data;
using ElectroStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElectroStore.Core
{
    public static class Setup
    {
        public static List<string> DefaultRoles = new List<string> { "Manager", "Employee", "Customer" };

        public static void RunAppDefaultAsync(IServiceProvider serviceProvider)
        {

            RoleManager<Role> roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
            foreach (string roleName in Setup.DefaultRoles)
            {
                if (!roleManager.RoleExistsAsync(roleName).Result)
                {
                    roleManager.CreateAsync(new Role(roleName)).Wait();
                }
            }
        }
    }
}
