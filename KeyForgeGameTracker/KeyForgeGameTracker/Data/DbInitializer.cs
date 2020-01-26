using KeyForgeGameTracker.Areas.Identity.Models;
using KeyForgeGameTracker.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace KeyForgeGameTracker.Data
{
    public class DbInitializer
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using (var context = new KeyForgeContext(serviceProvider.GetRequiredService<DbContextOptions<KeyForgeContext>>(),
                                                     serviceProvider.GetRequiredService<IHttpContextAccessor>()))
            using (var userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>())
            {

                _ = await EnsureRole(serviceProvider, Role.Admin);
                _ = await EnsureRole(serviceProvider, Role.Standard);
                _ = await EnsureRole(serviceProvider, Role.ReadOnly);

                _ = await EnsureUser(serviceProvider, "graeme.dg.corrin@gmail.com", "Graeme", "Corrin", "Test12#$");
                _ = await EnsureUserRole(serviceProvider, "graeme.dg.corrin@gmail.com", Role.Admin);

            }
        }

        private static async Task<AppRole> EnsureRole(IServiceProvider serviceProvider, string rolename)
        {
            var roleManager = serviceProvider.GetService<RoleManager<AppRole>>();
            var role = await roleManager.FindByNameAsync(rolename);

            if (role == null)
            {
                role = new AppRole(rolename);
                await roleManager.CreateAsync(role);
            }
            return role;
        }

        private static async Task<AppUser> EnsureUser(IServiceProvider serviceProvider, string username, string firstName, string lastName, string password)
        {
            var userManager = serviceProvider.GetService<UserManager<AppUser>>();
            var user = await userManager.FindByNameAsync(username);

            if (user == null)
            {
                user = new AppUser
                {
                    UserName = username,
                    Email = username,
                    EmailConfirmed = true,
                    FirstName = firstName,
                    LastName = lastName
                };
                _ = await userManager.CreateAsync(user, password);

            }
            return user;
        }

        private static async Task<IdentityResult> EnsureUserRole(IServiceProvider serviceProvider, string username, string role)
        {
            var userManager = serviceProvider.GetService<UserManager<AppUser>>();
            var user = await userManager.FindByEmailAsync(username);

            if (user == null)
            {
                throw new Exception($"Cannot assign role.  User: {username} does not exist");
            }

            return await userManager.AddToRoleAsync(user, role);
        }
    }
}
