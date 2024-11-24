using AJKAccessControl.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace AJKAccessControl.Infrastructure.Identity
{
    public class IdentityUserSeeder
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public IdentityUserSeeder(UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task CreateDefaultUsers()
        {
            var result = await _userManager.CreateAsync(new User
            {
                UserName = "ADMIN",
                Email = "",
                EmailConfirmed = true,
                FirstName = "System",
                LastName = "Admin",
                CreatedBy = "System"
            });
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync("ADMIN");
                if (user == null)
                {
                    throw new Exception("The user Admin was not created.");
                }
                await _userManager.AddPasswordAsync(user, "Admin@123");
                await _userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}