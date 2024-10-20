using Microsoft.AspNetCore.Identity;

namespace AJKAccessControl.Infrastructure.Identity
{
    public class IdentityRoleSeeder
    {
        private readonly RoleManager<IdentityRole<Guid>> _roleManager;

        public IdentityRoleSeeder(RoleManager<IdentityRole<Guid>> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task CreateDefaultRoles()
        {
            var roles = Enum.GetValues(typeof(Role)).Cast<Role>().Select(r => r.ToString()).ToList();
            foreach (var role in roles)
            {
                var roleExists = await _roleManager.RoleExistsAsync(role.ToString());
                if (!roleExists)
                {
                    var result = await _roleManager.CreateAsync(new IdentityRole<Guid>(role.ToString()));
                }
            }
        }
    }
}